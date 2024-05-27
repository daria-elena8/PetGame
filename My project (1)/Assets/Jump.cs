using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    private Animator mAnimator;

    void Start()
    {
        mAnimator = GetComponent<Animator>();


    }

   
    void Update()
    {
        if (mAnimator != null)
        {
            AnimatorStateInfo currentState = mAnimator.GetCurrentAnimatorStateInfo(0);

            if (Input.GetKeyDown(KeyCode.Space) && currentState.IsName("Idle"))
            {
               
                    mAnimator.SetTrigger("TrJump");
                
            }
            if (Input.GetMouseButtonDown(0) && currentState.IsName("Idle"))
            {
                mAnimator.SetTrigger("TrSit");

              
            }

            if (Input.GetMouseButtonDown(0) && (currentState.IsName("Sit") || currentState.IsName("Sit_Idle")))
            {
                mAnimator.SetTrigger("TrStand");
            }
        }
    }



}