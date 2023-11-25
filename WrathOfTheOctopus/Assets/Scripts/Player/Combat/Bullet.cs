using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed;
    public Vector3 Angle;

    void Update()
    {
        transform.position += Speed * Time.deltaTime * Angle;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if (player == null)
        {
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
