using Collectibles.Food;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : AFood
{
    public override void Start()
    {
        carbs = 20f;
        copy = Instantiate(nutritionFacts, transform);
        copy.transform.position = new Vector3(transform.position.x, transform.position.y + 0.15f, transform.position.z);

        copy.text = carbs + "g";
        copy.color = Color.red;
    }
}
