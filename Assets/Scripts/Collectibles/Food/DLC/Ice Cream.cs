using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Collectibles.Food;

public class IceCream : AFood
{
    public override void Start()
    {
        carbs = 15f;
        copy = Instantiate(nutritionFacts, transform);
        copy.transform.position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);

        copy.text = carbs + "g";
        copy.color = Color.red;
    }
}
