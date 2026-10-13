using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private FallingItem itemPrefab;
    [SerializeField] private ItemData[] itemTypes;
    [SerializeField] private float startInterval = 1f;
    [SerializeField] private float startFallSpeed = 3f;
    [SerializeField] private float minInterval = 0.3f;
    [SerializeField] private float maxFallSpeed = 9f;

    private float currentInterval;
    private float currentSpeed;
    private float timer;
    private float spawnY;
    private float halfWidth;

    private void OnEnable()
    {
        DifficultyManager.OnDifficultyChanged += HandleDifficultyChanged;
    }

    private void OnDisable()
    {
        DifficultyManager.OnDifficultyChanged -= HandleDifficultyChanged;
    }

    private void Start()
    {
        currentInterval = startInterval;
        currentSpeed = startFallSpeed;
        spawnY = Camera.main.orthographicSize + 1f;
        halfWidth = Camera.main.orthographicSize * Camera.main.aspect - 0.5f;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= currentInterval)
        {
            timer = 0f;
            Spawn();
        }
    }

    private void HandleDifficultyChanged(int level)
    {
        currentInterval = Mathf.Max(minInterval, startInterval - (level - 1) * 0.1f);
        currentSpeed = Mathf.Min(maxFallSpeed, startFallSpeed + (level - 1) * 0.6f);
    }

    private void Spawn()
    {
        float x = Random.Range(-halfWidth, halfWidth);
        FallingItem item = Instantiate(itemPrefab, new Vector3(x, spawnY, 0f), Quaternion.identity);
        item.Initialize(itemTypes[Random.Range(0, itemTypes.Length)], currentSpeed);
    }
}