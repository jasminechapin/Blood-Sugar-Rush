using Collectibles.Food;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public abstract class ACaffeinated : AFood
{
    protected float effectTime;

    //private void OnTriggerEnter2D(Collider2D col)
    //{
    //    if (col.gameObject.CompareTag("Player"))
    //    {
    //        col.GetComponent<PlatformerCharacter2D>().SpeedUp(effectTime, true);
    //        Destroy(gameObject);
    //    }
    //}
}
