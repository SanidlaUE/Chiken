using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragobleItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public ItemType itemType;
    public FoodType foodType;
    public ToyType toyType;
    public WashclothType washclothType;
    public static event Action<DragobleItem> OnDrag;
    public DragobleManager manager;
    public Canvas _canvas;
    public Vector2 _offset;


    public void OnBeginDrag(PointerEventData eventData)
    {
        OnDrag?.Invoke(this);
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas.transform as RectTransform,
            eventData.position, Camera.main, out var pos);
        manager.dragObj.transform.localPosition = pos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        StartCoroutine(manager.DisableIcon());
    }
}
