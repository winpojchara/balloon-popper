using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordItem : DroppingItems
{
    public override void Item_perform(GameObject user)
    {
        InventoryHolder inventoryHolder = FindObjectOfType<InventoryHolder>();
        inventoryHolder.AddItem(sO_Item);
    }

    public override void Destroy_perform()
    {
        Destroy(gameObject);
    }

}
