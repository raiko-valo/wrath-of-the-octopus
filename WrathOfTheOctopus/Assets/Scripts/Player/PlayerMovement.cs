using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool isMoving = false; // Flag to track movement state
    private float moveSpeed = 5.0f; // Adjust the speed as needed
    private float angle = 0.0f;
    private Camera cam;
    private Vector3 mousePos;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(1))
        {
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            StartMoving();
        }

        if (isMoving && Vector3.Distance(mousePos, transform.position) > 0.05)
        {
            Vector3 direction = (mousePos - transform.position).normalized;
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        }

        transform.rotation = Quaternion.Euler(0, 0, angle);

        if (!isMoving) transform.eulerAngles = new Vector3(0, 0, 0);
    }

    void StartMoving()
    {
        // Check if the Octopus is not already moving
        if (!isMoving)
        {
            StartCoroutine(MoveOctopus());
        }
    }

    IEnumerator MoveOctopus()
    {
        isMoving = true;

        // Continue moving the Octopus until it reaches the mouse position
        while (transform.position != mousePos)
        {
            // Calculate the direction to move
            Vector3 direction = (mousePos - transform.position).normalized;

            // Move towards the mousePos
            transform.position = Vector3.MoveTowards(transform.position, mousePos, moveSpeed * Time.deltaTime);

            yield return null;
        }

        isMoving = false;
    }
}
