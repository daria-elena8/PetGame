using System.Collections;


using System;

using UnityEngine;



public class CurrencyManager : MonoBehaviour

{
    public static AudioClip moneySound;
    public static AudioSource audioSource;
    private static int currency = 0;
    // Definirea unui eveniment pentru schimbarea currency-ului 

    public static event Action<int> OnCurrencyChanged;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing on CurrencyManager object.");
        }

        if (moneySound == null)
        {
            // Încarcă sunetul
            moneySound = Resources.Load<AudioClip>("Collecting-Money-Coins-G-www.fesliyanstudios.com");
        }
    }
    public static void LoadSound()
    {
       
        if (moneySound == null)
        {
            // Încarcă sunetul
            moneySound = Resources.Load<AudioClip>("Assets/Collecting-Money-Coins-G-www.fesliyanstudios.com.mp3");
        }
    }

    // Metodă pentru a adăuga currency 

    public static void AddCurrency(int amount)
    {
        currency += amount;
        // Emiterea evenimentului 
        OnCurrencyChanged?.Invoke(currency);
        // Redă sunetul
        
        audioSource.PlayOneShot(moneySound);


    }



    // Metodă pentru a obține valoarea actuală a currency-ului 

    public static int GetCurrency()
    {
        return currency;

    }



    public static void RemoveCurrency(int amount)
    {

        currency -= amount;
        OnCurrencyChanged?.Invoke(currency);

    }

}

