using System.Collections;


using System;

using UnityEngine;



public class CurrencyManager : MonoBehaviour

{

    private int currency = 0;
    // Definirea unui eveniment pentru schimbarea currency-ului 

    public static event Action<int> OnCurrencyChanged;



    // Metodă pentru a adăuga currency 

    public void AddCurrency(int amount)
    {
        currency += amount;
        // Emiterea evenimentului 
        OnCurrencyChanged?.Invoke(currency);

    }



    // Metodă pentru a obține valoarea actuală a currency-ului 

    public int GetCurrency()
    {
        return currency;

    }



    public void RemoveCurrency(int amount)
    {

        currency -= amount;
        OnCurrencyChanged?.Invoke(currency);

    }

}

