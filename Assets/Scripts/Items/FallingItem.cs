using System;
using UnityEngine;

public class FallingItem : MonoBehaviour
{
    public static event Action<ItemData> OnItemCaught;
    public static event Action<ItemData> OnItemMissed;

    private float fallSpeed = 3f;
    private ItemData data;
    private float bottomY;

    public void Initialize(ItemData itemData, float speed)
    {
        data = itemData;
        fallSpeed = speed;
        GetComponent<SpriteRenderer>().color = data.color;
    }

    private void Start()
    {
        bottomY = -Camera.main.orthographicSize - 1f;
    }

    private void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);

        if (transform.position.y < bottomY)
        {
            OnItemMissed?.Invoke(data);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        OnItemCaught?.Invoke(data);
        Destroy(gameObject);
    }
}