using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AEnemy : MonoBehaviour
{
    protected bool inRange;
    protected GameObject player;
    protected float speed;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        inRange = false;
        speed = 2f;
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        print(col.gameObject.tag);

        if (col.gameObject.tag == "Player")
        {
            inRange = true;
            player = col.gameObject;
        }
        else
        {
            inRange = false;
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (inRange)
        {
            Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            FaceTarget(player.transform.position);
        }
    }

    protected void FaceTarget(Vector3 target)
    {
        Vector2 positionOffset = target - transform.position;
        Vector2 rightward = new Vector2(transform.position.y, -target.x);
        bool object2IsToTheRight = Vector2.Dot(positionOffset, rightward) > 0;

        GetComponent<SpriteRenderer>().flipX = object2IsToTheRight;
    }
}
