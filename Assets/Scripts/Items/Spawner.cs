using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private FallingItem itemPrefab;
    [SerializeField] private ItemData[] itemTypes;
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private float fallSpeed = 3f;

    private float timer;
    private float spawnY;
    private float halfWidth;

    private void Start()
    {
        spawnY = Camera.main.orthographicSize + 1f;
        halfWidth = Camera.main.orthographicSize * Camera.main.aspect - 0.5f;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;
            Spawn();
        }
    }

    private void Spawn()
    {
        float x = Random.Range(-halfWidth, halfWidth);
        FallingItem item = Instantiate(itemPrefab, new Vector3(x, spawnY, 0f), Quaternion.identity);
        item.Initialize(itemTypes[Random.Range(0, itemTypes.Length)], fallSpeed);
    }
}