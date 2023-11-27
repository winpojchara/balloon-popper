using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class LootBox : DroppingItems
{
    [SerializeField] Sprite lootBox_normal;
    [SerializeField] Sprite lootBox_opened;
    public List<GameObject> spawnitems = new List<GameObject>();
    SpriteRenderer spriteRenderer;
    [SerializeField] bool isOpened = false;
    [Header("Spawn")]
    public Transform spawnLocation;
    [SerializeField] float spawnRandomRange_X_value;
    public Transform respawnLocation;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public override void Item_perform(GameObject user)
    {
        if (isOpened)
        {
            return;
        }
        int num = UnityEngine.Random.Range(0, 2);

        switch (num)
        {
            case 0:
                {
                    print("Get sword");
                    GameObject obj = Instantiate(spawnitems[0],spawnLocation.position,quaternion.identity);
                    Rigidbody2D rg = obj.GetComponent<Rigidbody2D>();
                    rg.AddForce(Vector2.up * 300);
                    break;
                }
            case 1:
                {
                    print("Get bow");
                    GameObject obj = Instantiate(spawnitems[1],spawnLocation.position,quaternion.identity);
                    Rigidbody2D rg = obj.GetComponent<Rigidbody2D>();
                    rg.AddForce(Vector2.up * 300);
                    break;
                }
        }
        isOpened = true;
        PlayerInteraction playerInteraction = user.GetComponent<PlayerInteraction>();
        playerInteraction.SetInteractableItem(null);
        spriteRenderer.sprite = lootBox_opened;
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (isOpened)
        {
            return;
        }
        base.OnTriggerEnter2D(other);
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);
    }
    public override void Destroy_perform()
    {
        Invoke("Respawn", 5);
    }

    void Respawn()
    {
        Debug.Log("Lootbox Spawn");
        isOpened = false;
        spriteRenderer.sprite = lootBox_normal;
        float randomizeX = UnityEngine.Random.Range(-spawnRandomRange_X_value,spawnRandomRange_X_value);
        transform.position = respawnLocation.position + new Vector3(randomizeX,0);
    }
}
