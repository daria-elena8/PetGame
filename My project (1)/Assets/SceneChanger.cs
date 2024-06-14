using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public Animator foxAnimator;
    private AnimatorStateInfo savedStateInfo;
    private bool stateSaved = false;

    // Metoda pentru a schimba scena la Main Scene
    public void GoToMainScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    // Metoda pentru a schimba scena la Minigame Scene
    public void GoToMinigameScene()
    {
        SaveAnimatorState();
        SceneManager.LoadScene("Minigame");
    }

    // Metoda pentru a salva starea animatorului
    private void SaveAnimatorState()
    {
        if (foxAnimator != null)
        {
            savedStateInfo = foxAnimator.GetCurrentAnimatorStateInfo(0);
            Debug.Log("Saved State: " + savedStateInfo.fullPathHash);
            stateSaved = true;
        }
    }

    // Metoda pentru a restaura starea animatorului
    private void RestoreAnimatorState()
    {
        if (foxAnimator != null && stateSaved)
        {
            foxAnimator.Play(savedStateInfo.fullPathHash, -1, savedStateInfo.normalizedTime);
            Debug.Log("Restored State: " + savedStateInfo.fullPathHash);
            stateSaved = false;
        }
    }

    // Metoda apelată atunci când un nou scene este încărcat
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "SampleScene")
        {
            RestoreAnimatorState();
        }
    }

    // Abonare la evenimentul de încărcare a scenei
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Dezabonare de la evenimentul de încărcare a scenei
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
