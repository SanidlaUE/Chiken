using UnityEngine;

[CreateAssetMenu(fileName = "FoodType", menuName = "Scriptable Objects/FoodType")]
public class FoodType : ScriptableObject
{
    public int satiety;
    public int price;
    public Sprite sprite;
}