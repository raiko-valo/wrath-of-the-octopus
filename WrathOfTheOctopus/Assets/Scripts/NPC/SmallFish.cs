using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallFish : MonoBehaviour
{
    private Vector3 targetPosition;
    public float moveSpeed = 0.5f; // Adjust the speed as 
    private bool isMoving = false; // Flag to track movement state
    private Rigidbody2D rb;
    public ContactFilter2D movementFilter;
    private readonly List<RaycastHit2D> castCollisions = new();
    public float collisionOffset = 0.05f;
    private Vector3 originalPosition;
    private float movementRadius = 5f;
    private bool startMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalPosition = new Vector3(transform.position.x, transform.position.y, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (startMoving)
        {
            FlipHorizontally();
            RandomMovement();
        }
    }

    void NewEndPosition()
    {
        targetPosition = new Vector3(
            Random.Range(originalPosition.x + movementRadius, originalPosition.x - movementRadius),
            Random.Range(originalPosition.y + 0.5f, originalPosition.y - 0.5f),
            0f
        );
    }

    void RandomMovement()
    {
        if (!isMoving)
        {
            StartCoroutine(MoveShark());
        }
    }

    void FlipHorizontally()
    {
        if (transform.position.x - targetPosition.x > 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    private void OnBecameVisible()
    {
        startMoving = true;
        NewEndPosition();
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
}
