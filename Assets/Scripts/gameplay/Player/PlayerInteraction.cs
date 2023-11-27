using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerInteraction : MonoBehaviour
{
    PlayerController player;
    [SerializeField] GameObject parent;
    [SerializeField] private GameObject currentInteractableItem;


    private void Awake()
    {
        player = parent.GetComponent<PlayerController>();
        
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        player.onInteractionPress += Interact_performed;
    }

    private void Update()
    {
    
        // if (Input.GetKeyDown(KeyCode.E) && currentInteractableItem != null)
        // {
        //     // Perform the interaction with the current item
        //     InteractWithItem(currentInteractableItem);
        // }
    }

    public void Interact_performed()
    {
        print("E");
        if(currentInteractableItem == null)
        {
            return;
        }

        InteractWithItem(currentInteractableItem);
        PlayPickSound();
    }

    public void SetInteractableItem(GameObject item)
    {
        currentInteractableItem = item;
    }

    public void PlayPickSound()
    {
        AudioPlaying.GetAudioPlaying.PlaySound("Pick");
    }

    public void InteractWithItem(GameObject item)
    {
        //interaction
        Debug.Log("Interacting with " + item.name);

        DroppingItems items = item.GetComponent<DroppingItems>();
        items.Item_perform(gameObject);
        
        items.Destroy_perform();
    }
}

