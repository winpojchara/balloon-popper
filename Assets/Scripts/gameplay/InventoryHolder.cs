using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHolder : MonoBehaviour
{
    public enum WeaponState { none, sword, bow }
    [SerializeField] WeaponState weaponState;
    [SerializeField] int currentequipItem = 0;
    public List<Item> items = new List<Item>();
    [SerializeField] PlayerController playerController;
    [SerializeField] GameObject playerSword;
    [SerializeField] GameObject playerBow;
    [SerializeField] bool busy;
    public event Action onUseItem;


    public int GetcurrentequipItemNum => currentequipItem;
    void Start()
    {
        playerController.onSwitchItemPress += SwitchItem;
        playerController.onAttackPress += UseItem;
    }
    public void SwitchItem(int num)
    {
        if (busy) return;
        if (GetItemSlot(num).sO_Item == null) return;
        currentequipItem = num;
        print("Switch to item :" + currentequipItem.ToString());
        RemoveSword();
        RemoveBow();
        SetupItem();

    }

    public void SetupItem()
    {
        if (GetEquipItem().sO_Item == null)
        {
            //no weapon
            weaponState = WeaponState.none;
        }
        else
        {
            switch (GetEquipItem().sO_Item.Id)
            {
                case 1://sword
                    weaponState = WeaponState.sword;
                    break;
                case 2://bow
                    weaponState = WeaponState.bow;
                    break;
            }
        }

        switch (weaponState)
        {
            case WeaponState.none: break;
            case WeaponState.sword:
                EquipSword();
                SwordWeapon swordWeapon = playerSword.GetComponent<SwordWeapon>();
                onUseItem += swordWeapon.Attack;
                swordWeapon.onAttack += ConsumeAmmo;
                break;
            case WeaponState.bow:
                EquipBow();
                BowWeapon bowWeapon = playerBow.GetComponent<BowWeapon>();
                onUseItem += bowWeapon.Shoot;
                bowWeapon.onAttack += ConsumeAmmo;
                break;
        }
    }

    public void UseItem()
    {
        if (onUseItem != null)
        {
            onUseItem();

        }

    }

    public void ConsumeAmmo(bool @true)
    {
        if (@true)
        {
            GetEquipItem().ammo--;
        }
        if (GetEquipItem().ammo <= 0)
        {
            GetEquipItem().RemoveItem();
            ClearItem();
        }
    }

    

    public void ConsumeAmmo()
    {
        GetEquipItem().ammo--;
        if (GetEquipItem().ammo <= 0)
        {
            GetEquipItem().RemoveItem();
            ClearItem();
        }
    }

    public void EquipSword()
    {
        playerSword.SetActive(true);
    }

    private void ClearItem()
    {
        RemoveSword();
        RemoveBow();
    }

    public void RemoveSword()
    {
        playerSword.SetActive(false);
        SwordWeapon swordWeapon = playerSword.GetComponent<SwordWeapon>();
        onUseItem -= swordWeapon.Attack;
        swordWeapon.onAttack -= ConsumeAmmo;

    }

    public void EquipBow()
    {
        playerBow.SetActive(true);
    }
    public void RemoveBow()
    {
        playerBow.SetActive(false);
        BowWeapon bowWeapon = playerBow.GetComponent<BowWeapon>();
        onUseItem -= bowWeapon.Shoot;
        bowWeapon.onAttack -= ConsumeAmmo;
    }

    public Item GetEquipItem()
    {
        return items[currentequipItem];
    }


    public Item GetItemSlot(int num)
    {
        return items[num];
    }

    public void SetBusy(bool value)
    {
        busy = value;
    }

    public void AddItem(SO_item new_sO_Item)
    {  
        //add current slot first if it's empty
        if(GetEquipItem().sO_Item == null)
        {
            GetEquipItem().SetNewItem(new_sO_Item);
            SwitchItem(currentequipItem);
            return;
        }

        //if no then add the empty slot
        for (int i = 0; i <= 3; i++)
        {
            if (GetItemSlot(i).sO_Item == null)
            {
                GetItemSlot(i).SetNewItem(new_sO_Item);
                SwitchItem(currentequipItem);
                return;
            }
        }

        //if inventory is full, replace with current slot
        GetEquipItem().SetNewItem(new_sO_Item);
        SwitchItem(currentequipItem);


    }
}
