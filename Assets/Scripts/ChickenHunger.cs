using System;
using UnityEngine;

public class ChickenHunger : MonoBehaviour
{
    public int hunger;
    public int  maxHunger;
    public static event Action OnChickenNotHunger;
    public MessageBox dialogBox;

    public void Feed(FoodType type)
    {
        if (hunger >= maxHunger)
        {
            hunger = maxHunger;
            OnChickenNotHunger?.Invoke();
            dialogBox.SendMessage("Im not hungry");
        }
    }

    public void OnEnable()
    {
        if(!PlayerPrefs.HasKey("hunger"))
        {
            PlayerPrefs.SetInt("hunger", maxHunger);
            hunger = maxHunger;
        }
        else
        {
            hunger = PlayerPrefs.GetInt("hunger");
        }
    }

    public void OnDisable()
    {
        PlayerPrefs.SetInt("hunger", hunger);
    }
}
