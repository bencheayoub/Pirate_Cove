using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public static event Action<int> OnHealthChanged;
    public static event Action OnPlayerDied;

    [SerializeField] private int maxHealth = 3;

    private int health;
    private bool isDead;

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
        health = maxHealth;
        OnHealthChanged?.Invoke(health);
    }

    private void HandleItemCaught(ItemData item)
    {
        if (isDead || item.damage <= 0) return;

        health = Mathf.Max(0, health - item.damage);
        OnHealthChanged?.Invoke(health);
        Debug.Log("Health: " + health);

        if (health == 0)
        {
            isDead = true;
            OnPlayerDied?.Invoke();
            Debug.Log("Player died");
        }
    }
}