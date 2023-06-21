using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Items")]
public class Item : ScriptableObject
{
    public int id;
    public string name;
    public Sprite itemSprite;
    public ItemType type;
    public ActionType actionType;
    
    [Header("Extra's")]
    public bool Stackable;
    public string extraText;

    [Header("WeaponSpecs")]
    public float AttackSpeed;
    public float AnimationSpeed;
    public float Damage;

}

public enum ItemType
{
    Crop,
    Consumable,
    Weapon,
    Tool,
    Misc,
}
public enum ActionType
{
    Attack,
    Shoot,
    Farm,
    Plant,
    Consume,
    Mine,
    Cut,
    Ammo,
    Throw
}

