using System;
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
        public ErrorMessageManager errorMessageManager;
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
            errorMessageManager.ShowErrorMessage("Not enough currency to buy a potion!", 2f);
        }
        }
    
        public void UsePotion()
        {
            if (potionCount > 0)
            {
                potionCount--;
                UpdatePotionText();
            }
            else
            {
            errorMessageManager.ShowErrorMessage("No potions left to use!", 2f);
        }

        }
    
         private void UpdatePotionText()
        {
            potionText.text = "" + potionCount;
        }
}







