using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CravingsSlime : AEnemy
{
    private Animator animator;
    private Vector2 randPosn;

    static int AnimatorWalk = Animator.StringToHash("Walk");
    static int AnimatorAttack = Animator.StringToHash("Attack");

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (inRange)
        {
            animator.SetBool(AnimatorWalk, true);
            Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            FaceTarget(player.transform.position);

            if (Mathf.Abs(Vector2.Distance(transform.position, base.player.transform.position)) < 1f)
            {
                animator.SetTrigger(AnimatorAttack);
            }
        }
        else
        {
            if (Mathf.Abs(Vector2.Distance(randPosn, transform.position)) < 1f)
            {
                randPosn = new Vector2(UnityEngine.Random.Range(-8f, 8f), transform.position.y);
            }
            else
            {
                animator.SetBool(AnimatorWalk, true);
                Vector2.MoveTowards(transform.position, randPosn, 2f * Time.deltaTime);
                FaceTarget(randPosn);   
            }
        }
    }
}

