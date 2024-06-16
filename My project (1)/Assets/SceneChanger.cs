using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneChanger : MonoBehaviour
{
    public Animator foxAnimator;
    private AnimatorStateInfo savedStateInfo;
    private bool stateSaved = false;


    void Start()
    {
        if (foxAnimator == null)
        {
            Debug.LogError("Fox Animator not set in SceneChanger.");
        }
      
    }

    public void GoToMainScene()
    {
        // Salvăm starea animatorului înainte de a schimba scena
        if (foxAnimator != null)
        {
            savedStateInfo = foxAnimator.GetCurrentAnimatorStateInfo(0);
            stateSaved = true;
        }
        SceneManager.LoadScene(1);
    }

    public void GoToMinigameScene()
    {
        SceneManager.LoadScene(3);
    }

    void OnEnable()
    {
        // Restaurăm starea animatorului când ne întoarcem la scena principală
        if (stateSaved && foxAnimator != null)
        {
            foxAnimator.Play(savedStateInfo.fullPathHash, -1, savedStateInfo.normalizedTime);
        }
    }
}
