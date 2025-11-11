using UnityEngine;

[CreateAssetMenu(fileName = "SkinType", menuName = "Scriptable Objects/SkinType")]

public class SkinType : ScriptableObject
{
    public int id;
    public int price;
    public Sprite sprite;
    public RuntimeAnimatorController animatorController;
    public bool isPurchased = false;
    public bool isEquipped = false; 
}
