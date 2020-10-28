using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InsulinGauge : APlayerBar
{
    public readonly static int maxSyringes = 6;
    public Syringe[] syringes = new Syringe[maxSyringes];
    bool changeDose = true;
    int dose;
    public RectTransform plunger;
    private float pullSpeed;
    private float pushSpeed;
    private Vector3 defaultPlungerLocation;

    [Range(0, 6)]
    public int numSyringes;

    private new void Awake()
    {
        base.Awake();
        defaultPlungerLocation = plunger.localPosition;
        numSyringes = 0;
        SetNumSyringes(6);

        currentValue = 0;
        dose = 0;
        pullSpeed = 2.7f;
        pushSpeed = pullSpeed * 4;
        
        barText.text = "Dose";
        SetCurrentFraction = (currentValue / maxValue);
    }

    private IEnumerator DrawInsulin(int maxInsulin)
    {
        yield return new WaitForSeconds(.2f);


        if (Input.GetKey(KeyCode.DownArrow) && currentValue <= maxInsulin)
        {
            currentValue += 0.025f;
            float y = plunger.localPosition.y - pullSpeed;
            plunger.localPosition = new Vector3(plunger.localPosition.x, y, plunger.localPosition.z);
        }

        else if (Input.GetKey(KeyCode.UpArrow) && currentValue >= 0)
        {
            currentValue -= 0.1f;
            float y = plunger.localPosition.y + pushSpeed;
            plunger.localPosition = new Vector3(plunger.localPosition.x, y, plunger.localPosition.z);
        }
    }

    private IEnumerator InjectInsulin()
    {
        while (currentValue > 0)
        {
            currentValue -= 0.2f;
            float y = plunger.localPosition.y + (pushSpeed * 2);
            plunger.localPosition = new Vector3(plunger.localPosition.x, y, plunger.localPosition.z);
            yield return null;
        }

        plunger.localPosition = defaultPlungerLocation;
    }

    private void SetNumSyringes(int num)
    {
        if (num < 0)
        {
            throw new Exception("Number of syringes is less than zero");
        }

        // remove syringes
        if (num < numSyringes)
        {
            for (int i = numSyringes; i > num; i--)
            {
                syringes[i - 1].gameObject.SetActive(false);
            }
        }

        // add syringes
        if (num > numSyringes)
        {
            for (int i = 0; i < num; i++)
            {
                syringes[i].gameObject.SetActive(true);
            }
        }

        numSyringes = num;
    }

    private void AdministerInsulin()
    {
        player.FinalBloodSugar -= Math.Max(0, dose * player.CorrectionFactor);
    }

    public void AddSyringe()
    {
        if (numSyringes < maxSyringes)
        {
            numSyringes++;
            SetNumSyringes(numSyringes);
        }
    }

    protected override void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            base.barText.text += currentValue.ToString();
            if (changeDose)
            {
                StartCoroutine(DrawInsulin(numSyringes));
            }
            changeDose = !changeDose;
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            StopCoroutine(DrawInsulin(numSyringes));
            dose = (int)currentValue;
            barText.text += dose.ToString();
            
            StartCoroutine(InjectInsulin());
            
            SetNumSyringes((int)(numSyringes - dose));
            AdministerInsulin();
            barText.text = "Dose: ";
        }

        UpdateBar();
    }
}
