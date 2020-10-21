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

    private new void Awake()
    {
        base.Awake();
        numSyringes = 0;
        SetNumSyringes(6);

        currentValue = 0;
        dose = 0;
        
        barText.text = "Dose";
        SetCurrentFraction = (currentValue / maxValue);
    }

    private IEnumerator DrawInsulin(int maxInsulin)
    {
        if (Input.GetKey(KeyCode.DownArrow) && currentValue <= maxInsulin)
        {
            currentValue += 0.025f;
            yield return new WaitForSeconds(.15f);
        }
        else if (Input.GetKey(KeyCode.UpArrow) && currentValue >= 0)
        {
            currentValue -= 0.1f;
            yield return new WaitForSeconds(.1f);
        }
    }

    private void SetNumSyringes(int num)
    {
        if (num >= 0 && num <= 6)
        {
            if (num < numSyringes)
            {
                for (int i = numSyringes; i > num; i--)
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
        }

        numSyringes = num;
    }

    private void AdministerInsulin()
    {
        player.FinalBloodSugar -= Math.Max(0, dose * player.CorrectionFactor);
    }

    public void AddSyringe()
    {
        numSyringes++;
        SetNumSyringes(numSyringes);
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
            currentValue = 0;
            barText.text += dose.ToString();
            currentValue = 0f;// animate administering insulin?
            //maxValue -= dose;
            
            SetNumSyringes((int)(numSyringes - dose)); //hardcoded bad boi
                                                                            //fill.rectTransform.right = new Vector3(fill.rectTransform.right.x + (210 * (numSyringes / maxSyringes)),
                                                                            //    fill.rectTransform.right.y, fill.rectTransform.right.z);
            AdministerInsulin();
            barText.text = "Dose: ";
        }

        UpdateBar();
    }
}
