using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkMovement : MonoBehaviour
{
    private float xCoordinate;
    private bool startMoving = false;
    private Vector3 originalRotation;
    private float lastPingPongValue;
    private Vector2 lastDirection;

    public float accelerationSpeed = 25.0f; // Adjust the acceleration speed as needed
    public float detectionRange = 6.0f; // Adjust the detection range as needed
    private GameObject octopus;
    private Rigidbody2D rb;
    private bool isPlayerInRange = false;
    private bool cooldown = false;

    void Start()
    {
        xCoordinate = transform.position.x;
        originalRotation = transform.eulerAngles;
        lastPingPongValue = 0f;

        // Find the octopus GameObject and its Rigidbody2D component
        octopus = GameObject.FindWithTag("Player");
        rb = GetComponent <Rigidbody2D>();

        // Initialize the lastDirection based on the initial positions
        lastDirection = (octopus.transform.position - transform.position).normalized;
    }

    void Update()
    {
        if (startMoving)
        {
            if (isPlayerInRange)
            {
                // Check if the octopus is within the detection range
                if (Vector2.Distance(octopus.transform.position, transform.position) <= detectionRange)
                {
                    // Accelerate towards the octopus
                    Vector2 direction = (octopus.transform.position - transform.position).normalized;
                    rb.velocity = direction * accelerationSpeed;

                    // Determine the direction based on the position of the octopus
                    if (octopus.transform.position.x > transform.position.x)
                    {
                        // Octopus is to the right of the shark
                        FlipHorizontally();
                    }
                    else
                    {
                        // Octopus is to the left of the shark
                        ResetScale();
                    }

                    lastDirection = direction;
                }
                else
                {
                    isPlayerInRange = false; // Player exited the range
                }
            }
            else
            {
                PingPongMovement();
                UpdatePlayerInRange();
            }
        }

        if (Vector2.Distance(octopus.transform.position, transform.position) <= 0.1f)
        {
            if (!cooldown)
            {
                Health.Instance.RemoveHealth();
                cooldown = true;
                //StartCoroutine(Wait(3));
            }
        }
    }

    void FlipHorizontally()
    {
        transform.eulerAngles = new Vector3(originalRotation.x, 180, originalRotation.z);
    }

    void ResetScale()
    {
        transform.eulerAngles = originalRotation;
    }

    void PingPongMovement()
    {
        float pingPongValue = Mathf.PingPong(Time.time, 5f);
        if (pingPongValue > lastPingPongValue)
        {
            FlipHorizontally();
        }
        else
        {
            ResetScale();
        }
        lastPingPongValue = pingPongValue;
        transform.position = new Vector3(pingPongValue + xCoordinate, transform.position.y, 0f);
    }

    void UpdatePlayerInRange()
    {
        // Check if the octopus is within the detection range
        if (Vector2.Distance(octopus.transform.position, transform.position) <= detectionRange)
        {
            isPlayerInRange = true; // Player entered the range
        }
    }

    void OnBecameVisible()
    {
        startMoving = true;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    IEnumerator Wait(int n)
    {
        cooldown = false;
        yield return new WaitForSeconds(n);
    }
}
