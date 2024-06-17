using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MiniGameManager : MonoBehaviour
{

    public GameObject startButton;
    public Button backToMainButton;
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
        backToMainButton.onClick.AddListener(BackToMainScene);
        // Asociază evenimentul de click al butonului de restart
        restartButton.onClick.AddListener(RestartGame);
        FindObjectOfType<PauseManager>().ShowPauseButton();
       

    }

    void Update()
    {
        if (gameStarted)
        {
            // Crește punctajul pe baza timpului supraviețuit
            score += (int)(Time.deltaTime * 100);
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
        //backButton.SetActive(false);
        gameOverPanel.SetActive(false);
        backToMainButton.gameObject.SetActive(false);
        Time.timeScale = 1; // Pornește timpul
    }

   

    public void GameOver()
    {
        //player.OnCollisionEnter();
        gameStarted = false;
        cubeSpawner.enabled = false;
      
        if (score > 20)
        {
            CurrencyManager.AddCurrency2((int)score);
            coinsEarnedText.text = "Coins Earned: " + score;
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMainScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
        //var sceneChanger = FindObjectOfType<SceneChanger>();
        //if (sceneChanger != null)
        //{
        //    //sceneChanger.GoToMainScene();
        //    SceneManager.LoadScene(1);
        //}
        //else
        //{
        //    Debug.LogError("SceneChanger not found in the scene.");
        //}
    }
}
