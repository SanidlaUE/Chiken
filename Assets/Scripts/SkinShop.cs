using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinShop : MonoBehaviour
{
    [Header("UI References")] public Button getButton;
    public Button selectButton;
    public TMP_Text priceText;
    public TMP_Text moneyText;

    [Header("Skin Panels")] public SkinPanel[] skinPanels;

    public SkinPanel selectedSkin;

    private void Start()
    {
        if (PlayerPrefs.GetInt("skin1_get", 0) == 0)
        {
            PlayerPrefs.SetInt("skin1_get", 1);
            PlayerPrefs.SetInt("skin1_equip", 1);
        }

        getButton.onClick.AddListener(OnGetButtonClick);
        selectButton.onClick.AddListener(OnSelectButtonClick);

        foreach (var panel in skinPanels)
        {
            panel.Initialize(this);
        }
        UpdateUI();
    }
    public void OnSkinPanelSelected(SkinPanel skinPanel)
    {
        selectedSkin = skinPanel;
        UpdateUI();
    }
    private void OnGetButtonClick()
    {
        if (selectedSkin != null)
        {
            selectedSkin.GetSkin();
            UpdateUI();
        }
    }
    private void OnSelectButtonClick()
    {
        if (selectedSkin != null)
        {
            selectedSkin.SelectSkin();
            UpdateUI();
        }
    }
    private void UpdateUI()
    {
        if (selectedSkin != null)
        {
            bool isGet = selectedSkin.IsGet;
            bool isEquipped = selectedSkin.IsEquipped;


            getButton.gameObject.SetActive(!isGet);
            selectButton.gameObject.SetActive(isGet && !isEquipped);

            priceText.text = selectedSkin.price.ToString();

            int currentMoney = PlayerPrefs.GetInt("Money", 0);
            getButton.interactable = currentMoney >= selectedSkin.price;
        }
        else
        {
            getButton.gameObject.SetActive(false);
            selectButton.gameObject.SetActive(false);
        }
        moneyText.text = PlayerPrefs.GetInt("Money", 0).ToString();
        
        foreach (var panel in skinPanels)
        {
            panel.UpdateVisual();
        }
    }

    [ContextMenu("Reset All Skins")]
    public void ResetAllSkins()
    {
        foreach (var panel in skinPanels)
        {
            PlayerPrefs.DeleteKey(panel.skinName + "_get");
            PlayerPrefs.DeleteKey(panel.skinName + "_equip");
        }

        PlayerPrefs.SetInt("skin1_get", 1);
        PlayerPrefs.SetInt("skin1_equip", 1);
        UpdateUI();
    }

    [ContextMenu("Add 1000 Money")]
    public void AddTestMoney()
    {
        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money", 0) + 1000);
        UpdateUI();
    }
}
[System.Serializable]
public class SkinPanel
{
    [Header("Skin Settings")] public string skinName;
    public int skinNum;
    public int price;

    [Header("UI References")] public Button panelButton;
    public Image skinImage;
    public GameObject selectedFrame;
    public GameObject equippedIndicator;

    private SkinShop _skinShop;

    public bool IsGet => PlayerPrefs.GetInt(skinName + "_get", 0) == 1;
    public bool IsEquipped => PlayerPrefs.GetInt(skinName + "_equip", 0) == 1;

    public void Initialize(SkinShop shop)
    {
        _skinShop = shop;
        panelButton.onClick.AddListener(OnPanelClick);
    }
    private void OnPanelClick()
    {
        _skinShop.OnSkinPanelSelected(this);
    }
    public void UpdateVisual()
    {
        if (selectedFrame != null)
            selectedFrame.SetActive(_skinShop.selectedSkin == this);


        if (equippedIndicator != null)
            equippedIndicator.SetActive(IsEquipped);
    }
    public void GetSkin()
    {
        int currentMoney = PlayerPrefs.GetInt("Money", 0);

        if (currentMoney >= price && !IsGet)
        {
            PlayerPrefs.SetInt("Money", currentMoney - price);
            PlayerPrefs.SetInt(skinName + "_get", 1);


            SelectSkin();
        }
    }
    public void SelectSkin()
    {
        if (IsGet)
        {
            foreach (var panel in _skinShop.skinPanels)
            {
                PlayerPrefs.SetInt(panel.skinName + "_equip", 0);
            }


            PlayerPrefs.SetInt(skinName + "_equip", 1);
            PlayerPrefs.SetInt("skinNum", skinNum);
        }
    }
}