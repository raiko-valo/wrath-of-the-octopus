using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFollow : MonoBehaviour
{
    void Update()
    {
        transform.position = new Vector3(
            Player.Instance.transform.position.x,
            transform.position.y,
            3f
        );
    }
}
