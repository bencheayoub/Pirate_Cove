using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text timerText;
    [FormerlySerializedAs("highScoresText")]
    [SerializeField] private TMP_Text finalScoreText;
    [SerializeField] private GameObject gameOverPanel;

    private int lastScore;

    private void Start()
    {
        gameOverPanel.SetActive(false);
    }

    private void OnEnable()
    {
        ScoreSystem.OnScoreChanged += UpdateScore;
        HealthSystem.OnHealthChanged += UpdateHealth;
        TimerSystem.OnTimeChanged += UpdateTime;
        GameStateManager.OnGameOver += ShowGameOver;
        HighScoreSystem.OnHighScoresUpdated += ShowFinalScore;
    }

    private void OnDisable()
    {
        ScoreSystem.OnScoreChanged -= UpdateScore;
        HealthSystem.OnHealthChanged -= UpdateHealth;
        TimerSystem.OnTimeChanged -= UpdateTime;
        GameStateManager.OnGameOver -= ShowGameOver;
        HighScoreSystem.OnHighScoresUpdated -= ShowFinalScore;
    }

    private void UpdateScore(int score)
    {
        lastScore = score;
        scoreText.text = "Score: " + score;
    }

    private void UpdateHealth(int health)
    {
        healthText.text = "Health: " + health;
    }

    private void UpdateTime(float seconds)
    {
        int total = Mathf.CeilToInt(seconds);
        timerText.text = (total / 60) + ":" + (total % 60).ToString("00");
    }

    private void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
    }

    private void ShowFinalScore(int[] scores)
    {
        finalScoreText.text =
            "Your score: " + lastScore + "\n" +
            "Best: " + scores[0] + "\n\n" +
            "Press ENTER to restart";
    }
}