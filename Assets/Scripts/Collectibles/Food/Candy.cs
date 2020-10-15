using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Collectibles.Food;

public class Candy : AFood
{
    public override void Start()
    {
        carbs = 2f;
        copy = Instantiate(nutritionFacts, transform);
        copy.transform.position = new Vector3(transform.position.x - 0.25f, transform.position.y + 1.5f, transform.position.z);

        copy.text = carbs + "g";
        copy.color = Color.red;
    }
}
