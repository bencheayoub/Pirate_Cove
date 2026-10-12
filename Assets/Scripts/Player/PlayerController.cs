using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 8f;

    private float minX;
    private float maxX;

    private void Start()
    {
        // Work out the screen edges so the basket can't leave the screen
        float halfBasketWidth = transform.localScale.x / 2f;
        float halfScreenWidth = Camera.main.orthographicSize * Camera.main.aspect;

        minX = -halfScreenWidth + halfBasketWidth;
        maxX = halfScreenWidth - halfBasketWidth;
    }

    private void Update()
    {
        float input = Input.GetAxisRaw("Horizontal"); // A/D or Left/Right arrows

        Vector3 pos = transform.position;
        pos.x += input * moveSpeed * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        transform.position = pos;
    }
}