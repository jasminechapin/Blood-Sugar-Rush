using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Collectibles.Food
{
    public abstract class AFood : Collectible
    {
        public float carbs;
        public TMP_Text nutritionFacts;
        public TMP_Text copy;
        public BloodSugarCalc player;

        public abstract void Start();

        // if the player is within range, reveal the insulin dose
        private void OnTriggerStay2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("VisibleRange"))
            {
                copy.text = (carbs / player.CarbRatio).ToString() + " units";
            }
        }

        // if the player moves away from the food, hide the insulin dose and show the amount of carbs
        private void OnTriggerExit2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("VisibleRange"))
            {
                copy.text = carbs + "g";
            }
        }
    }
}
