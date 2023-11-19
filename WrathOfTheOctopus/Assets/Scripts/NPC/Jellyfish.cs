using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jellyfish : MonoBehaviour
{
    public int damage = 1;
    public float movementSpeed = 0.5f;
    private float yCoordinate;

    private bool startMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        yCoordinate = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (startMoving)
        {
            transform.position = new Vector3(transform.position.x, Mathf.PingPong(Time.time*movementSpeed, 2f) + yCoordinate, 0f);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*SpaceShip spaceShip = collision.gameObject.GetComponent<SpaceShip>();
        if (spaceShip != null)
        {
            //collision.gameObject.GetComponent<SpaceShip>().Hit(damage);
            Destroy(gameObject);
        }*/
    }
}
