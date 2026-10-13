using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private GameObject gameOverPanel;

    private void OnEnable()
    {
        ScoreSystem.OnScoreChanged += UpdateScore;
        HealthSystem.OnHealthChanged += UpdateHealth;
        GameStateManager.OnGameOver += ShowGameOver;
    }

    private void OnDisable()
    {
        ScoreSystem.OnScoreChanged -= UpdateScore;
        HealthSystem.OnHealthChanged -= UpdateHealth;
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

    private void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
    }
}