using UnityEngine;
using UnityEngine.UI;

public class ChickenController : MonoBehaviour
{
    [Header("Chicken Stats")]
    public float hunger = 100f;
    public float happiness = 100f;
    public float dirt = 100f;
    
    [Header("UI References")]
    public Slider hungerBar;
    public Slider happinessBar;
    
    [Header("Dirt Images")]
    public Image dirtImage1; 
    public Image dirtImage2;   
    public Image dirtImage3; 
    
    void Start()
    {
        UpdateUI();
        InvokeRepeating("DecreaseStats", 1f, 2f);
    }

    public void OnEnable()
    {
        hunger = PlayerPrefs.GetFloat("hunger", 100f);
        happiness = PlayerPrefs.GetFloat("happiness", 100f);
        dirt = PlayerPrefs.GetFloat("dirt", 100f);
    }

    public void OnDisable()
    {
        PlayerPrefs.SetFloat("hunger", hunger);
        PlayerPrefs.SetFloat("happiness", happiness);
        PlayerPrefs.SetFloat("dirt", dirt);
    }
    
    public void UseItem(string type, int mightValue)
    {
        Debug.Log($"Использован {type} с силой: {mightValue}");
        
        switch (type.ToLower())
        {
            case "food":
                hunger = Mathf.Clamp(hunger + mightValue, 0, 100);
                Debug.Log($"Курица поела! Сытость: +{mightValue}");
                break;
                
            case "toy":
                happiness = Mathf.Clamp(happiness + mightValue, 0, 100);
                Debug.Log($"Курица поиграла! Счастье: +{mightValue}");
                break;
                
            case "cloth":
                dirt = Mathf.Clamp(dirt + mightValue, 0, 100);
                Debug.Log($"Курица приоделась! Счастье: +{mightValue}");
                break;
                
            default:
                Debug.Log($"Неизвестный тип предмета: {type} с силой: {mightValue}");
                break;
        }
        
        UpdateUI();
    }
    
    private void DecreaseStats()
    {
        hunger = Mathf.Clamp(hunger - 1, 0, 100);
        happiness = Mathf.Clamp(happiness - 1, 0, 100);
        dirt = Mathf.Clamp(dirt - 1, 0, 100);
        UpdateUI();
    }
    
    private void UpdateUI()
    {
        if (hungerBar != null) hungerBar.value = hunger / 100f;
        if (happinessBar != null) happinessBar.value = happiness / 100f;
        
        
        UpdateDirt();
    }
    
    private void UpdateDirt()
    {
        if (dirtImage1 != null) dirtImage1.gameObject.SetActive(dirt <= 80f);
        if (dirtImage2 != null) dirtImage2.gameObject.SetActive(dirt <= 50f);
        if (dirtImage3 != null) dirtImage3.gameObject.SetActive(dirt <= 20f);
    }
}