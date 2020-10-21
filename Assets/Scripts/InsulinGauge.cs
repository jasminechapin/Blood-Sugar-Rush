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

    [Range(0, 6)]
    public int numSyringes;

    private void Start()
    {
        numSyringes = 1;
        currentValue = 0;
        for (int i = 1; i < maxSyringes; i++) 
        {
            syringes[i].gameObject.SetActive(false);
        }   
        dose = 0;
        
        barText.text = "Dose";
        SetCurrentFraction = (currentValue / maxValue);
    }

    //set insulin gauge
    public float Slide()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            return 1;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            return -1;
        }

        return 0;
    }

    private void SetNumSyringes(int num)
    {
        if (num < numSyringes)
        {
            for (int i = maxSyringes; i > num; i--)
            {
                syringes[i - 1].gameObject.SetActive(false);
            }
        }
        else
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
        player.BloodSugar -= Math.Max(0, dose * player.CorrectionFactor);
        player.FinalBloodSugar = player.BloodSugar;
    }

    public void AddSyringe()
    {
        numSyringes++;
        SetNumSyringes(numSyringes);
    }

    protected override void Update()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKey("z"))
            {
                base.barText.text += currentValue.ToString();
                print(currentValue);
                if (changeDose)
                {
                    currentValue += Slide();
                }
                changeDose = !changeDose;
            }

            if (Input.GetKeyUp("z"))
            {
                dose = (int)currentValue;
                barText.text += dose.ToString();
                currentValue = 0f;
                maxValue -= dose;
                SetNumSyringes((int)(numSyringes - Mathf.CeilToInt(dose / 2))); //hardcoded bad boi
                                                                                //fill.rectTransform.right = new Vector3(fill.rectTransform.right.x + (210 * (numSyringes / maxSyringes)),
                                                                                //    fill.rectTransform.right.y, fill.rectTransform.right.z);
                AdministerInsulin();
                barText.text = "Dose: ";
            }
        } else
        {
            barText.text = "Dose: ";
        }


        UpdateBar();
    }
}
