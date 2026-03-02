using UnityEngine;
using UnityEngine.SceneManagement;

public class WinUiController : MonoBehaviour
{
    public static WinUiController Instance;
    [SerializeField] private GameObject winPanel;

    private void Awake()
    {
     Instance = this;
    if(winPanel != null) winPanel.SetActive(false);
    }

    public void ShowWinPanel()
    {
        if (winPanel != null) winPanel.SetActive(true);
        Time.timeScale = 0f; // Pause the game
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        GameManager.AutoStartAfterReload = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void GoToHome()
    {
        Time.timeScale = 1f; // Resume the game
        SceneManager.LoadScene("SampleScene");
    }
}
