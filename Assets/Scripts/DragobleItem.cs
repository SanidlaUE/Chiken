using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Settings")] public ConsumableType itemType;

    //[Header("References")] public Image itemImage;
    public Canvas parentCanvas;
    public ChickenController chicken;

    private Vector2 originalPosition;
    private Transform originalParent;
    private CanvasGroup canvasGroup;
    private bool isDragging = false;
    private bool isInitialized = false;

    public int ItemValue
    {
        get
        {
            if (!isInitialized) return 10;

            int value = 10;
            switch (itemType)
            {
                case ConsumableType.Food:
                    value = PlayerPrefs.GetInt("SelectedFoodMight", 10);
                    break;
                case ConsumableType.Toy:
                    value = PlayerPrefs.GetInt("SelectedToyMight", 10);
                    break;
                case ConsumableType.Cloth:
                    value = PlayerPrefs.GetInt("SelectedClothMight", 10);
                    break;
            }

            Debug.Log($"ItemValue для {itemType}: {value}");
            return value;
        }
    }

    /*public void OnEnable()
    {
        switch (itemType)
        {
            case ConsumableType.Food:
            {
                int first = PlayerPrefs.GetInt("food1" + "_equip", 0);
                int second = PlayerPrefs.GetInt("food3" + "_equip", 0);
                int third = PlayerPrefs.GetInt("food3" + "_equip", 0);
                if (PlayerPrefs.GetInt("food1" + "_equip", 0) == 1)
                {
                    itemImage.sprite = food1;
                }

                if (PlayerPrefs.GetInt("food2" + "_equip", 0) == 1)
                {
                    itemImage.sprite = food2;
                }

                if (PlayerPrefs.GetInt("food3" + "_equip", 0) == 1)
                {
                    itemImage.sprite = food3;
                }

                break;
            }
            
        }
       
    }*/

    void Start()
    {
        Debug.Log($"Start вызван для {gameObject.name} ({itemType})");

        Initialize();
    }

    private void Initialize()
    {
        try
        {
            canvasGroup = GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = gameObject.AddComponent<CanvasGroup>();
                Debug.Log($"CanvasGroup добавлен для {gameObject.name}");
            }

            if (parentCanvas == null)
            {
                parentCanvas = GetComponentInParent<Canvas>();
                if (parentCanvas == null)
                {
                    Debug.LogError(
                        $"Parent Canvas не найден для {gameObject.name}! Убедитесь что объект находится на Canvas.");
                    return;
                }
                else
                {
                    Debug.Log($"ParentCanvas найден: {parentCanvas.name}");
                }
            }

            /*if (itemImage == null)
            {
                itemImage = GetComponent<Image>();
                if (itemImage == null)
                {
                    Debug.LogError($"Image компонент не найден на {gameObject.name}!");
                    return;
                }
                else
                {
                    Debug.Log($"ItemImage найден для {gameObject.name}");
                }
            }*/

            if (chicken == null)
            {
                chicken = FindObjectOfType<ChickenController>();
                if (chicken == null)
                {
                    Debug.LogError(
                        $"ChickenController не найден в сцене для {gameObject.name}! Добавьте курицу в сцену.");
                    return;
                }
                else
                {
                    Debug.Log($"ChickenController найден: {chicken.gameObject.name}");
                }
            }

            originalPosition = transform.position;
            originalParent = transform.parent;

            isInitialized = true;
            Debug.Log($"DraggableItem {gameObject.name} успешно инициализирован!");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Ошибка инициализации {gameObject.name}: {e.Message}");
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!isInitialized)
        {
            Debug.LogWarning($"DraggableItem {gameObject.name} не инициализирован!");
            return;
        }

        /*if (!IsItemAvailable())
        {
            Debug.LogWarning($"Предмет {itemType} не доступен для использования!");
            return;
        }*/

        isDragging = true;
        canvasGroup.alpha = 0.7f;
        canvasGroup.blocksRaycasts = false;
        transform.SetParent(parentCanvas.transform);

        Debug.Log($"Начато перетаскивание {itemType}");
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragging || !isInitialized) return;

        RectTransformUtility.ScreenPointToWorldPointInRectangle(
            transform as RectTransform,
            eventData.position,
            parentCanvas.worldCamera,
            out Vector3 worldPoint);

        transform.position = worldPoint;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isDragging || !isInitialized) return;

        isDragging = false;
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        CheckDropOnChicken(eventData);
        ReturnToOriginalPosition();

        Debug.Log($"Завершено перетаскивание {itemType}");
    }

    /*private bool IsItemAvailable()
    {
        if (!isInitialized) return false;

        bool isAvailable = false;

        switch (itemType)
        {
            case ConsumableType.Food:
                int foodSelected = PlayerPrefs.GetInt("SelectedFoodConsumable", 0);
                bool food1Equipped = PlayerPrefs.GetInt("food1_equip", 0) == 1;
                isAvailable = foodSelected > 0 || food1Equipped;
                break;

            case ConsumableType.Toy:
                int toySelected = PlayerPrefs.GetInt("SelectedToyConsumable", 0);
                bool toy1Equipped = PlayerPrefs.GetInt("toy1_equip", 0) == 1;
                isAvailable = toySelected > 0 || toy1Equipped;
                break;

            case ConsumableType.Cloth:
                int clothSelected = PlayerPrefs.GetInt("SelectedClothConsumable", 0);
                bool cloth1Equipped = PlayerPrefs.GetInt("cloth1_equip", 0) == 1;
                isAvailable = clothSelected > 0 || cloth1Equipped;
                break;

            default:
                isAvailable = false;
                break;
        }

        Debug.Log($"Предмет {itemType} доступен: {isAvailable}");
        return isAvailable;
    }*/

    private void CheckDropOnChicken(PointerEventData eventData)
    {
        if (!isInitialized || chicken == null) return;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        bool droppedOnChicken = false;

        foreach (var result in results)
        {
            ChickenController chickenController = result.gameObject.GetComponent<ChickenController>();
            if (chickenController != null)
            {
                chicken.UseItem(itemType.ToString().ToLower(), ItemValue);
                droppedOnChicken = true;
                Debug.Log($"Предмет {itemType} использован на курице!");
                break;
            }
        }

        if (!droppedOnChicken)
        {
            Debug.Log($"Предмет {itemType} не был сброшен на курицу");
        }
    }

    private void ReturnToOriginalPosition()
    {
        if (gameObject != null && originalParent != null)
        {
            transform.SetParent(originalParent);
            transform.position = originalPosition;
        }
    }


    /*public void UpdateItemState()
    {
        if (!isInitialized || itemImage == null) return;

        bool isAvailable = IsItemAvailable();
        itemImage.color = isAvailable ? Color.white : new Color(1, 1, 1, 0.3f);

        Debug.Log($"Состояние {itemType} обновлено: доступен = {isAvailable}");
    }*/
}