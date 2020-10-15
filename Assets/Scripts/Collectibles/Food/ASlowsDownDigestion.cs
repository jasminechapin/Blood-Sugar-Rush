using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASlowsDownDigestion : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.GetComponent<BloodSugarCalc>().SlowDownDigestion();
            Destroy(gameObject);
        }
    }
}
