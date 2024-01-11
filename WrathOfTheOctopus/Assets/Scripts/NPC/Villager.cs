using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private float detectionRadius = 10f;

    private NPCbullet attackMove;

    private void Start()
    {
        originalPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        float randomOffset = Random.Range(-2, 2);
        float newTargetX = originalPosition.x + randomOffset;

        // Set the new target position
        targetPosition = newTargetX;

        NextAttack = Time.time;

    }

    private void Update()
    {
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
            float randomOffset = Random.Range(-7, 7);
            float newTargetX = originalPosition.x + randomOffset;

            // Set the new target position
            targetPosition = newTargetX;
        }
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

    private bool IsPlayerNearObject()
    {
        // Calculate the distance between the player and the object
        float distanceToPlayer = Vector3.Distance(transform.position, Player.Instance.transform.position);

        // Check if the distance is less than or equal to the detection radius
        return distanceToPlayer <= detectionRadius;
    }

    void ToggleAttack(NPCbullet attackPrefab, float attackDuration)
    {
        if (attackPrefab != null)
        {

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
}
