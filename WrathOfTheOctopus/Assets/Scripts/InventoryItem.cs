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
    [HideInInspector] public ItemSten item;
    public string name;
    public Text text;
    public void InitialiseItem(ItemSten newItem)
    {
        item = newItem;
        image.sprite = newItem.image;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
        transform.eulerAngles = new Vector3(0, 0, 0);
        text.text = name;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
        text.text = "";
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.text = name;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.text = "";
    }
}
