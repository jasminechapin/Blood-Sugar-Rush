using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyDrink : ACaffeinated
{
    public override void Start()
    {
        carbs = 30f;
        copy = Instantiate(nutritionFacts, transform);
        copy.transform.position = new Vector3(transform.position.x - 0.25f, transform.position.y + 1.5f, transform.position.z);

        copy.text = carbs + "g";
        copy.color = Color.red;
    }
}
