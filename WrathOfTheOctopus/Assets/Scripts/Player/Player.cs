using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public Health health;
    private void Awake()
    {
        Instance = this;
        Events.OnDeath += RespawnPlayer;
    }

    private void OnDestroy()
    {
        Events.OnDeath -= RespawnPlayer;
    }

    public bool InRange(Vector3 pos, float range)
    {
        return Vector3.Distance(transform.position, pos) <= range;
    }

    public void RespawnPlayer()
    {
        gameObject.transform.position = new Vector3(0, 0, 0);
        for (int i = 0; i < 4; i++)
        {
            StartCoroutine(Flash(3));
        }
    }

    IEnumerator Flash(int n)
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 100);
        yield return new WaitForSeconds(n);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 100);
        yield return new WaitForSeconds(n);
    }
}
