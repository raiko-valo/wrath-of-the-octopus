using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SharkMovement : MonoBehaviour
{
    public float DetectionRange = 6.0f;

    private float movementRadius = 5f;
    private float idleSpeed = 2f;
    private float attackSpeed = 4f;
    private float startTime = 0f;
    private float nextAttackTime = 0f;
    private float idleJourneyLength;

    private Vector3 originalPosition;
    private Vector3 endPosition;
    private Vector3 startPosition;

    private bool startMoving = false;
    private Vector3 originalRotation;

    private GameObject octopus;
    private bool isPlayerInRange = false;

    void Start()
    {

        originalPosition = new Vector3(transform.position.x, transform.position.y, 0f); ;

        octopus = GameObject.FindWithTag("Player");

        NewEndPosition();
    }

    void Update()
    {
        if (startMoving && Time.time >= nextAttackTime)
        {
            if (isPlayerInRange)
            {
                if (Vector2.Distance(octopus.transform.position, transform.position) <= DetectionRange)
                {
                    MoveToPlayer();
                }
                else
                {
                    originalPosition = transform.position;
                    NewEndPosition();
                    isPlayerInRange = false; // Player exited the range
                }
            }
            else
            {
                RandomMovement();
                UpdatePlayerInRange();
            }
        }
    }

    void FlipHorizontally(float oldPosition, float newPosition)
    {
        if (oldPosition - newPosition < 0)
        {
            transform.eulerAngles = new Vector3(originalRotation.x, 180, originalRotation.z);
        } else
        {
            transform.eulerAngles = new Vector3(originalRotation.x, 0, originalRotation.z);
        }
    }

    void RandomMovement()
    {

        float distCovered = (Time.time - startTime) * idleSpeed;
        float fracJourney = distCovered / idleJourneyLength;

        transform.position = Vector3.Lerp(startPosition, endPosition, fracJourney);

        if (transform.position == endPosition)
        {
            NewEndPosition();
        }

    }

    void NewEndPosition()
    {
        startPosition = transform.position;
        endPosition = new Vector3(
            Random.Range(originalPosition.x + movementRadius, originalPosition.x - movementRadius),
            Random.Range(originalPosition.y + movementRadius/3, originalPosition.y - movementRadius/3),
            0f
        );
        idleJourneyLength = Vector3.Distance(startPosition, endPosition);
        startTime = Time.time;

        FlipHorizontally(endPosition.x, startPosition.x);
    }

    void MoveToPlayer()
    {
        Vector3 direction = octopus.transform.position - transform.position;
        Vector3 normalizedDirection = direction.normalized;
        Vector3 newPosition = transform.position + normalizedDirection * attackSpeed * Time.deltaTime;

        FlipHorizontally(newPosition.x, transform.position.x);
        transform.position = newPosition;
    }

    void UpdatePlayerInRange()
    {
        // Check if the octopus is within the detection range
        if (Vector2.Distance(octopus.transform.position, transform.position) <= DetectionRange)
        {
            isPlayerInRange = true; // Player entered the range
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Health health = collision.GetComponent<Health>();

        if (health != null)
        {
            nextAttackTime = Time.time + 1f;
        }
    }

    void OnBecameVisible()
    {
        startMoving = true;
    }
}
