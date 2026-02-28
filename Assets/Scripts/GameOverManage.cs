using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    void Start()
    {
        int score = PlayerPrefs.GetInt("LastScore", 0);
        int highScore = PlayerPrefs.GetInt("HighScore", 0);

        scoreText.text = "Score: " + score;
        highScoreText.text = "High Score: " + highScore;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}