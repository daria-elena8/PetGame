using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    private int currency = 0; // Exemplu de suma inițială

    public static event Action<int> OnCurrencyChanged;

    public void AddCurrency(int amount)
    {
        currency += amount;
        OnCurrencyChanged?.Invoke(currency);
    }

    public void RemoveCurrency(int amount)
    {
        currency -= amount;
        OnCurrencyChanged?.Invoke(currency);
    }

    public int GetCurrency()
    {
        return currency;
    }
}
