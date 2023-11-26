using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFollow : MonoBehaviour
{
    public float smoothSpeed = 0.125f;

    private void Start()
    {
        transform.position = new Vector3(
            Player.Instance.transform.position.x,
            Player.Instance.transform.position.x,
            -10f
        );
    }

    void Update()
    {
        Vector3 desiredPosition = new Vector3(
            Player.Instance.transform.position.x,
            transform.position.y,
            3f
        );


        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition;
    }
}
