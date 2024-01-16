using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Trap : MonoBehaviour
{
    private PlayerMovement player;
    private Vector3 playerPosition;
    private float trapTime;
    public float trapCooldown = 10f;
    public float trapDuration = 3f;


    void Update()
    {
        if ((Time.time > trapTime + trapDuration) && player != null)
        {
            print("siin");
            player.isTraped = false;
            trapTime = Time.time + trapCooldown;
            player = null;
        }
    }

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.gameObject.GetComponent<PlayerMovement>();

        playerPosition = collision.transform.position;

        if (player != null && Time.time > trapTime)
        {
            trapTime = Time.time;
            player.isTraped = true;

        }
    }
}
