using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Syringe : Collectible
{
    public InsulinGauge gauge;


    protected new void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") && !collected)
        {
            collected = true;
            gauge.AddSyringe();
            base.OnCollisionEnter2D(col);
        }
    }
}
