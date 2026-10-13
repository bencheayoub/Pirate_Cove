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
    private Collider2D catchZone;

    public void Initialize(ItemData itemData, float speed)
    {
        data = itemData;
        fallSpeed = speed;
        myRenderer = GetComponent<SpriteRenderer>();
        myRenderer.sprite = data.sprite;
        myRenderer.color = Color.white;
    }

    private void Start()
    {
        bottomY = -Camera.main.orthographicSize - 1f;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Transform zone = player.transform.Find("CatchZone");
            if (zone != null) catchZone = zone.GetComponent<Collider2D>();
        }

        if (catchZone == null)
            Debug.LogError("CatchZone not found under Player!");
    }

    private void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);

        if (catchZone != null && myRenderer.bounds.Intersects(catchZone.bounds))
        {
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