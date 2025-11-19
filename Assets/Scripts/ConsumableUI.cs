using UnityEngine;
using UnityEngine.UI;

public class ConsumableUI : MonoBehaviour
{
    public ConsumableType consumableType;
    public Image _default;
    public Sprite food1;
    public Sprite food2;
    public Sprite food3;
    public Sprite toy1;
    public Sprite toy2;
    public Sprite toy3;
    public Sprite cloth1;
    public Sprite cloth2;
    public Sprite cloth3;

    public void OnEnable()
    {
        switch (consumableType)
        {
            case ConsumableType.Food:
            {
                switch (PlayerPrefs.GetInt("SelectedFoodMight"))
                {
                    case 10:
                    {
                        _default.sprite = food1;
                        break;
                    }
                    case 20:
                    {
                        _default.sprite = food2;
                        break;
                    }
                    case 50:
                    {
                        _default.sprite = food3;
                        break;
                    }
                }

                break;
            }
            case ConsumableType.Toy:
            {
                switch (PlayerPrefs.GetInt("SelectedToyMight"))
                {
                    case 10:
                    {
                        _default.sprite = toy1;
                        break;
                    }
                    case 20:
                    {
                        _default.sprite = toy2;
                        break;
                    }
                    case 50:
                    {
                        _default.sprite = toy3;
                        break;
                    }
                }

                break;
            }
            case ConsumableType.Cloth:
            {
                switch (PlayerPrefs.GetInt("SelectedClothMight"))
                {
                    case 10:
                    {
                        _default.sprite = cloth1;
                        break;
                    }
                    case 20:
                    {
                        _default.sprite = cloth2;
                        break;
                    }
                    case 50:
                    {
                        _default.sprite = cloth3;
                        break;
                    }
                }

                break;
            }

        }
    }
}