using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public SO_item sO_Item;
    public int ammo;

    public SO_item GetSO_Item => sO_Item;
    public bool enable()
    {
        if (sO_Item == null)
        {
            return false;
        }
        return true;
    }

    public void SetNewItem(SO_item new_sO_Item)
    {
        sO_Item = new_sO_Item;
        SetItemStats();
    }

    public void RemoveItem()
    {
        sO_Item = null;
        ammo = 0;
    }

    public void SetItemStats()
    {
        ammo = sO_Item.StartAmmo;
    }




}
