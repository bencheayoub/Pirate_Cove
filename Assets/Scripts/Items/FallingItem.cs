using System;
using UnityEngine;

public class FallingItem : MonoBehaviour
{
    public static event Action<ItemData> OnItemCaught;
    public static event Action<ItemData> OnItemMissed;

    private float fallSpeed = 3f;
    private ItemData data;
    private float bottomY;
    private SpriteRenderer myRenderer;
    private SpriteRenderer playerRenderer;

    public void Initialize(ItemData itemData, float speed)
    {
        data = itemData;
        fallSpeed = speed;
        myRenderer = GetComponent<SpriteRenderer>();
        myRenderer.color = data.color;
    }

    private void Start()
    {
        bottomY = -Camera.main.orthographicSize - 1f;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            playerRenderer = player.GetComponent<SpriteRenderer>();
        else
            Debug.LogError("No object with the Player tag found!");
    }

    private void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);

        if (playerRenderer != null && myRenderer.bounds.Intersects(playerRenderer.bounds))
        {
            Debug.Log("Caught: " + data.name);
            OnItemCaught?.Invoke(data);
            Destroy(gameObject);
            return;
        }

        if (transform.position.y < bottomY)
        {
            OnItemMissed?.Invoke(data);
            Destroy(gameObject);
        }
    }
}