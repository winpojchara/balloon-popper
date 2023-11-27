using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_InventorySlot : MonoBehaviour
{
    public Image weaponImage;
    public TextMeshProUGUI ammotxt;
    public GameObject highlightedSelect;
    [Header("inventoryholder")]
    [SerializeField] InventoryHolder inventoryHolder;
    [SerializeField] int numSlot;

    void Update()
    {
        Item item = inventoryHolder.GetItemSlot(numSlot);
        bool highlight = inventoryHolder.GetcurrentequipItemNum == numSlot;
        DisplayHighlight(highlight);
        if (item.enable())
        {
            DisplayText(item.ammo.ToString());
            DisplayImage(item.GetSO_Item.WeaponImage);
            weaponImage.enabled = true;
        }
        else
        {
            DisplayText("");
            weaponImage.enabled = false;
        }
    }

    public void DisplayText(string n_text)
    {
        ammotxt.text = n_text;
    }

    public void DisplayImage(Sprite n_sprite)
    {
        weaponImage.sprite = n_sprite;
    }

    public void DisplayHighlight(bool value)
    {
        highlightedSelect.SetActive(value);
    }
    
}
