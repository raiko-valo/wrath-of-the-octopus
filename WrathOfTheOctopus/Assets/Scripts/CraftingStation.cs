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

    public float CraftingRange;

    private GameObject craftingMenu;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        //Creates this crafting station UI
        craftingMenu = Instantiate(CraftingMenu, Canva.transform);
        craftingMenu.SetActive(false);

        //Each item in CraftableItems gets its crafting card, wich shows the item sprite, ammount and crafting requirements
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

    private void Update()
    {
        if (!Player.Instance.InRange(transform.position, CraftingRange)) 
            craftingMenu.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (craftingMenu.activeSelf) 
            craftingMenu.SetActive(false);
        else if (Player.Instance.InRange(transform.position, CraftingRange)) 
            craftingMenu.SetActive(true);
    }

    private void OnMouseEnter()
    {
        if (Player.Instance.InRange(transform.position, CraftingRange)) 
            spriteRenderer.color = Color.gray;
    }

    private void OnMouseExit()
    {
        spriteRenderer.color = Color.white;
    }
}
