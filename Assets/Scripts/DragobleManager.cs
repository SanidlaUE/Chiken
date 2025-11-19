using UnityEngine;


public class DragItemManager : MonoBehaviour
{
    [Header("Drag Items")]
    public DraggableItem foodDragItem;
    public DraggableItem toyDragItem;
    public DraggableItem clothDragItem;
    
    private ConsumableShopManager shopManager;
    
    void Start()
    {
        shopManager = FindObjectOfType<ConsumableShopManager>();
        //UpdateAllItemsState();
    }
    
    /*public void UpdateAllItemsState()
    {
        if (foodDragItem != null) foodDragItem.UpdateItemState();
        if (toyDragItem != null) toyDragItem.UpdateItemState();
        if (clothDragItem != null) clothDragItem.UpdateItemState();
    }*/
    /*public void OnConsumableSelectionChanged()
    {
        UpdateAllItemsState();
    }*/
}
