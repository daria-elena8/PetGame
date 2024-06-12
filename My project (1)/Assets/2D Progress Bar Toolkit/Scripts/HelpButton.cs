using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpButton : MonoBehaviour
{
    public GameObject helpPanel;
    // Start is called before the first frame update
    void OpenPanel()
    {
        if (helpPanel != null)
        {
            helpPanel.SetActive(true);
        }
    }

   
}
