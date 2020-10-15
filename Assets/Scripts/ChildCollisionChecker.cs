//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ChildCollisionChecker : MonoBehaviour
//{
//    public GameObject parent;

//    private void OnTriggerStay2D(Collider2D col)
//    {
//        if (col.gameObject.tag == "Ground")
//        {
//            parent.GetComponent<PlayerControls>().m_Grounded = true;
//            parent.GetComponent<PlayerControls>().m_Jump = false;
//            print("GROUNDED");
//        }
//        else
//        {
//            parent.GetComponent<PlayerControls>().m_Grounded = false;
//        }

//        parent.GetComponent<PlayerControls>().m_Anim.SetBool("Jump", parent.GetComponent<PlayerControls>().m_Grounded);
//        parent.GetComponent<PlayerControls>().m_Anim.SetBool("Ground", !parent.GetComponent<PlayerControls>().m_Grounded);
//    }
//}
