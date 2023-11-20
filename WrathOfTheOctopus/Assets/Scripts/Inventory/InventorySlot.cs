using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public int Index;
    public Vector3 DefaultScale;
    public Vector3 SelectedScale;

    private bool selected;
    private Image image;
    private Color highlightColor = new Color32(230, 230, 230, 180);
    private Color defaultColor = new Color32(128, 128, 128, 180);
    private Color selectedColor = new Color32(102, 255, 102, 180);

    private void Start()
    {
        image = GetComponent<Image>();
        Events.OnChangeSelected += OnChangeSelected;
    }

    private void OnDestroy()
    {
        Events.OnChangeSelected -= OnChangeSelected;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            inventoryItem.parentAfterDrag = transform;
            inventoryItem.transform.eulerAngles = gameObject.transform.eulerAngles;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (selected) image.color = selectedColor;
        else image.color = highlightColor;
        gameObject.transform.localScale = SelectedScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (selected) image.color = selectedColor;
        else image.color = defaultColor;
        gameObject.transform.localScale = DefaultScale;
    }

    void OnChangeSelected(int index)
    {
        selected = Index == index;
        if (selected) image.color = selectedColor;
        else image.color = defaultColor;
    }
}
