using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityStandardAssets._2D;
//using UnityStandardAssets.CrossPlatformInput;
using Collectibles.Food;

[assembly: InternalsVisibleTo("InsulinGauge")]
[assembly: InternalsVisibleTo("A1cCalc")]

public class BloodSugarCalc : MonoBehaviour
{
    public float BloodSugar { get; set; }
    public float CarbRatio { get; set; }
    public bool Metabolizing { get; set; }
    public float CorrectionFactor { get; set; }
    public float FinalBloodSugar { get; set; }

    public CameraShake cameraShake;
    public GameObject lowVision;
    public PlatformerCharacter2D player;

    private float metabolismSpeed;
    private float runStartTime;
    private bool digestionModified;

    public enum BloodSugarThresholds
    {
        LOW = 75,
        TARGET = 120,
        HIGH = 240,
    };

    public enum Condition
    {
        Eating,
        Walking,
        Running,
        Stressed,
        Sick,
        InLove,
    };

    //example insulin dose math
    /*
     * 127g
     * 210 bg
     * ((210 - 150) / 30) = 2 units
     * (127 / 10) = 12.7 units
     * 14.7 units to maintain bg
     * reverse, no insulin:
     * 210 + 3 * 127 = 491 (near death)
     */

    private void Start()
    {
        BloodSugar = 150f;
        CarbRatio = 10f;
        // 1 unit for every 10 grams; 3 mg/dL inc for every gram
        //A Correction Factor (sometimes called insulin 
        //sensitivity), is how much 1 unit of rapid acting 
        //insulin will generally lower your blood glucose 
        //over 2 to 4 hours when you are in a fasting or pre-meal state.
        CorrectionFactor = 30f; //1 unit of insulin for every 30 mg/dL (2mmol) of inc of b.s.
        metabolismSpeed = 20f;
        FinalBloodSugar = BloodSugar;
        digestionModified = false;
        Metabolizing = false;
        player = GetComponent<PlatformerCharacter2D>();
    }

    float GetBG()
    {
        return BloodSugar;
    }

    //eating the foods
    private void Eat(float carbs)
    {
        FinalBloodSugar = FinalBloodSugar + carbs * (CorrectionFactor / CarbRatio);
    }

    //jumping through food
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Food"))
        {
            Eat(col.gameObject.GetComponent<AFood>().carbs);
        }
        // null ref exception when you leave radius
        if (col.gameObject.CompareTag("Oxytocin"))
        {
            BloodSugar = 60f;
            FinalBloodSugar = 60f;
        }
    }

    private void Update()
    {
        lowVision.transform.position = transform.position;
        if (BloodSugar <= 0)
        {
            Metabolizing = false;
            StopAllCoroutines();
        }
        else if (Math.Abs(BloodSugar - FinalBloodSugar) >= (1 / metabolismSpeed))
        {
            Metabolizing = true;
            StartCoroutine(Metabolize(Condition.Eating));
        }
        else
        {
            StopCoroutine(Metabolize(Condition.Eating));
            Metabolizing = false;

            if (digestionModified)
            {
                metabolismSpeed = 20f;
            }
        }

        //bool running = Math.Abs(CrossPlatformInputManager.GetAxis("Horizontal")) > 0;
        bool running = false;

        if (running)
        {
            print(runStartTime);
            Running();
        }
        else
        {
            runStartTime = 0;
        }

        CheckLowBloodSugar();
        //CheckHighBloodSugar();
    }

    private void CheckLowBloodSugar()
    {
        player.SpeedUp(holdChange: BloodSugar < (float)BloodSugarThresholds.LOW);

        if (BloodSugar < (float)BloodSugarThresholds.LOW)
        {
            StartCoroutine(cameraShake.Shake(3f, (BloodSugarThresholds.TARGET - BloodSugarThresholds.LOW) / 44));
            lowVision.SetActive(true);
        }
        else
        {
            StopCoroutine(cameraShake.Shake(3f, (BloodSugarThresholds.TARGET - BloodSugarThresholds.LOW) / 44));
            lowVision.SetActive(false);
        }
    }

    private void CheckHighBloodSugar()
    {
        player.SlowDown(holdChange: BloodSugar > (float)BloodSugarThresholds.HIGH);
    }

    // increases blood sugar by metabolism speed after 2 seconds
    private IEnumerator Metabolize(Condition condition)
    {
        //yield return new WaitForSeconds(5f);

        //changed from while to if
        if ((BloodSugar - FinalBloodSugar) >= (1 / metabolismSpeed))
        {
            BloodSugar -= (1 / metabolismSpeed);
            yield return new WaitForSeconds(5f);
        }
        else
        {
            BloodSugar += (1 / metabolismSpeed);
            yield return new WaitForSeconds(5f);
        }
        
        //yield return null;
    }

    public void SpeedUpDigestion()
    {
        metabolismSpeed /= 2;
        digestionModified = true;
    }

    public void SlowDownDigestion()
    {
        metabolismSpeed *= 2;
        digestionModified = true;
    }

    private void Running()
    {
        /*
         * 10-second calc delay
         * Blood sugar rises by 2 for every second of running
         * 40 seconds after initial 10 seconds of running, bg drops by 50
         */
        runStartTime += 1;

        if (runStartTime < 300)
        {
            FinalBloodSugar += 0.1f;
            // calc abruptly stopping
        }
        if (runStartTime > 500)
        {
            FinalBloodSugar -= 0.15f;
        }
    }

    private void Walking()
    {
        /*
         * 15-second calc delay
         * Blood sugar drops by 15 / s
         */
        throw new NotImplementedException();
    }
}
