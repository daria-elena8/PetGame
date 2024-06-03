using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PotionManager : MonoBehaviour
{
    public Text potionText;
    public CurrencyManager currencyManager;
    public ErrorMessageManager errorMessageManager; // Adăugăm referința la ErrorMessageManager
    private int potionCount = 0;

    void Start()
    {
        UpdatePotionText();
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
            errorMessageManager.ShowErrorMessage("Not enough currency to buy a potion!", 2f); // Afișează mesajul de eroare
        }
    }

    public void RemovePotion()
    {
        if (potionCount > 0)
        {
            potionCount--;
            UpdatePotionText();
        }
        else
        {
            Debug.Log("No potions left to use!");
            errorMessageManager.ShowErrorMessage("No potions left to use!", 2f); // Afișează mesajul de eroare
        }
    }

    private void UpdatePotionText()
    {
        potionText.text = "" + potionCount;
    }
}
