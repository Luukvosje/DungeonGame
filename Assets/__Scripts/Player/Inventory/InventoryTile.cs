using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class InventoryTile : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool Activated = false;
    private InventoryManager manager;

    public bool ContainingItem = false;

    public Item ItemHolding;

    private void Start()
    {
        manager = FindObjectOfType<InventoryManager>();
        if (!ContainingItem)
            transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0);
    }
    public void AssignNewItemToSlot(Item item)
    {
        ContainingItem = true;
        ItemHolding = item;
        
        transform.GetChild(0).GetComponent<Image>().sprite = item.itemSprite;
        transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
    }


    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        Activated = true;
        manager.selectedItemSlot = gameObject;
        GetComponent<Image>().sprite = manager.hoveringTile;
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        Activated = false;
        GetComponent<Image>().sprite = manager.normalTile;
    }
}
