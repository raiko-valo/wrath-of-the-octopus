using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Villager : MonoBehaviour
{
    public float NextAttack;
    public float CooldownAttack;
    public NPCbullet BulletAttackPrefab;

    private float smoothSpeed = 0.7f;

    private Rigidbody2D rb;
    private Vector3 originalPosition;
    private float targetPosition;
    private bool isPlayerNear;
    private float detectionRadius = 6f;
    private bool isIdle = false;
    private NPCbullet attackMove;
    private Tilemap tilemapGameObject;
    private Animator animator;

    public void SetBulletAttackPrefab(NPCbullet bulletPrefab)
    {
        BulletAttackPrefab = bulletPrefab;
    }


    private void Start()
    {
        GameObject tilemapObject = GameObject.FindWithTag("GroundTile");
        tilemapGameObject = tilemapObject.GetComponent<Tilemap>();
        originalPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        float randomOffset = Random.Range(-2, 2);
        float newTargetX = originalPosition.x + randomOffset;

        // Set the new target position
        targetPosition = newTargetX;

        NextAttack = Time.time;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        FlipHorizontally();

        animator.SetBool("Moving", !isIdle);
        if (IsPlayerNearObject())
        {
            targetPosition = Player.Instance.transform.position.x;
            smoothSpeed = 1.3f;

            if (Time.time >= NextAttack)
            {
                ToggleAttack(BulletAttackPrefab, 0.7f);
            }
        }
        else if (Mathf.Abs(transform.position.x - targetPosition) <= 0.3)
        {
            smoothSpeed = 0.7f;
            targetPosition = originalPosition.x + Random.Range(-7, 7);
        }


        if (IsDiagonalTileWalkable() && IsPlayerNearObject())
        {
            move();
        } else if (isIdle) 
        {
            targetPosition = transform.position.x;
        } else if (!IsDiagonalTileWalkable() && !IsPlayerNearObject())
        {
            smoothSpeed = 0.7f;
            targetPosition = originalPosition.x + Random.Range(-7, 7);
        } else if (IsDiagonalTileWalkable())
        {
            move();
        }
        else
        {
            rb.velocity = Vector3.zero;
        }


        // Check if the random number is 1
        if (Random.Range(1, 100) == 1)
        {
            isIdle = !isIdle;
        }
    }

    void FlipHorizontally()
    {
        if (rb.velocity.x / Mathf.Abs(rb.velocity.x) < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
    private bool IsPlayerNearObject()
    {
        isIdle = false;
        // Calculate the distance between the player and the object
        float distanceToPlayer = Vector3.Distance(transform.position, Player.Instance.transform.position);
        return distanceToPlayer <= detectionRadius;
    }

    void move()
    {

        float newX = Mathf.Lerp(transform.position.x, targetPosition, smoothSpeed * Time.deltaTime);

        Vector3 newPosition = new Vector3(
            newX,
            transform.position.y,
            0
        );

        // Calculate velocity based on the new position
        Vector3 velocity = (newPosition - transform.position) / Time.deltaTime;

        // Set a constant velocity
        velocity.x = velocity.x / Mathf.Abs(velocity.x) * smoothSpeed;

        // Apply the velocity to the Rigidbody
        rb.velocity = velocity;
    }

    void ToggleAttack(NPCbullet attackPrefab, float attackDuration)
    {
        if (attackPrefab != null)
        {
            animator.SetTrigger("Attack");
            attackMove = Instantiate<NPCbullet>(attackPrefab);
            attackMove.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            Vector3 playerPosition = Player.Instance.transform.position;
            playerPosition.z = 0f;

            Vector3 direction = (playerPosition - attackMove.transform.position).normalized;

            attackMove.Angle = new Vector3(direction.x, direction.y, 0);

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            attackMove.transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);

            NextAttack = Time.time + attackDuration;
            attackMove = null;

        }
    }

    bool IsDiagonalTileWalkable()
    {
        if (tilemapGameObject == null)
        {
            return false;
        }
        // Check if the tile at the diagonal position is walkable
        int direction = (int)(rb.velocity.x / Mathf.Abs(rb.velocity.x));
        Vector3 diagonalPosition = new Vector3(transform.position.x + direction, transform.position.y - 1, 0);
        return tilemapGameObject.GetTile(tilemapGameObject.WorldToCell(diagonalPosition)) != null;
    }
}
