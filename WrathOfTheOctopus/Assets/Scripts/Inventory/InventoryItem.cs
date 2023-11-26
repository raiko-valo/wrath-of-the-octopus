using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("UI")]
    public Image image;
    [HideInInspector] public Transform parentAfterDrag;
    [HideInInspector] public Transform parentBeforeDrag;
    [HideInInspector] public ItemData item;
    public Text text;

    private float parentHeight;

    private void Awake()
    {
        parentHeight = transform.parent.GetComponent<RectTransform>().rect.height;
    }
    public void InitialiseItem(ItemData newItem)
    {
        item = newItem;
        image.sprite = newItem.Sprite;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        parentBeforeDrag = transform.parent;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
        transform.eulerAngles = new Vector3(0, 0, 0);
        text.text = item.Name;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        transform.rotation = parentAfterDrag.rotation;
        transform.SetParent(parentAfterDrag);
        text.text = "";

        int startParentIndex = parentBeforeDrag.GetComponent<InventorySlot>().Index;
        int endParentIndex = parentAfterDrag.GetComponent<InventorySlot>().Index;
        float distanceFromCenter = Vector3.Distance(
            new Vector3(Screen.width / 2, Screen.height / 2, 0),
            Input.mousePosition
            );
        if (distanceFromCenter > Events.GetInventoryWheelSize())
        {
            item.Drop(Player.Instance.transform.position);
            InventoryController.Instance.RemoveItemAt(endParentIndex);
            if (InventoryController.Instance.GetSelectedIndex() == endParentIndex)
                Events.ChangeSelected(-1);
        }
        else if (distanceFromCenter < Events.GetInventoryWheelSize() - parentHeight)
        {
            if (item as HealthBuff != null)
            {
                HealthBuff healthBuff = item as HealthBuff;
                if (healthBuff.GetConsumable())
                {
                    healthBuff.Consume();
                    InventoryController.Instance.RemoveItemAt(startParentIndex);
                }
            }
            else Events.ChangeSelected(endParentIndex);
        }
        else
        {
            InventoryController.Instance.ChangeIndex(startParentIndex, endParentIndex);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.text = item.Name;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.text = "";
    }
}
