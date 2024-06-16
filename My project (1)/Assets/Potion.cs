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
    public ErrorMessageManager errorMessageManager;

    public HungryBar hungryBar;
    public Happiness happinessBar;
    public FitnessBar fitnessBar;

    private int potionCount = 0;


    public AudioClip potionSound; //  variabila pentru sunetul poțiunii
    private AudioSource audioSource; //  o variabilă pentru AudioSource
    public AudioClip buyPotionSound;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        initialPosition = rectTransform.anchoredPosition;
        audioSource = GetComponent<AudioSource>();
        LoadPotionSound();
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
                        PlayPotionSound();
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
                    PlayPotionSound();
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
    private void PlayPotionSound()
    {
        if (audioSource != null && potionSound != null)
        {
            audioSource.PlayOneShot(potionSound);
        }
        else
        {
            Debug.LogError("AudioSource or potionSound is not set.");
        }
    }
    private void LoadPotionSound()
    {
        if (potionSound == null)
        {
            // Încarcă sunetul din folderul Resources
            potionSound = Resources.Load<AudioClip>("game-bonus-144751.mp3");
            if (potionSound == null)
            {
                Debug.LogError("Potion sound not found in Resources folder");
            }
        }
        if (buyPotionSound == null)
        {
            // Încarcă sunetul de cumpărare din folderul Resources
            buyPotionSound = Resources.Load<AudioClip>("item-equip-6904.mp3"); 
            if (buyPotionSound == null)
            {
                Debug.LogError("Buy potion sound not found in Resources folder");
            }
        }
    }
    public void AddPotion()
    {
        if (CurrencyManager.GetCurrency() >= 30)
        {
            potionCount++;
            CurrencyManager.RemoveCurrency(30);
            UpdatePotionText();
            PlayBuyPotionSound();
        }
        else
        {
            Debug.Log("Not enough currency to buy a potion!");
            errorMessageManager.ShowErrorMessage("Not enough currency to buy a potion!", 2f);
        }
    }

    private void PlayBuyPotionSound()
    {
        if (audioSource != null && buyPotionSound != null)
        {
            audioSource.PlayOneShot(buyPotionSound);
        }
        else
        {
            Debug.LogError("AudioSource or buyPotionSound is not set.");
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
