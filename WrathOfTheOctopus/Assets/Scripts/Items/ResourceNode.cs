using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.ParticleSystem;

public class ResourceNode : MonoBehaviour
{
    public ItemData Item;
    public int ItemDropAmount;
    public int NodeHealth;
    public int ToolLevelRequired;
    public float MiningRange;
    public ParticleSystem MineParticles;
    public ParticleSystem DestroyParticles;
    public AudioClipGroup audioClipMine;

    protected private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    protected virtual void Update()
    {
        if (Player.Instance.InRange(transform.position, MiningRange) && IsMouseOverObject())
        {
            spriteRenderer.color = Color.gray;
            if (Input.GetKeyDown(KeyCode.Mouse0)) Mine();
        }
        else
        {
            spriteRenderer.color = Color.white;
        }
    }

    [ContextMenu("DropItems")]
    public void DropItems()
    {
        for (int item = 0; item < ItemDropAmount; item++)
        {
            float randomAngle = Random.Range(0f, Mathf.PI * 2f);

            // Calculate a random position within the circle using polar coordinates
            float x = transform.position.x + Mathf.Cos(randomAngle) * 0.5f;
            float y = transform.position.y + Mathf.Sin(randomAngle) * 0.5f;

            Vector3 spawnPosition = new Vector3(x, y, transform.position.z);

            Item.Drop(spawnPosition);
        }
    }

    bool IsMouseOverObject()
    {
        // Cast a ray from the mouse position into the scene
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero, 100.0f,LayerMask.GetMask("ResourceNode"));

        // Check if the ray hits a collider
        if (hit.collider != null)
        {
            // Check if the collider belongs to the desired GameObject
            if (hit.collider.gameObject == gameObject)
            {
                return true;
            }
        }

        return false;
    }

    protected void Mine() 
    {
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ToolData tool = InventoryController.Instance.GetSelected() as ToolData;
        if (tool == null)
        {
            tool = ScriptableObject.CreateInstance<ToolData>();
            tool.ToolLevel = 0;
            tool.Damage = 1;
        }
        if (tool.ToolLevel >= ToolLevelRequired)
        {
            ParticleSystem particle = Instantiate(MineParticles, worldPoint, MineParticles.transform.rotation);
            Destroy(particle.gameObject, particle.main.duration);
            audioClipMine.Play();
            NodeHealth -= tool.Damage;
            if (NodeHealth <= 0)
            {
                DropItems();
                ParticleSystem destroyParticle = Instantiate(DestroyParticles, transform.position, DestroyParticles.transform.rotation);
                Destroy(destroyParticle.gameObject, destroyParticle.main.duration);
                Destroy(gameObject);
            }
        }
    }
}
