using System;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    public static event Action<int> OnScoreChanged;

    private int score;

    private void OnEnable()
    {
        FallingItem.OnItemCaught += HandleItemCaught;
    }

    private void OnDisable()
    {
        FallingItem.OnItemCaught -= HandleItemCaught;
    }

    private void Start()
    {
        OnScoreChanged?.Invoke(score);
    }

    private void HandleItemCaught(ItemData item)
    {
        if (item.isBomb) return;

        score += item.points;
        OnScoreChanged?.Invoke(score);
        Debug.Log("Score: " + score);
    }
}