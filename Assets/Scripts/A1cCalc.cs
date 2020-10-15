using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class A1cCalc : MonoBehaviour
{
    public float[] bloodSugarTimes;
    public float A1c;
    public BloodSugarCalc playerSugar;
    public float curSugar;
    public float elapsingTime;

    private enum A1cRanges
    {
        MIN = 0,
        MAX = 23,
    };

    [Range((float)A1cRanges.MIN, (float)A1cRanges.MAX)]
    public int prevBloodSugarRange;
    public TextMeshPro time;
    public int[] bloodSugarPoints;

    // Start is called before the first frame update
    void Start()
    {
        bloodSugarTimes = new float[24];
        curSugar = playerSugar.BloodSugar;
        prevBloodSugarRange = Mathf.FloorToInt(playerSugar.BloodSugar / 15);
        elapsingTime = 0f;
        SetBloodSugarPoints();
        A1c = 0f;
    }

    void SetBloodSugarPoints()
    {
        bloodSugarPoints = new int[24];

        #region Blood Sugar Points
        bloodSugarPoints[0] = -100;
        bloodSugarPoints[1] = -80;
        bloodSugarPoints[2] = -50;
        bloodSugarPoints[3] = -20;
        bloodSugarPoints[4] = 10;
        bloodSugarPoints[5] = 100;
        bloodSugarPoints[6] = 100;
        bloodSugarPoints[7] = 100;
        bloodSugarPoints[8] = 100;
        bloodSugarPoints[9] = 85;
        bloodSugarPoints[10] = 70;
        bloodSugarPoints[11] = 55;
        bloodSugarPoints[12] = 40;
        bloodSugarPoints[13] = 25;
        bloodSugarPoints[14] = 10;
        bloodSugarPoints[15] = -5;
        bloodSugarPoints[16] = -20;
        bloodSugarPoints[17] = -35;
        bloodSugarPoints[18] = -50;
        bloodSugarPoints[19] = -65;
        bloodSugarPoints[20] = -80;
        bloodSugarPoints[21] = -95;
        bloodSugarPoints[22] = -110;
        bloodSugarPoints[23] = -125;
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        curSugar = playerSugar.BloodSugar;
        time.text = Mathf.Floor(Time.time).ToString();
        elapsingTime += Time.deltaTime;

        if (Mathf.FloorToInt(playerSugar.BloodSugar / 15) != prevBloodSugarRange)
        {
            if (Mathf.FloorToInt(playerSugar.BloodSugar / 15) < (float)A1cRanges.MIN)
            {
                prevBloodSugarRange = (int)A1cRanges.MIN;
            }
            else if (Mathf.FloorToInt(playerSugar.BloodSugar / 15) > (float)A1cRanges.MAX)
            {
                prevBloodSugarRange = (int)A1cRanges.MAX;
            }
            else
            {
                prevBloodSugarRange = Mathf.FloorToInt(playerSugar.BloodSugar / 15);
            }

            UpdateSugarTimes(elapsingTime);
            elapsingTime = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !playerSugar.Metabolizing)
        {
            print(other.tag);
            //Time.timeScale = 0f; // change, but right idea!
            A1c = ReturnA1c();
        }
    }

    void UpdateSugarTimes(float elapsedTime)
    {
        bloodSugarTimes[prevBloodSugarRange] += elapsingTime;
    }

    public float ReturnA1c()
    {
        float[] points = new float[24];

        for (int i = 0; i < bloodSugarTimes.Length; i++)
        {
            print("percent time" + bloodSugarTimes[i]);
            float percentTimes = (bloodSugarTimes[i] / Time.time) * 100;
            //print(percentTimes + "," + bloodSugarPoints[i]);
            points[i] = percentTimes * bloodSugarPoints[i];
        }

        float maxPnt = -1f;

        foreach (float pnt in points)
        {
            if (pnt > maxPnt)
            {
                maxPnt = pnt;
            }
        }

        return maxPnt;
    }
}
