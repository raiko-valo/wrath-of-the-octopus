using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class UpgradeIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string Name;
    public string Description;
    public string Requirements;
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI DescriptionText;
    public TextMeshProUGUI RequirementsText;

    private Image image;
    private Color highlightColor = new Color(0, 0, 0);
    private Color normalColor = new Color(0, 0, 0, 120);

    private void Start()
    {
        image = GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        NameText.text = Name + ":";
        DescriptionText.text = Description;
        RequirementsText.text = "Requirements:" + Environment.NewLine + Environment.NewLine + Requirements;
        image.color = highlightColor;
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        NameText.text = "";
        DescriptionText.text = "";
        RequirementsText.text = "";
        image.color = normalColor;
    }

    public void DestroyUpgrade()
    {
        gameObject.SetActive(false);
        NameText.text = "";
        DescriptionText.text = "";
        RequirementsText.text = "";
    }
}
