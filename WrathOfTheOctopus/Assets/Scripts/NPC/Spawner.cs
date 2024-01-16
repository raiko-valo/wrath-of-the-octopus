using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Spawner : MonoBehaviour
{
    public List<GameObject> villagerPrefabs;  // Assign your Villager prefab in the Inspector
    public int maxVillagers = 10;
    public float spawnInterval = 100f;  // 1 minute interval

    private List<GameObject> spawnedVillagers = new();

    private bool hasSpawned = false;
    private float nextSpawnTime;

    // Start is called before the first frame update
    void Start()
    {
        // Spawn initial villagers
        SpawnVillager();
        nextSpawnTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >=  nextSpawnTime)
        {
            // Check if the number of villagers is below the maximum
            if (spawnedVillagers.Count <= maxVillagers)
            {
                // Start a timer to spawn villagers with the specified interval
                SpawnVillager();
            }

            nextSpawnTime = Time.time + spawnInterval;
        }

        for (int i = 0; i < spawnedVillagers.Count; i++)
        {
            GameObject go = spawnedVillagers[i];
            if (go == null)
            {
                spawnedVillagers.Remove(go);
            }
        }


        // Check for victory condition
        if (spawnedVillagers.Count == 0 && hasSpawned)
        {
            Debug.Log("Victory! All villagers are gone.");
            // You might want to perform additional actions here for the victory scenario

            Destroy(gameObject);
        }
    }

    void SpawnVillager()
    {
        // Instantiate a new Villager prefab
        int randomIndex = Random.Range(0, villagerPrefabs.Count);
        GameObject newVillagerObject = Instantiate(villagerPrefabs[randomIndex], transform);
        newVillagerObject.transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        // Add the new villager to the list
        spawnedVillagers.Add(newVillagerObject);

        hasSpawned = true;
    }
}
