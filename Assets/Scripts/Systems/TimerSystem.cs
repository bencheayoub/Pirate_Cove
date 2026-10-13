using System;
using UnityEngine;

public class TimerSystem : MonoBehaviour
{
    public static event Action<float> OnTimeChanged;
    public static event Action OnTimeUp;

    [SerializeField] private float startTime = 90f;
    [SerializeField] private float catchBonus = 2f;
    [SerializeField] private float missPenalty = 1f;

    private float timeLeft;
    private bool finished;

    private void OnEnable()
    {
        FallingItem.OnItemCaught += HandleItemCaught;
        FallingItem.OnItemMissed += HandleItemMissed;
    }

    private void OnDisable()
    {
        FallingItem.OnItemCaught -= HandleItemCaught;
        FallingItem.OnItemMissed -= HandleItemMissed;
    }

    private void Start()
    {
        timeLeft = startTime;
        OnTimeChanged?.Invoke(timeLeft);
    }

    private void Update()
    {
        if (finished) return;

        timeLeft -= Time.deltaTime;
        CheckTimeUp();
        OnTimeChanged?.Invoke(Mathf.Max(0f, timeLeft));
    }

    private void HandleItemCaught(ItemData item)
    {
        if (finished || item.isBomb) return;
        timeLeft += catchBonus;
    }

    private void HandleItemMissed(ItemData item)
    {
        if (finished || item.isBomb) return;
        timeLeft -= missPenalty;
        CheckTimeUp();
    }

    private void CheckTimeUp()
    {
        if (timeLeft > 0f) return;

        timeLeft = 0f;
        finished = true;
        OnTimeChanged?.Invoke(0f);
        OnTimeUp?.Invoke();
        Debug.Log("Time is up");
    }
}