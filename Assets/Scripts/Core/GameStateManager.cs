using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public static event Action OnGameOver;

    private bool isGameOver;

    private void OnEnable()
    {
        HealthSystem.OnPlayerDied += EndGame;
        TimerSystem.OnTimeUp += EndGame;
    }

    private void OnDisable()
    {
        HealthSystem.OnPlayerDied -= EndGame;
        TimerSystem.OnTimeUp -= EndGame;
    }

    private void Update()
    {
        if (!isGameOver) return;

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            Restart();
    }

    private void EndGame()
    {
        if (isGameOver) return;

        isGameOver = true;
        Time.timeScale = 0f;
        OnGameOver?.Invoke();
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}