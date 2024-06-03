using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PotionButton : MonoBehaviour
{
    public PotionManager potionManager;

    public void OnPotionButtonPressed()
    {
        Debug.Log("Potion button pressed");
        potionManager.AddPotion();
    }
}
