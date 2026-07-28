using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Shop Item")]
public class ShopItem : ScriptableObject
{
    public Sprite icon;
    public string title;
    [TextArea] public string description;
    public int cost;
    public Rarity rarity;

    public enum Rarity
    {
        Common,
        Rare,
        Epic,
        Ultimate
    }
}