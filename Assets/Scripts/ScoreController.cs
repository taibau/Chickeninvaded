using System;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField] TMP_Text textScore;

    public static ScoreController instance;
    public int Score { get; private set; }
    public event Action<int> OnScoreChanged;

    private void Awake()
    {
        instance = this;
    }

    public void GetScore(int score)
    {
        Score += score;
        textScore.text = "Score: " + Score.ToString();
        OnScoreChanged?.Invoke(Score);
    }
}
