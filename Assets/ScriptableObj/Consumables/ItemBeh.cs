using UnityEngine;

[CreateAssetMenu(fileName = "ItemBeh", menuName = "Scriptable Objects/ItemBeh")]
public class ItemBeh : ScriptableObject
{
    public int id;
    public Sprite itemImage;
    public int price;
    public int itemValue;
    public ConsumableType consumableType;
}
