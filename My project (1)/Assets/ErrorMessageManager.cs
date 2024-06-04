using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorMessageManager : MonoBehaviour
{
    public Text errorMessageText;

    void Start()
    {
        // Ascunde textul la început
        errorMessageText.gameObject.SetActive(false);
    }

    public void ShowErrorMessage(string message, float duration)
    {
        StartCoroutine(DisplayErrorMessage(message, duration));
    }

    private IEnumerator DisplayErrorMessage(string message, float duration)
    {
        errorMessageText.text = message;
        errorMessageText.gameObject.SetActive(true);
        yield return new WaitForSeconds(duration);
        errorMessageText.gameObject.SetActive(false);
    }
}