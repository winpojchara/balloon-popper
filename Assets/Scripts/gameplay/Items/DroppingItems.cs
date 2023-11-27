using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppingItems : MonoBehaviour
{
    [SerializeField] string playerinteraction_TAG = "PlayerInteraction";
    [SerializeField] protected SO_item sO_Item;

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        print("Enter");
        if (other.CompareTag(playerinteraction_TAG))
        {
            PlayerInteraction playerInteraction = other.GetComponent<PlayerInteraction>();
            if (playerInteraction != null)
            {
                playerInteraction.SetInteractableItem(this.gameObject);
            }
        }
    }
    

    public virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(playerinteraction_TAG))
        {
            PlayerInteraction playerInteraction = other.GetComponent<PlayerInteraction>();

            if (playerInteraction != this.gameObject)
            {
                playerInteraction.SetInteractableItem(null);
            }
        }
    }
    public virtual void Item_perform(GameObject user)
    {

    }

    public virtual void Destroy_perform()
    {

    }
}
