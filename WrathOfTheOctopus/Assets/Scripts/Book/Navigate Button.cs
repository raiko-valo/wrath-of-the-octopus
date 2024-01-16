using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NavigateButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject Button;
    public GameObject Menu;
    public bool Active;
    public NavigateButton[] OtherButtons;
    public TextMeshProUGUI MenuText;

    private float y;
    private float yButton;
    private bool hovering = false;

    private void Awake()
    {
        yButton = Button.transform.position.y;
        y = gameObject.transform.position.y;

        if (Active)
        {
            MenuText.text = Button.GetComponentInChildren<TextMeshProUGUI>().text;
            Menu.SetActive(true);
            Button.SetActive(false);
        }
        else
        {
            Menu.SetActive(false);
            Button.SetActive(true);
        }
    }

    private void Update()
    {
        if (hovering)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!Active) Activate();
            }
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        gameObject.transform.localScale = new Vector3(0.25f, 0.3177904f + 0.15f, 0.3177904f);
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, y - 40, gameObject.transform.position.z);
        Button.gameObject.transform.position = new Vector3(Button.transform.position.x, yButton - 70, Button.transform.position.z);
        hovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.transform.localScale = new Vector3(0.25f, 0.3177904f, 0.3177904f);
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, y, gameObject.transform.position.y);
        Button.gameObject.transform.position = new Vector3(Button.transform.position.x, yButton, Button.transform.position.z);
        hovering = false;
    }

    public void Activate()
    {
        foreach (NavigateButton button in OtherButtons)
        {
            if (button.Active)
            {
                button.Menu.SetActive(false);
                button.Button.SetActive(true);
                button.Active = false;
            }
        }
        MenuText.text = Button.GetComponentInChildren<TextMeshProUGUI>().text;
        Menu.SetActive(true);
        Button.SetActive(false);
        Active = true;
    }
}
