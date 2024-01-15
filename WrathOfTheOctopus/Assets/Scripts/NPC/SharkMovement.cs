using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.GraphicsBuffer;

public class SharkMovement : MonoBehaviour
{
    public float DetectionRange = 6.0f;
    public float moveSpeed = 2.0f; // Adjust the speed as needed
    public ContactFilter2D movementFilter;
    public float collisionOffset = 0.05f;

    private float movementRadius = 5f;
    private float nextAttackTime = 0f;
    private float nextMoveTime = 0f;
    private bool isMoving = false; // Flag to track movement state

    private Vector3 originalPosition;
    private Vector3 endPosition;
    private Vector3 startPosition;
    private readonly List<RaycastHit2D> castCollisions = new();

    private bool startMoving = false;
    private Vector3 originalRotation;
    private Rigidbody2D rb;
    private GameObject octopus;
    private bool isPlayerInRange = false;
    private Tilemap tilemapGameObject;
    private Vector3 targetPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        GameObject tilemapObject = GameObject.FindWithTag("GroundTile");
        tilemapGameObject = tilemapObject.GetComponent<Tilemap>();

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
                    moveSpeed = 4f;
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
                moveSpeed = 3f;
                RandomMovement();
                UpdatePlayerInRange();

                if (Time.time >= nextMoveTime) 
                {
                    NewEndPosition();
                    nextMoveTime = Time.time + 100f;
                }
            }
        }
    }

    void FlipHorizontally()
    {
        if (transform.position.x - targetPosition.x > 0)
        {
            transform.eulerAngles = new Vector3(originalRotation.x, 180, originalRotation.z);
        } else
        {
            transform.eulerAngles = new Vector3(originalRotation.x, 0, originalRotation.z);
        }
    }

    void RandomMovement()
    {
        if (!isMoving)
        {
            StartCoroutine(MoveShark());
        }
    }

    void NewEndPosition()
    {
        targetPosition = new Vector3(
            Random.Range(originalPosition.x + movementRadius, originalPosition.x - movementRadius),
            Random.Range(originalPosition.y + movementRadius/3, originalPosition.y - movementRadius/3),
            0f
        );

        FlipHorizontally();
    }

    void MoveToPlayer()
    {
        targetPosition = octopus.transform.position;
        if (!isMoving)
        {
            StartCoroutine(MoveShark());
        }
    }

    void UpdatePlayerInRange()
    {
        // Check if the octopus is within the detection range
        if (Vector2.Distance(octopus.transform.position, transform.position) <= DetectionRange)
        {
            isPlayerInRange = true; // Player entered the range
        }
    }

    IEnumerator MoveShark()
    {
        isMoving = true;
        // Continue moving the Octopus until it reaches the mouse position
        while (transform.position != targetPosition)
        {
            // Calculate the direction to move
            Vector2 direction = (targetPosition - transform.position).normalized;
            Vector2 moveLocation;
            if (!IsCollsion(direction))
            {
                moveLocation = targetPosition;
            }
            else if (!IsCollsion(new Vector2(direction.x, 0)))
            {
                moveLocation = new Vector2(targetPosition.x, transform.position.y);
            }
            else if (!IsCollsion(new Vector2(0, direction.y)))
            {
                moveLocation = new Vector2(transform.position.x, targetPosition.y);
            }
            else break;

            // Move towards the mousePos
            transform.position = Vector3.MoveTowards(transform.position, moveLocation, moveSpeed * Time.deltaTime);
            yield return null;
        }

        isMoving = false;
        NewEndPosition();
    }

    bool IsCollsion(Vector2 direction)
    {
        // Check for collisions
        int collisionCount = rb.Cast(
            direction,
            movementFilter,
            castCollisions,
            moveSpeed * Time.deltaTime + collisionOffset);
        return collisionCount != 0;
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
