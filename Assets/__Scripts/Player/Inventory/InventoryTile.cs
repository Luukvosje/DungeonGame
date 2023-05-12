using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class InventoryTile : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private InventoryManager manager;

    public Item ItemHolding;

    public bool Activated = false;
    public bool ContainingItem = false;

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
        manager.hoveringOverItem = true;
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        manager.hoveringOverItem = false;
        Activated = false;
        GetComponent<Image>().sprite = manager.normalTile;
    }
}
