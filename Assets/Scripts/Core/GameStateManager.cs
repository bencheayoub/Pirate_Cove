using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public static event Action OnGameOver;

    private void OnEnable()
    {
        HealthSystem.OnPlayerDied += HandlePlayerDied;
    }

    private void OnDisable()
    {
        HealthSystem.OnPlayerDied -= HandlePlayerDied;
    }

    private void HandlePlayerDied()
    {
        Time.timeScale = 0f;
        OnGameOver?.Invoke();
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}