using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int lives = 5;
    public TextMeshProUGUI highScoreText;

    public GameObject mainMenuPanel;
    public GameObject gameUIPanel;
    public GameObject gameOverPanel;
    public static bool AutoStartAfterReload = false;

    private bool gameStarted = false;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Time.timeScale = 0; // dừng game khi ở menu
        UpdateHighScoreUI();
        if (AutoStartAfterReload)
    {
        AutoStartAfterReload = false;
        StartGame();
    }
    else
    {
        mainMenuPanel.SetActive(true);
        gameUIPanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }
    }

    public void StartGame()
    {
        gameStarted = true;

        mainMenuPanel.SetActive(false);
        gameUIPanel.SetActive(true);

        Time.timeScale = 1;
    }

    public void LoseLife()
    {
        lives--;
        UIManager.Instance.UpdateLives(lives);

        if (lives <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        int currentScore = ScoreController.instance.CurrentScore;

        PlayerPrefs.SetInt("LastScore", currentScore);

        int highScore = PlayerPrefs.GetInt("HighScore", 0);

        if (currentScore > highScore)
        {
            PlayerPrefs.SetInt("HighScore", currentScore);
            PlayerPrefs.Save();
        }

        SceneManager.LoadScene("GameOverScene");
    }

    void SaveHighScore()
    {
        int currentScore = ScoreController.instance.CurrentScore;
        int highScore = PlayerPrefs.GetInt("HighScore", 0);

        if (currentScore > highScore)
        {
            PlayerPrefs.SetInt("HighScore", currentScore);
            PlayerPrefs.Save(); // bắt buộc lưu
        }
    }

    public void Restart()
    {
        lives = 5;
        Time.timeScale = 1;

        gameOverPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
        gameUIPanel.SetActive(false);

        UpdateHighScoreUI();
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game"); // để test trong Editor
    }
    void UpdateHighScoreUI()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);

        if (highScoreText != null)
            highScoreText.text = "High Score: " + highScore;
    }
}