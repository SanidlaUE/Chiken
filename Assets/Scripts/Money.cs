using System.Collections;
using UnityEngine;

public class Money : MonoBehaviour
{
    public static Money Instance { get; private set; }
    
    [SerializeField] private int moneyPerSecond = 10;
    
    private const string MONEY_KEY = "Money";
    private int currentMoney;
    private Coroutine moneyCoroutine;
    
    public System.Action<int> OnMoneyChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            
            Initialize();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Initialize()
    {
        currentMoney = PlayerPrefs.GetInt(MONEY_KEY, 0);
        moneyCoroutine = StartCoroutine(IncreaseMoneyRoutine());
    }

    private IEnumerator IncreaseMoneyRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            AddMoney(moneyPerSecond);
        }
    }
    public void AddMoney(int amount)
    {
        currentMoney += amount;
        SaveMoney();
        OnMoneyChanged?.Invoke(currentMoney);
    }
    public bool SpendMoney(int amount)
    {
        if (currentMoney >= amount)
        {
            currentMoney -= amount;
            SaveMoney();
            OnMoneyChanged?.Invoke(currentMoney);
            return true;
        }
        return false;
    }
    public int GetCurrentMoney()
    {
        return currentMoney;
    }
    public void SetMoneyPerSecond(int newRate)
    {
        moneyPerSecond = newRate;
    }
    public void ResetMoney()
    {
        currentMoney = 0;
        SaveMoney();
        OnMoneyChanged?.Invoke(currentMoney);
    }

    private void SaveMoney()
    {
        PlayerPrefs.SetInt(MONEY_KEY, currentMoney);
        PlayerPrefs.Save();
    }
    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            SaveMoney();
        }
    }

    private void OnApplicationQuit()
    {
        SaveMoney();
    }
}
