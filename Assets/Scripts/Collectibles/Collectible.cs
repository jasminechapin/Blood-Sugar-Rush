using UnityEngine;

public class Collectible : MonoBehaviour
{
    protected bool collected = false;

    protected void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") && collected)
        {
            Destroy(gameObject);
        }
    }
}