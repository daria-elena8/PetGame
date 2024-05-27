using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    private Animator mAnimator;

    // Variabile pentru swipe detection
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private float swipeThreshold = 50.0f; // Distanta minima pentru a considera un swipe

    void Start()
    {
        mAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (mAnimator != null)
        {
            AnimatorStateInfo currentState = mAnimator.GetCurrentAnimatorStateInfo(0);

            if (Input.GetMouseButtonDown(0))
            {
                // Inregistreaza pozitia de start a mouse-ului
                startTouchPosition = Input.mousePosition;
            }

            if (Input.GetMouseButtonUp(0))
            {
                // Inregistreaza pozitia de final a mouse-ului
                endTouchPosition = Input.mousePosition;
                DetectSwipe(currentState);
            }
        }
    }

    void DetectSwipe(AnimatorStateInfo currentState)
    {
        Vector2 swipeDelta = endTouchPosition - startTouchPosition;

        if (swipeDelta.magnitude > swipeThreshold)
        {
            if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
            {
                // Swipe pe orizontala
                if (swipeDelta.x > 0)
                {
                    OnSwipeRight(currentState);
                }
                else
                {
                    OnSwipeLeft(currentState);
                }
            }
            else
            {
                // Swipe pe verticala
                if (swipeDelta.y > 0)
                {
                    OnSwipeUp(currentState);
                }
                else
                {
                    OnSwipeDown(currentState);
                }
            }
        }
    }

    void OnSwipeUp(AnimatorStateInfo currentState)
    {
        Debug.Log("Swipe Up");
        // Actiuni pentru swipe in sus
        if (currentState.IsName("Idle"))
        {
            mAnimator.SetTrigger("TrJump");
        }
        else if (currentState.IsName("Sit") || currentState.IsName("Sit_Idle"))
        {
            mAnimator.SetTrigger("TrStand");
        }
    }

    void OnSwipeDown(AnimatorStateInfo currentState)
    {
        Debug.Log("Swipe Down");
        // Actiuni pentru swipe in jos
        if (currentState.IsName("Idle"))
        {
            mAnimator.SetTrigger("TrSit");
        }
        
    }

    void OnSwipeLeft(AnimatorStateInfo currentState)
    {
        Debug.Log("Swipe Left");
        // Actiuni pentru swipe la stanga
    }

    void OnSwipeRight(AnimatorStateInfo currentState)
    {
        Debug.Log("Swipe Right");
        // Actiuni pentru swipe la dreapta
    }
}
