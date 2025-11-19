using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("UI References")]
    public TMP_Text moneyText;
    public Image currentSkinDisplay;
    public Image currentFoodDisplay;
    public Image currentToyDisplay;
    public Image currentClothDisplay;
    public Button backToMainMenuButton;

    [Header("Sprite References")]
    public Sprite[] skinSprites; 
    public Sprite[] foodSprites; 
    public Sprite[] toySprites;  
    public Sprite[] clothSprites; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        EnsureMoneyInstance();
    }

    private void Start()
    {
        InitializeGame();
        
        if (Money.Instance != null)
        {
            Money.Instance.OnMoneyChanged += OnMoneyChanged;
        }
        else
        {
            Debug.LogWarning("Money instance is null in GameManager Start");
        }
        if (backToMainMenuButton != null)
        {
            backToMainMenuButton.onClick.AddListener(ReturnToMainMenu);
        }
        
        UpdateAllDisplays();
    }

    private void EnsureMoneyInstance()
    {
        if (Money.Instance == null)
        {
            GameObject moneyObject = new GameObject("Money");
            moneyObject.AddComponent<Money>();
            Debug.Log("Money instance created by GameManager");
        }
    }

    private void InitializeGame()
    {
        //EnsureDefaultItems();
    }

    /*private void EnsureDefaultItems()
    {
        if (PlayerPrefs.GetInt("skin1_get", 0) == 0)
        {
            PlayerPrefs.SetInt("skin1_get", 1);
            PlayerPrefs.SetInt("skin1_equip", 1);
            PlayerPrefs.SetInt("skinNum", 1);
        }

        if (PlayerPrefs.GetInt("food1_get", 0) == 0)
        {
            PlayerPrefs.SetInt("food1_get", 1);
            PlayerPrefs.SetInt("food1_equip", 1);
            PlayerPrefs.SetInt("SelectedFoodConsumable", 1);
        }

        if (PlayerPrefs.GetInt("toy1_get", 0) == 0)
        {
            PlayerPrefs.SetInt("toy1_get", 1);
            PlayerPrefs.SetInt("toy1_equip", 1);
            PlayerPrefs.SetInt("SelectedToyConsumable", 1);
        }

        if (PlayerPrefs.GetInt("cloth1_get", 0) == 0)
        {
            PlayerPrefs.SetInt("cloth1_get", 1);
            PlayerPrefs.SetInt("cloth1_equip", 1);
            PlayerPrefs.SetInt("SelectedClothConsumable", 1);
        }

        PlayerPrefs.Save();
    }*/

    private void OnMoneyChanged(int newMoney)
    {
        UpdateMoneyDisplay();
    }

    public void UpdateAllDisplays()
    {
        UpdateMoneyDisplay();
        UpdateSkinDisplay();
        //UpdateConsumableDisplays();
    }

    private void UpdateMoneyDisplay()
    {
        if (moneyText != null)
        {
            int currentMoney = 0;            
            
            if (Money.Instance != null)
            {
                currentMoney = Money.Instance.GetCurrentMoney();
            }
            else
            {
                currentMoney = PlayerPrefs.GetInt("Money", 0);
                EnsureMoneyInstance();
            }
            
            moneyText.text = currentMoney.ToString();
        }
    }

    private void UpdateSkinDisplay()
    {
        if (currentSkinDisplay != null && skinSprites != null && skinSprites.Length > 0)
        {
            int currentSkinNum = PlayerPrefs.GetInt("skinNum", 1);
            if (currentSkinNum > 0 && currentSkinNum <= skinSprites.Length)
            {
                currentSkinDisplay.sprite = skinSprites[currentSkinNum - 1];
            }
            else if (skinSprites.Length > 0)
            {
                currentSkinDisplay.sprite = skinSprites[0];
            }
        }
    }

    /*private void UpdateConsumableDisplays()
    {
        if (currentFoodDisplay != null && foodSprites != null && foodSprites.Length > 0)
        {
            int currentFoodNum = PlayerPrefs.GetInt("SelectedFoodConsumable", 1);
            if (currentFoodNum > 0 && currentFoodNum <= foodSprites.Length)
            {
                currentFoodDisplay.sprite = foodSprites[currentFoodNum - 1];
            }
            else if (foodSprites.Length > 0)
            {
                currentFoodDisplay.sprite = foodSprites[0];
            }
        }
        if (currentToyDisplay != null && toySprites != null && toySprites.Length > 0)
        {
            int currentToyNum = PlayerPrefs.GetInt("SelectedToyConsumable", 1);
            if (currentToyNum > 0 && currentToyNum <= toySprites.Length)
            {
                currentToyDisplay.sprite = toySprites[currentToyNum - 1];
            }
            else if (toySprites.Length > 0)
            {
                currentToyDisplay.sprite = toySprites[0];
            }
        }
        if (currentClothDisplay != null && clothSprites != null && clothSprites.Length > 0)
        {
            int currentClothNum = PlayerPrefs.GetInt("SelectedClothConsumable", 1);
            if (currentClothNum > 0 && currentClothNum <= clothSprites.Length)
            {
                currentClothDisplay.sprite = clothSprites[currentClothNum - 1];
            }
            else if (clothSprites.Length > 0)
            {
                currentClothDisplay.sprite = clothSprites[0];
            }
        }
    }*/
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    private void OnEnable()
    {
        if (Money.Instance != null)
        {
            Money.Instance.OnMoneyChanged += OnMoneyChanged;
        }
    }
    private void OnDisable()
    {
        if (Money.Instance != null)
        {
            Money.Instance.OnMoneyChanged -= OnMoneyChanged;
        }
    }
    
}
