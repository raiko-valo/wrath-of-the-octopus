using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Guide : MonoBehaviour
{
    public GameObject guide;

    public void OnClick()
    {
        guide.SetActive(false);
    }
}
