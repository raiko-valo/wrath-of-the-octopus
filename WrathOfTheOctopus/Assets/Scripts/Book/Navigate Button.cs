using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class NavigateButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private float y;

    private void Awake()
    {
        y = gameObject.transform.position.y;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, y + 20, gameObject.transform.position.y);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, y - 20, gameObject.transform.position.y);
    }
}
