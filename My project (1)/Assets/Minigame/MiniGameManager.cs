using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MiniGameManager : MonoBehaviour
{

    public GameObject startButton;
    public GameObject backButton;
    public Button restartButton;
    public GameObject gameOverPanel;
    public Text scoreText;
    public Spawner cubeSpawner;
    public GameObject player;
    public Text coinsEarnedText;
    

    private int score = 0;
    private bool gameStarted = false;

    void Start()
    {
        // Inițial, dezactivează spawn-ul cuburilor și ascunde panoul de game over
        cubeSpawner.enabled = false;
        gameOverPanel.SetActive(false);
        startButton.SetActive(true);
        Time.timeScale = 0; // Oprește timpul la început

        // Asociază evenimentul de click al butonului de restart
        restartButton.onClick.AddListener(RestartGame);
        FindObjectOfType<PauseManager>().ShowPauseButton();
    }

    void Update()
    {
        if (gameStarted)
        {
            // Crește punctajul pe baza timpului supraviețuit
            score += (int)(Time.deltaTime * 20);
            scoreText.text = "Score: " + score;
        }
    }

    public void StartGame()
    {
        gameStarted = true;
        score = 0; // Resetează scorul
        scoreText.text = "Score: " + score;
        cubeSpawner.enabled = true;
        startButton.SetActive(false);
        backButton.SetActive(false);
        gameOverPanel.SetActive(false);
        Time.timeScale = 1; // Pornește timpul
    }

   

    public void GameOver()
    {
        gameStarted = false;
        cubeSpawner.enabled = false;

        if (score > 200)
        {
            CurrencyManager.AddCurrency((int)score/10);
            coinsEarnedText.text = "Coins Earned: " + score/10;
        }
        else
        {
            coinsEarnedText.text = "Coins Earned: 0";
        }

        gameOverPanel.SetActive(true);
        Time.timeScale = 0; // Oprește timpul
    }

    public void RestartGame()
    {
        // Reîncarcă scena curentă
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
       
    }
}
