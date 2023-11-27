using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "ScriptableObjects/Item", order = 1)]
public class SO_item : ScriptableObject
{
    [SerializeField]string itemName = "newItem";
    [SerializeField]int id = 1;
    [SerializeField]int startAmmo;
    [SerializeField]Sprite weaponImage;

    public string Name { get => itemName;}
    public int Id { get => id;}
    public int StartAmmo { get => startAmmo;}
    public Sprite WeaponImage { get => weaponImage; }
}
