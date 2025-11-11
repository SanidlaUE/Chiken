using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DragobleManager : MonoBehaviour
{
    public FoodType feedType;
    public ToyType toyType;
    public WashclothType washclothType;
    public GameObject dragObj;
    public RectTransform _rect;

    public void BeginDrag(DragobleItem dragobleItem)
    {
        dragObj.SetActive(true);
        dragObj.GetComponent<Image>().sprite = dragobleItem.transform.GetChild(0).GetComponent<Image>().sprite;

        switch (dragobleItem.itemType)
        {
            case ItemType.food:
            {
                feedType = dragobleItem.foodType;
                break;
            }
            case ItemType.toy:
            {
                toyType = dragobleItem.toyType;
                break;
            }
            case ItemType.washcloth:
            {
                washclothType = dragobleItem.washclothType;
                break;
            }
        }
    }

    public void OnEnable()
    {
        _rect = dragObj.GetComponent<RectTransform>();
        DragobleItem.OnDrag += BeginDrag;
        dragObj.SetActive(false);
    }

    public void OnDisable()
    {
        DragobleItem.OnDrag -= BeginDrag;
    }

    public IEnumerator DisableIcon()
    {
        yield return new WaitForSeconds(0.1f);
        dragObj.SetActive(false);
    }
}
