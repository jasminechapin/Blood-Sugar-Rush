using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Oxytocin : MonoBehaviour
{
    private void OnCollisionStay2D(Collision2D collision)
    {
        StartCoroutine(Shrink());
        StartCoroutine(DelayedDestroy(2f));
    }

    private IEnumerator DelayedDestroy(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    private IEnumerator Shrink()
    {
        if (transform.localScale.x > 0)
        transform.localScale -= new Vector3(1f, 1f, 1f);

        yield return new WaitForSeconds(2f);
        yield return null;
    }
}
