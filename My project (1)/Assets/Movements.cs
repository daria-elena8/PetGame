using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movements : MonoBehaviour
{
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private Vector2 initialMousePosition;
    private Vector3 startObjectPosition; // Vector3 pentru a include componenta z
    private float swipeThreshold = 30.0f; // Pragul minim pentru a considera un swipe
    private bool isDragging = false;

    void Update()
    {
        HandleTouchInput();
        HandleMouseInput();
    }

    void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (IsTouchingObject(touch.position))
                    {
                        isDragging = true;
                        startTouchPosition = touch.position;
                        startObjectPosition = transform.position;
                    }
                    break;

                case TouchPhase.Moved:
                    if (isDragging)
                    {
                        Vector2 currentTouchPosition = touch.position;
                        Vector2 touchDelta = (currentTouchPosition - startTouchPosition) / (float)Screen.width;
                        transform.position = new Vector3(startObjectPosition.x + touchDelta.x * 10, startObjectPosition.y, startObjectPosition.z);
                    }
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    isDragging = false;
                    break;
            }
        }
    }

    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (IsTouchingObject(Input.mousePosition))
            {
                isDragging = true;
                initialMousePosition = Input.mousePosition;
                startObjectPosition = transform.position;
            }
        }

        if (Input.GetMouseButton(0) && isDragging)
        {
            Vector2 currentMousePosition = Input.mousePosition;
            Vector2 mouseDelta = (currentMousePosition - initialMousePosition) / (float)Screen.width;
            transform.position = new Vector3(startObjectPosition.x + mouseDelta.x * 10, startObjectPosition.y, startObjectPosition.z);
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }

    bool IsTouchingObject(Vector2 touchPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            return hit.transform == transform;
        }
        return false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject); // Distruge sfera la coliziune
    }
}
