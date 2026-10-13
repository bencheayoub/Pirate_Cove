using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private GameObject gameOverPanel;

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
    }

    private void OnDisable()
    {
        ScoreSystem.OnScoreChanged -= UpdateScore;
        HealthSystem.OnHealthChanged -= UpdateHealth;
        TimerSystem.OnTimeChanged -= UpdateTime;
        GameStateManager.OnGameOver -= ShowGameOver;
    }

    private void UpdateScore(int score)
    {
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
}