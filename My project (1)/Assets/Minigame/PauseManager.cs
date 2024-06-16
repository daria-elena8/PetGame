using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseButton;
    public GameObject pausePanel;
    public Button continueButton;
    public Button restartButton;
    public Button backToMainButton;

    private bool isPaused = false;

    void Start()
    {
        // Asigură-te că panelul de pauză și butonul de pauză sunt inițial ascunse
        pausePanel.SetActive(false);
        pauseButton.SetActive(true);

        // Asociază evenimentele de click ale butoanelor
        continueButton.onClick.AddListener(ContinueGame);
        restartButton.onClick.AddListener(RestartGame);
        backToMainButton.onClick.AddListener(BackToMainScene);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            PauseGame();
        }
    }

    public void ShowPauseButton()
    {
        pauseButton.SetActive(true);
    }

    public void HidePauseButton()
    {
        pauseButton.SetActive(false);
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        HidePauseButton();
    }

    public void ContinueGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        ShowPauseButton();
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        ShowPauseButton();
    }

    public void BackToMainScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
}
