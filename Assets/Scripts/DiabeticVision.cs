using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiabeticVision : MonoBehaviour
{
    public BloodSugarCalc player;

    void Update()
    {
        transform.position = player.gameObject.transform.position;
    }
}
