using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemHolder : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Transform parentAfterDrag;
    private Image image;
    private InventoryManager manager;
    private bool holdingitem;
    [HideInInspector] public Item ContainingItem;


    private void Start()
    {
        manager = FindObjectOfType<InventoryManager>();
        image = GetComponent<Image>();
    }

    public void OnBeginDrag(PointerEventData eventdata)
    {
        if (transform.GetComponentInParent<InventoryTile>().ContainingItem && !manager.holdingItemWithMouse)
        {
            manager.holdingItemWithMouse = GetComponentInParent<InventoryTile>().ItemHolding;
            image.raycastTarget = false;
            manager.tileclicked = GetComponentInParent<InventoryTile>();
            parentAfterDrag = transform.parent;
            transform.SetParent(transform.root);
            holdingitem = true;
        }
    }

    public void OnDrag(PointerEventData eventdata)
    {
        if (holdingitem)
        {
            transform.position = Input.mousePosition;
        }
    }
    public void OnEndDrag(PointerEventData eventdata)
    {
        if (holdingitem && manager.holdingItemWithMouse)
        {
            manager.tileclicked.ItemHolding = manager.selectedItemSlot.GetComponent<InventoryTile>().ItemHolding;
            manager.selectedItemSlot.GetComponent<InventoryTile>().ContainingItem = true;
            if (!manager.selectedItemSlot.GetComponent<InventoryTile>().ItemHolding)
            {
                manager.tileclicked.ContainingItem = false;
            }
            manager.selectedItemSlot.GetComponent<InventoryTile>().ItemHolding = manager.holdingItemWithMouse;
            manager.holdingItemWithMouse = null;
            manager.tileclicked = null;
            image.raycastTarget = true;

            transform.SetParent(manager.selectedItemSlot.transform);
            manager.selectedItemSlot.transform.GetChild(0).SetParent(parentAfterDrag);
            holdingitem = false;
        }
    }
}
