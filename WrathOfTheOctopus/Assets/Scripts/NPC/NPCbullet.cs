using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCbullet : MonoBehaviour
{
    public float Speed;
    public Vector3 Angle;
    public int Damage = 1;

    void Update()
    {
        transform.position += Speed * Time.deltaTime * Angle;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if (player != null)
        {
            Events.RemoveHealth(Damage);
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
