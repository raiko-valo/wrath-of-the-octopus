using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public CircleCollider2D circleCollider;
    public float smoothSpeed = 0.125f;

    private void Start()
    {
        transform.position = new Vector3(
            circleCollider.transform.position.x,
            circleCollider.transform.position.y,
            -10f
        );
    }

    void Update()
    {
        Vector3 desiredPosition = new Vector3(
            circleCollider.transform.position.x,
            circleCollider.transform.position.y,
            -10f
        );

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition;
    }
}
