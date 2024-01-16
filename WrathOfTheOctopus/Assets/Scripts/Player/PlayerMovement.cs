using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f; // Adjust the speed as needed
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    public Tilemap WaterTilemap;
    public Tilemap GroundTilemap;
    public bool isTraped = false;

    private bool isMoving = false; // Flag to track movement state
    private float angle = 0.0f;
    private Camera cam;
    private Vector3 mousePos;

    private CircleCollider2D circleCollider;
    private Rigidbody2D rb;
    private readonly List<RaycastHit2D> castCollisions = new();
    
    public Animator animator;
    public AudioClipGroup audioClipSwirl;

    private Vector3Int playerCellPosition;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;

        circleCollider = gameObject.AddComponent<CircleCollider2D>();
        circleCollider.radius = 0.4f;
        circleCollider.isTrigger = false;
        circleCollider.enabled = false;
        mousePos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        Move();
        audioClipSwirl.Play();
        animator.SetBool("Moving", isMoving);
    
    }

    void Move()
    {
        playerCellPosition = WaterTilemap.WorldToCell(transform.position);

        if (WaterTilemap.GetTile(playerCellPosition) != null)
        {
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            circleCollider.enabled = false;

             if (Input.GetMouseButton(1))
            {
                mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0;
                StartMovingInWater();
            }
        }
        else
        {
            rb.isKinematic = false;
            circleCollider.enabled = true;

            if (Input.GetMouseButton(1))
            {
                mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0;
            }

            StartMovingOnLand();
        }

        if (isMoving && Vector3.Distance(mousePos, transform.position) > 0.05)
        {
            Vector3 direction = (mousePos - transform.position).normalized;
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        }

        transform.rotation = Quaternion.Euler(0, 0, angle);


        if (!isMoving) transform.eulerAngles = new Vector3(0, 0, 0);
    }

    void StartMovingInWater()
    {
        // Check if the Octopus is not already moving
        if (isTraped)
        {
            return;
        }


        if (!isMoving)
        {
            animator.SetBool("Moving", true);
            StartCoroutine(MoveOctopus());
        }
    }
    
    void StartMovingOnLand()
    {
        if (isTraped)
        {
            return;
        }
        int direction = (mousePos.x - transform.position.x > 0) ? 1 : -1;

        Vector3 movement = new Vector3(direction * moveSpeed, 0f, 0f);
        rb.velocity = movement;

        if (Mathf.Abs(transform.position.x - mousePos.x) <= 0.1)
        {
            rb.velocity = new Vector3(0, 0f, 0f);
        }
    }

    IEnumerator MoveOctopus()
    {
        isMoving = true;

        // Continue moving the Octopus until it reaches the mouse position
        while (transform.position != mousePos)
        {

            // Calculate the direction to move
            Vector2 direction = (mousePos - transform.position).normalized;
            Vector2 moveLocation;
            if (WaterTilemap.GetTile(playerCellPosition) == null)
            {
                yield return null;
            }
            if (!IsCollsion(direction))
            {
                moveLocation = mousePos;
            }
            else if (!IsCollsion(new Vector2(direction.x,0)))
            {
                moveLocation = new Vector2(mousePos.x, transform.position.y);
            }
            else if (!IsCollsion(new Vector2(0, direction.y)))
            {
                moveLocation = new Vector2(transform.position.x, mousePos.y);
            }
            else break;
            if (isTraped)
            {
                break;
            }
            

            // Move towards the mousePos
            
            transform.position = Vector3.MoveTowards(transform.position, moveLocation, moveSpeed * Time.deltaTime);
            yield return null;
        }

        isMoving = false;
        animator.SetBool("Moving", false);
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

    void OnCollisionStay(Collision collision)
    {
        // Check if the collision is with a tilemap (you can replace "Tilemap" with your tilemap's tag or layer)
        if (collision.gameObject.CompareTag("GroundTilemap"))
        {
            // Set y-velocity to zero to stop vertical movement when standing on the tilemap
            rb.velocity = new Vector3(rb.velocity.x, 0f);
        }
    }

    public void IncreaseSpeed()
    {
        moveSpeed += 1;
    }
}
