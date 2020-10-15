using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InsulinGauge : MonoBehaviour
{
    float dose;
    public Slider gauge;
    public BloodSugarCalc player;
    public Image fill;
    public readonly static int maxSyringes = 6;
    public GameObject[] syringes = new GameObject[maxSyringes];
    public TextMeshPro dosage;
    bool changeDose = true;
    public int numSyringes;

    private void Awake()
    {
        gauge.value = 0;
        foreach (GameObject syringe in syringes)
        {
            syringe.SetActive(true);
            numSyringes = maxSyringes;
        }
        dosage.text = dose.ToString();
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
                syringes[i - 1].SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < num; i++)
            {
                syringes[i].SetActive(true);
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

    private void Update()
    {
        if (Input.GetKey("z"))
        {
            dosage.text = gauge.value.ToString();
            print(gauge.value);
            if (changeDose)
            {
                gauge.value += Slide();
            }
            changeDose = !changeDose;
        }

        if (Input.GetKeyUp("z"))
        {
            dose = gauge.value;
            dosage.text = dose.ToString();
            gauge.value = 0f;
            gauge.maxValue -= dose;
            SetNumSyringes((int)(numSyringes - Mathf.CeilToInt(dose / 2))); //hardcoded bad boi
            //fill.rectTransform.right = new Vector3(fill.rectTransform.right.x + (210 * (numSyringes / maxSyringes)),
            //    fill.rectTransform.right.y, fill.rectTransform.right.z);
            AdministerInsulin();
            dosage.text = "0";
        }
    }
}
