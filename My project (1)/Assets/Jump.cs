using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    private Animator mAnimator;
    private Collider2D foxCollider;

    // Variabile pentru swipe detection
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private float swipeThreshold = 50.0f; // Distanta minima pentru a considera un swipe
    private bool jumpType = false;
    private bool isSwipeValid = false;
    public Happiness happinessBar;
    public FitnessBar fitnessBar;
    public HungryBar hungryBar;
    public static string foxAnimatorState;
    void Start()
    {
        mAnimator = GetComponent<Animator>();
        foxCollider = GetComponent<Collider2D>();

        if (foxCollider == null)
        {
            Debug.LogError("No Collider2D component found on the Fox object.");
        }
        if (happinessBar == null)
        {
            happinessBar = FindObjectOfType<Happiness>();
        }
        if (fitnessBar == null)
        {
            fitnessBar = FindObjectOfType<FitnessBar>();
        }
        if (hungryBar == null)
        {
            hungryBar = FindObjectOfType<HungryBar>();
        }
        RestoreAnimatorState();
    }

    void Update()
    {
        // Logica de swipe este acum în metodele OnMouseDown și OnMouseUp
    }

    void OnMouseDown()
    {
        // Inregistreaza pozitia de start a mouse-ului
        startTouchPosition = Input.mousePosition;
        Debug.Log("Mouse button down at: " + startTouchPosition);

        // Verifica daca pozitia de start este pe collider-ul obiectului Fox
        if (IsPointedOverCollider(startTouchPosition))
        {
            isSwipeValid = true;
            Debug.Log("Swipe started on Fox object.");
        }
        else
        {
            isSwipeValid = false;
            Debug.Log("Swipe did not start on Fox object.");
        }
    }

    void OnMouseUp()
    {
        if (isSwipeValid)
        {
            // Inregistreaza pozitia de final a mouse-ului
            endTouchPosition = Input.mousePosition;
            Debug.Log("Mouse button up at: " + endTouchPosition);

            DetectSwipe();
            isSwipeValid = false; // Resetam validitatea swipe-ului
        }
    }

    bool IsPointedOverCollider(Vector2 screenPosition)
    {
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(screenPosition);
        Debug.Log("World point: " + worldPoint);
        bool isOver = foxCollider.OverlapPoint(worldPoint);
        Debug.Log("Is over collider: " + isOver);
        return isOver;
    }

    void DetectSwipe()
    {
        Vector2 swipeDelta = endTouchPosition - startTouchPosition;
        Debug.Log("Swipe delta: " + swipeDelta);

        if (swipeDelta.magnitude > swipeThreshold)
        {
            AnimatorStateInfo currentState = mAnimator.GetCurrentAnimatorStateInfo(0);

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
        else
        {
            Debug.Log("Swipe delta too small: " + swipeDelta.magnitude);
        }
    }

    void OnSwipeUp(AnimatorStateInfo currentState)
    {
        Debug.Log("Swipe Up");
        // Actiuni pentru swipe in sus
        if (currentState.IsName("Idle"))
        {
            if (jumpType == false)
            {
                mAnimator.SetTrigger("TrJump");
                jumpType = true;
                CurrencyManager.AddCurrency(10);
               
                happinessBar.IncreaseHappiness();
                fitnessBar.IncreaseFitness();
                hungryBar.DecreaseHungry();

             
            }
            else
            {
                mAnimator.SetTrigger("TrJump2");
                jumpType = false;
                CurrencyManager.AddCurrency(2);
                happinessBar.IncreaseHappiness();
                fitnessBar.IncreaseFitness();
                hungryBar.DecreaseHungry();
            }
        }
        else if (currentState.IsName("Sit") || currentState.IsName("Sit_Idle"))
        {
            mAnimator.SetTrigger("TrStand");
            happinessBar.IncreaseHappiness();
            
        }
    }

    void OnSwipeDown(AnimatorStateInfo currentState)
    {
        Debug.Log("Swipe Down");
        // Actiuni pentru swipe in jos
        if (currentState.IsName("Idle"))
        {
            mAnimator.SetTrigger("TrSit");
            CurrencyManager.AddCurrency(5);
            happinessBar.IncreaseHappiness();
           
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
    // Salvează starea animatorului
    public static void SaveAnimatorState(Animator animator)
    {
        if (animator != null)
        {
            var stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            foxAnimatorState = stateInfo.IsName("Idle") ? "Idle" : stateInfo.IsName("Sit") ? "Sit" : "";
        }
    }

    // Restaurează starea animatorului
    private void RestoreAnimatorState()
    {
        if (mAnimator != null && !string.IsNullOrEmpty(foxAnimatorState))
        {
            if (foxAnimatorState == "Idle")
            {
                mAnimator.Play("Idle", 0, 0);
            }
            else if (foxAnimatorState == "Sit")
            {
                mAnimator.Play("Sit", 0, 0);
            }
        }
    }
}
