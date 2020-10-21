using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class CameraShake : MonoBehaviour
{
    public Vector3 originalPos;

    public IEnumerator Shake(float duration, float magnitude)
    {
        float x = originalPos.x + Random.Range(-0.05f, 0.05f) * magnitude;
        float y = originalPos.y + Random.Range(-0.05f, 0.05f) * magnitude;
        transform.localPosition = new Vector3(x, y, originalPos.z);
        yield return null;
    }

    private void Update()
    {
        originalPos = GetComponent<Camera2DFollow>().target.position;
    }
}
