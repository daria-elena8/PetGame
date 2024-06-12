using UnityEngine;
using UnityEngine.UI;

public class InstructionsManager : MonoBehaviour
{
    public GameObject instructionsPanel;

    void Start()
    {
        instructionsPanel.SetActive(false);
    }

    public void ShowInstructions()
    {
        Debug.Log("Button clicked");
        instructionsPanel.SetActive(true);
        
    }   

    public void HideInstructions()
    {
        instructionsPanel.SetActive(false);
    }
}
