using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    private void Awake()
    {
        Instance = this;
    }

    public bool InRange(Vector3 pos, float range)
    {
        return Vector3.Distance(transform.position, pos) <= range;
    }

    public void DestroyPlayer()
    {
        if (this.gameObject != null)
        {
            Destroy(this.gameObject);
        }
    }
}
