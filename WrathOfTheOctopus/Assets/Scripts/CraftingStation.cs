using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingStation : MonoBehaviour
{
    public CraftingData CraftableItems;

    public GameObject Canva;
    public GameObject CraftingMenu;
    public GameObject CraftablePrefab;
    public GameObject CraftingComponentPrefab;

    private GameObject craftingMenu;

    void Start()
    {
        craftingMenu = Instantiate(CraftingMenu, Canva.transform);
        craftingMenu.SetActive(false);
        foreach (ItemData item in CraftableItems.Items)
        {
            GameObject menuItem = Instantiate(CraftablePrefab, craftingMenu.transform);
            menuItem.transform.GetChild(0).GetComponentInChildren<Image>().sprite = item.Sprite;
            menuItem.GetComponentInChildren<TextMeshProUGUI>().text = "x" + item.Recipe.Ammount.ToString();
            menuItem.GetComponent<CraftingCard>().Item = item;
            Transform list = menuItem.transform.Find("List").transform;
            foreach (ItemPair itemPair in item.Recipe.Ingredients)
            {
                GameObject component = Instantiate(CraftingComponentPrefab, list);
                component.GetComponentInChildren<Image>().sprite = itemPair.Item.Sprite;
                component.GetComponentInChildren<TextMeshProUGUI>().text = "x"+itemPair.Ammount.ToString();
            }
        }
    }

    private void OnMouseEnter()
    {
        craftingMenu.SetActive(true);
    }

    private void OnMouseExit()
    {
        craftingMenu.SetActive(false);
    }
}
