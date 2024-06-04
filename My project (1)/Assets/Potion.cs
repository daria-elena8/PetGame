using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Potion : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private Vector2 initialPosition;
    private Animator foxAnimator;

    public Text potionText;
    public CurrencyManager currencyManager;
    public ErrorMessageManager errorMessageManager;

    public HungryBar hungryBar;
    public Happiness happinessBar;
    public FitnessBar fitnessBar;

    private int potionCount = 0;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        initialPosition = rectTransform.anchoredPosition;
        //GameObject foxObject = GameObject.FindGameObjectWithTag("Fox");
        //if(foxObject != null)
        //    foxAnimator = foxObject.GetComponent<Animator>();
        if (hungryBar == null)
        {
            hungryBar = FindObjectOfType<HungryBar>();
        }
        if (happinessBar == null)
        {
            happinessBar = FindObjectOfType<Happiness>();
        }
        if (fitnessBar == null)
        {
            fitnessBar = FindObjectOfType<FitnessBar>();
        }

    }
    private void Start()
    {
        UpdatePotionText();        
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
                    if (potionCount > 0)
                    {
                        foxAnimator.SetTrigger("TrPotion");
                        Debug.Log("Trigger 'TrPotion' set on " + result.gameObject.name);
                        RemovePotion();
                        triggerSet = true;
                        hungryBar.MaxHungry();
                        happinessBar.MaxHappiness();
                        fitnessBar.MaxFitness();
                    }
                    else
                    {
                        Debug.Log("No potions left to use!");
                        errorMessageManager.ShowErrorMessage("No potions left to use!", 2f);
                    }

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
                if (potionCount > 0)
                {
                    foxAnimator.SetTrigger("TrPotion");
                    Debug.Log("Fallback trigger 'TrPotion' set on " + eventData.pointerEnter.name);
                    RemovePotion();
                }
                else
                {
                    Debug.Log("No potions left to use!");
                    errorMessageManager.ShowErrorMessage("No potions left to use!", 2f);
                }
            }
        }

        // Reveniți la poziția inițială
        rectTransform.anchoredPosition = initialPosition;
    }

    public void AddPotion()
    {
        if (currencyManager.GetCurrency() >= 30)
        {
            potionCount++;
            currencyManager.RemoveCurrency(30);
            UpdatePotionText();
        }
        else
        {
            Debug.Log("Not enough currency to buy a potion!");
            errorMessageManager.ShowErrorMessage("Not enough currency to buy a potion!", 2f);
        }
    }

    private void RemovePotion()
    {
        if (potionCount > 0)
        {
            potionCount--;
            UpdatePotionText();
        }
        else
        {
            Debug.Log("No potions left to use!");
            errorMessageManager.ShowErrorMessage("No potions left to use!", 2f);
        }
    }

    private void UpdatePotionText()
    {
        potionText.text = "" + potionCount;
    }

}
