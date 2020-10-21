using System;
using System.Collections;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class PlatformerCharacter2D : MonoBehaviour
    {
        public readonly float defaultMaxSpeed = 1f;
        [SerializeField] private float m_MaxSpeed = 1f;                    // The fastest the player can travel in the x axis.
        [SerializeField] private float m_JumpForce = 350f;                  // Amount of force added when the player jumps.
//        [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
        [SerializeField] private bool m_AirControl = true;                 // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        private Transform m_WallCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        private bool m_Grounded;            // Whether or not the player is grounded.
        private bool m_OnWall;            // Whether or not the player is grounded.
        private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        const float k_OnWallRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        private Animator m_Anim;            // Reference to the player's animator component.
        private Rigidbody2D m_Rigidbody2D;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.

        private void Awake()
        {
            m_GroundCheck = transform.Find("GroundCheck");
            m_WallCheck = transform.Find("WallCheck");
            m_CeilingCheck = transform.Find("CeilingCheck");
            m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            m_Grounded = false;
            m_OnWall = false;

            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    m_Grounded = true;
                    m_Anim.SetBool("Ground", m_Grounded);
                }
                if (colliders[i].gameObject.CompareTag("Wall"))
                {
                    m_OnWall = true;
                    m_Anim.SetBool("OnWall", m_OnWall);
                    //slide off wall
                }
            }


            Collider2D[] colliders1 = Physics2D.OverlapCircleAll(m_WallCheck.position, k_OnWallRadius, m_WhatIsGround);
            for (int i = 0; i < colliders1.Length; i++)
            {
                if (colliders1[i].gameObject != gameObject)
                {
                    m_OnWall = true;
                    m_Anim.SetBool("OnWall", m_OnWall);
                    //slide off wall
                }
            }

            m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
        }

        // increases blood sugar by metabolism speed after 2 seconds
        private IEnumerator IncreaseSpeed(float move)
        {
            float targetSpeed = 6f;

            if (m_MaxSpeed < targetSpeed) {
                m_MaxSpeed += 0.5f;
                yield return new WaitForSeconds(2f);
            }
        }

        public void Move(float move, bool crouch, bool jump)
        {
            if (m_Grounded && jump && m_Anim.GetBool("Ground") && !m_Anim.GetBool("OnWall"))
            {
                m_Anim.SetTrigger("Jump");
                m_Grounded = false;
                m_Anim.SetBool("Ground", false);
                m_OnWall = false;
                m_Anim.SetBool("OnWall", false);
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            }

            //only control the player if grounded or airControl is turned on
            if (m_Grounded || m_AirControl)
            {
                if (Math.Abs(move) > 0 && Math.Abs(move) < 1)
                {
                    StartCoroutine(IncreaseSpeed(move));
                } else
                {
                    StopCoroutine(IncreaseSpeed(move));
                    m_MaxSpeed = 1f;
                }

                // The Speed animator parameter is set to the absolute value of the horizontal input.
                m_Anim.SetFloat("Speed", Mathf.Abs(move));

                // Move the character
                m_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed, m_Rigidbody2D.velocity.y);

                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
                // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
            }
        }


        private void Flip()
        {
            m_FacingRight = !m_FacingRight;

            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

        public void SpeedUp(float speedUpTime = 0, bool holdChange = false)
        {
            if (holdChange == true)
            {
                m_MaxSpeed = defaultMaxSpeed * 6;
            }
            else
            {
                while (speedUpTime < 50f)
                {
                    m_MaxSpeed = defaultMaxSpeed * 6;
                    speedUpTime++;
                }

                m_MaxSpeed = defaultMaxSpeed;
            }
        }

        public void SlowDown(float slowDownTime = 0, bool holdChange = false)
        {
            if (holdChange == true)
            {
                m_MaxSpeed = defaultMaxSpeed / 2;
            }
            else
            {
                while (slowDownTime < 50f)
                {
                    m_MaxSpeed = defaultMaxSpeed / 2;
                    slowDownTime++;
                }

                m_MaxSpeed = defaultMaxSpeed;
            }
        }
    }
}
