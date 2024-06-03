using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private Vector2 initialPosition;

    public PotionManager potionManager;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        initialPosition = rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        if (eventData.pointerEnter != null && eventData.pointerEnter.CompareTag("Fox"))
        {
            Animator foxAnimator = eventData.pointerEnter.GetComponent<Animator>();
            if (foxAnimator != null)
            {
                foxAnimator.SetTrigger("TrEat");

                // !!!!!!!!!!!!Asta s-ar putea sa nu fie bine
                // sa se declanseze pentru orice drag 
                potionManager.UsePotion();

            }
        }
        rectTransform.anchoredPosition = initialPosition;
    }
}
