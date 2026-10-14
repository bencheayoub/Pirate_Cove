using System;
using UnityEngine;

public class HighScoreSystem : MonoBehaviour
{
    public static event Action<int[]> OnHighScoresUpdated;

    private const int MaxEntries = 5;
    private const string KeyPrefix = "HighScore_";

    private int currentScore;

    private void OnEnable()
    {
        ScoreSystem.OnScoreChanged += HandleScoreChanged;
        GameStateManager.OnGameOver += SaveCurrentScore;
    }

    private void OnDisable()
    {
        ScoreSystem.OnScoreChanged -= HandleScoreChanged;
        GameStateManager.OnGameOver -= SaveCurrentScore;
    }

    private void HandleScoreChanged(int score)
    {
        currentScore = score;
    }

    // Static, so the menu scene can read the scores without this object existing
    public static int[] GetScores()
    {
        int[] scores = new int[MaxEntries];
        for (int i = 0; i < MaxEntries; i++)
            scores[i] = PlayerPrefs.GetInt(KeyPrefix + i, 0);
        return scores;
    }

    private void SaveCurrentScore()
    {
        int[] scores = GetScores();

        if (currentScore > scores[MaxEntries - 1])
        {
            scores[MaxEntries - 1] = currentScore;
            Array.Sort(scores);
            Array.Reverse(scores);

            for (int i = 0; i < MaxEntries; i++)
                PlayerPrefs.SetInt(KeyPrefix + i, scores[i]);
            PlayerPrefs.Save();
        }

        OnHighScoresUpdated?.Invoke(scores);
    }
}