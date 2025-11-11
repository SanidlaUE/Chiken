
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Coins : MonoBehaviour
{
    public static int money;
    public TMP_Text moneyText;

    void Update()
    {
        money = PlayerPrefs.GetInt("Money");
        moneyText.text = money.ToString();
    }
}