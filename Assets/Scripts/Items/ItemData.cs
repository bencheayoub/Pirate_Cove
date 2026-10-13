using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Catcher/Item Data")]
public class ItemData : ScriptableObject
{
    public int points = 10;
    public int damage = 0;
    public bool isBomb = false;
    public Color color = Color.white;
    public Sprite sprite;
}