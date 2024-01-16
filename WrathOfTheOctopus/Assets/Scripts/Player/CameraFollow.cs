using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject gameObject;
    public float smoothSpeed = 0.125f;

    private void Start()
    {
        transform.position = new Vector3(
            gameObject.transform.position.x,
            gameObject.transform.position.y,
            -10f
        );
    }

    void Update()
    {
        Vector3 desiredPosition = new Vector3(
            gameObject.transform.position.x,
            gameObject.transform.position.y,
            -10f
        );

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition;
    }
}
