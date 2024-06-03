using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyDisplay : MonoBehaviour
{
    public Text currencyText;
    private CurrencyManager currencyManager;

    void Start()
    {
        // Găsește CurrencyManager-ul în scenă
        currencyManager = FindObjectOfType<CurrencyManager>();

        // Inițializare text
        UpdateCurrencyDisplay(currencyManager.GetCurrency());
    }

    void OnEnable()
    {
        // Subscribe la evenimentul de actualizare al currency-ului
        CurrencyManager.OnCurrencyChanged += UpdateCurrencyDisplay;
    }

    void OnDisable()
    {
        // Dezabonare de la eveniment
        CurrencyManager.OnCurrencyChanged -= UpdateCurrencyDisplay;
    }

    // Metodă pentru a actualiza textul
    void UpdateCurrencyDisplay(int newCurrency)
    {
        currencyText.text = "" + newCurrency;
    }
}
