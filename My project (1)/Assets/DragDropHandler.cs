using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private Vector2 initialPosition;
    private Animator foxAnimator;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        initialPosition = rectTransform.anchoredPosition;
        //GameObject foxObject = GameObject.FindGameObjectWithTag("Fox");
        //if(foxObject != null)
        //    foxAnimator = foxObject.GetComponent<Animator>();

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        Debug.Log("OnBeginDrag triggered");

    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        Debug.Log("OnDrag triggered");
        //foxAnimator.SetTrigger("TrYes");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        Debug.Log("OnEndDrag triggered");

        bool triggerSet = false;

        // Creăm un PointerEventData pentru a face raycast la poziția cursorului
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = eventData.position
        };

        // Obținem toate obiectele sub cursor
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        // Verificăm dacă printre aceste obiecte se află unul cu tag-ul "Fox"
        foreach (RaycastResult result in results)
        {
            Debug.Log("Raycast hit: " + result.gameObject.name);
            if (result.gameObject.CompareTag("Fox"))
            {
                Animator foxAnimator = result.gameObject.GetComponent<Animator>();
                if (foxAnimator != null)
                {
                    foxAnimator.SetTrigger("TrEat");
                    Debug.Log("Trigger 'TrEat' set on " + result.gameObject.name);
                    triggerSet = true;
                }
                break;
            }
        }

        // Fallback: folosim eventData.pointerEnter dacă raycast-ul nu găsește obiectul Fox
        if (!triggerSet && eventData.pointerEnter != null && eventData.pointerEnter.CompareTag("Fox"))
        {
            Animator foxAnimator = eventData.pointerEnter.GetComponent<Animator>();
            if (foxAnimator != null)
            {
                foxAnimator.SetTrigger("TrEat");
                Debug.Log("Fallback trigger 'TrEat' set on " + eventData.pointerEnter.name);
            }
        }

        // Reveniți la poziția inițială
        rectTransform.anchoredPosition = initialPosition;
    }
}
