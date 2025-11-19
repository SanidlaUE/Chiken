using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ConsumableShopManager : MonoBehaviour
{
    [Header("UI References")] public Button getButton;
    public Button selectButton;
    public Button backButton;
    public TMP_Text priceText;
    public TMP_Text moneyText;

    [Header("Consumable Panels")] public ConsumablePanel[] consumablePanels;

    public ConsumablePanel selectedConsumable;

    [Header("Drag and Drop References")] public DragItemManager dragItemManager;

    private void Start()
    {
        InitializeDefaultConsumables();

        getButton.onClick.AddListener(OnGetButtonClick);
        selectButton.onClick.AddListener(OnSelectButtonClick);
        backButton.onClick.AddListener(OnBackButtonClick);

        foreach (var panel in consumablePanels)
        {
            panel.Initialize(this);
        }

        if (consumablePanels.Length > 0 && selectedConsumable == null)
        {
            selectedConsumable = consumablePanels[0];
        }

        if (Money.Instance != null)
        {
            Money.Instance.OnMoneyChanged += OnMoneyChanged;
        }

        UpdateUI();
    }

    private void OnMoneyChanged(int newMoney)
    {
        UpdateUI();
    }

    private void InitializeDefaultConsumables()
    {
        if (PlayerPrefs.GetInt("food1_get", 0) == 0)
        {
            PlayerPrefs.SetInt("food1_get", 1);
            PlayerPrefs.SetInt("food1_equip", 1);
            PlayerPrefs.SetInt("SelectedFoodConsumable", 1);
            PlayerPrefs.SetInt("SelectedFoodMight", 10);
        }

        if (PlayerPrefs.GetInt("toy1_get", 0) == 0)
        {
            PlayerPrefs.SetInt("toy1_get", 1);
            PlayerPrefs.SetInt("toy1_equip", 1);
            PlayerPrefs.SetInt("SelectedToyConsumable", 1);
            PlayerPrefs.SetInt("SelectedToyMight", 10);
        }

        if (PlayerPrefs.GetInt("cloth1_get", 0) == 0)
        {
            PlayerPrefs.SetInt("cloth1_get", 1);
            PlayerPrefs.SetInt("cloth1_equip", 1);
            PlayerPrefs.SetInt("SelectedClothConsumable", 1);
            PlayerPrefs.SetInt("SelectedClothMight", 10);
        }

        PlayerPrefs.Save(); // ДОБАВЬТЕ СОХРАНЕНИЕ
    }

    /*private void InitializeSelectedConsumables()
    {
        // Если Selected значения не установлены, устанавливаем дефолтные
        if (PlayerPrefs.GetInt("SelectedFoodConsumable", 0) == 0)
        {
            PlayerPrefs.SetInt("SelectedFoodConsumable", 1);
            // Находим might из дефолтного food1
            var defaultFood = System.Array.Find(consumablePanels, p => p.consumableName == "food1");
            if (defaultFood != null)
            {
                PlayerPrefs.SetInt("SelectedFoodMight", defaultFood.might);
            }
            else
            {
                PlayerPrefs.SetInt("SelectedFoodMight", 10);
            }
        }

        if (PlayerPrefs.GetInt("SelectedToyConsumable", 0) == 0)
        {
            PlayerPrefs.SetInt("SelectedToyConsumable", 1);
            var defaultToy = System.Array.Find(consumablePanels, p => p.consumableName == "toy1");
            if (defaultToy != null)
            {
                PlayerPrefs.SetInt("SelectedToyMight", defaultToy.might);
            }
            else
            {
                PlayerPrefs.SetInt("SelectedToyMight", 10);
            }
        }

        if (PlayerPrefs.GetInt("SelectedClothConsumable", 0) == 0)
        {
            PlayerPrefs.SetInt("SelectedClothConsumable", 1);
            var defaultCloth = System.Array.Find(consumablePanels, p => p.consumableName == "cloth1");
            if (defaultCloth != null)
            {
                PlayerPrefs.SetInt("SelectedClothMight", defaultCloth.might);
            }
            else
            {
                PlayerPrefs.SetInt("SelectedClothMight", 10);
            }
        }

        PlayerPrefs.Save();
    }*/

    public void OnConsumablePanelSelected(ConsumablePanel consumablePanel)
    {
        selectedConsumable = consumablePanel;
        UpdateUI();
    }

    private void OnGetButtonClick()
    {
        if (selectedConsumable != null)
        {
            selectedConsumable.GetConsumable();
            UpdateUI();
        }
    }

    private void OnSelectButtonClick()
    {
        if (selectedConsumable != null)
        {
            selectedConsumable.SelectConsumable();
            UpdateUI();
        }
    }

    private void OnBackButtonClick()
    {
        SaveSelectedConsumables();
        SceneManager.LoadScene("MainMenu");
    }

    private void SaveSelectedConsumables()
    {
        foreach (var panel in consumablePanels)
        {
            if (panel.IsEquipped)
            {
                switch (panel.consumableType)
                {
                    case ConsumableType.Food:
                       // PlayerPrefs.SetInt("SelectedFoodConsumable", panel.consumableNum);
                        PlayerPrefs.SetInt("SelectedFoodMight", panel.might);
                        break;
                    case ConsumableType.Toy:
                       // PlayerPrefs.SetInt("SelectedToyConsumable", panel.consumableNum);
                        PlayerPrefs.SetInt("SelectedToyMight", panel.might);
                        break;
                    case ConsumableType.Cloth:
                       // PlayerPrefs.SetInt("SelectedClothConsumable", panel.consumableNum);
                        PlayerPrefs.SetInt("SelectedClothMight", panel.might);
                        break;
                }
            }
        }

        PlayerPrefs.Save();
        /*if (dragItemManager != null)
            dragItemManager.OnConsumableSelectionChanged();*/
    }

    private void UpdateUI()
    {
        if (selectedConsumable != null)
        {
            bool isGet = selectedConsumable.IsGet;
            bool isEquipped = selectedConsumable.IsEquipped;

            getButton.gameObject.SetActive(!isGet);
            selectButton.gameObject.SetActive(isGet && !isEquipped);

            priceText.text = selectedConsumable.price.ToString();

            int currentMoney = Money.Instance != null ? Money.Instance.GetCurrentMoney() : 0;
            getButton.interactable = currentMoney >= selectedConsumable.price;
        }
        else
        {
            getButton.gameObject.SetActive(false);
            selectButton.gameObject.SetActive(false);
        }

        moneyText.text = Money.Instance != null ? Money.Instance.GetCurrentMoney().ToString() : "0";

        foreach (var panel in consumablePanels)
        {
            panel.UpdateVisual();
        }
    }
   

    
}

public enum ConsumableType
{
    Food,
    Toy,
    Cloth
}

[System.Serializable]
public class ConsumablePanel
{
    [Header("Consumable Settings")] public string consumableName;
    public int consumableNum;
    public ConsumableType consumableType;
    public int price;
    public int might;

    [Header("UI References")] public Button panelButton;
    public Image consumableImage;
    public GameObject selectedFrame;
    public GameObject equippedIndicator;

    private ConsumableShopManager _consumableShop;

    public bool IsGet => PlayerPrefs.GetInt(consumableName + "_get", 0) == 1;
    public bool IsEquipped => PlayerPrefs.GetInt(consumableName + "_equip", 0) == 1;


    public void Initialize(ConsumableShopManager shop)
    {
        _consumableShop = shop;
        panelButton.onClick.AddListener(OnPanelClick);
    }

    private void OnPanelClick()
    {
        _consumableShop.OnConsumablePanelSelected(this);
    }

    public void UpdateVisual()
    {
        if (selectedFrame != null)
            selectedFrame.SetActive(_consumableShop.selectedConsumable == this);

        if (equippedIndicator != null)
            equippedIndicator.SetActive(IsEquipped);
    }

    public void GetConsumable()
    {
        if (Money.Instance != null && Money.Instance.SpendMoney(price) && !IsGet)
        {
            PlayerPrefs.SetInt(consumableName + "_get", 1);
            SelectConsumable();
        }
    }

    public void SelectConsumable()
    {
        if (IsGet)
        {
            foreach (var panel in _consumableShop.consumablePanels)
            {
                if (panel.consumableType == this.consumableType)
                {
                    PlayerPrefs.SetInt(panel.consumableName + "_equip", 0);
                }
            }

            PlayerPrefs.SetInt(consumableName + "_equip", 1);

            switch (consumableType)
            {
                case ConsumableType.Food:
                    PlayerPrefs.SetInt("SelectedFoodConsumable", consumableNum);
                    PlayerPrefs.SetInt("SelectedFoodMight", might);
                    break;
                case ConsumableType.Toy:
                    PlayerPrefs.SetInt("SelectedToyConsumable", consumableNum);
                    PlayerPrefs.SetInt("SelectedToyMight", might);
                    break;
                case ConsumableType.Cloth:
                    PlayerPrefs.SetInt("SelectedClothConsumable", consumableNum);
                    PlayerPrefs.SetInt("SelectedClothMight", might);
                    break;
            }
        }
    }
}