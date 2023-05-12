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

}

public enum ItemType
{
    Crop,
    Consumable,
    Weapon,
    Tool
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
}

