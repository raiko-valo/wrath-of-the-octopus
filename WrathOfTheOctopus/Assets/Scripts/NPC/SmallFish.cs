using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallFish : MonoBehaviour
{
    private float xCoordinate;

    private bool startMoving = false;
    private Vector3 originalRotation;
    private float lastpingPongValue;

    // Start is called before the first frame update
    // Start is called before the first frame update
    void Start()
    {
        xCoordinate = transform.position.x;
        originalRotation = transform.eulerAngles;
        lastpingPongValue = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (startMoving)
        {
            float pingPongValue = Mathf.PingPong(Time.time, 5f);
            if (pingPongValue > lastpingPongValue)
            {
                transform.eulerAngles = originalRotation; // Restore the original rotation
            }
            else
            {
                transform.eulerAngles = new Vector3(originalRotation.x, 180, originalRotation.z);
            }
            lastpingPongValue = pingPongValue;
            transform.position = new Vector3(pingPongValue + xCoordinate, transform.position.y, 0f);
        }
    }

    private void OnBecameVisible()
    {
        startMoving = true;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
