using System;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public static event Action<int> OnDifficultyChanged;

    [SerializeField] private float secondsPerLevel = 15f;

    private float timer;
    private int level = 1;

    private void Start()
    {
        Debug.Log("DifficultyManager started");
        OnDifficultyChanged?.Invoke(level);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= secondsPerLevel)
        {
            timer = 0f;
            level++;
            OnDifficultyChanged?.Invoke(level);
            Debug.Log("Level " + level);
        }
    }
}