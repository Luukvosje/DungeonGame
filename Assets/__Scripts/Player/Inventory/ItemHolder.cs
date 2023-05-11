using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ItemHolder : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private InventoryManager manager;

    private Image image;
    public TextMeshProUGUI countText;


    private bool holdingitem;
    public int count = 1;

    [SerializeField] private Transform parentAfterDrag;
    [HideInInspector] public Item ContainingItem;


    private void Start()
    {
        manager = FindObjectOfType<InventoryManager>();
        image = GetComponent<Image>();
    }

    public void RefreshCount()
    {
        countText.text = count.ToString();
        bool textActive = count > 1;
        countText.gameObject.SetActive(textActive);
    }

    public void OnBeginDrag(PointerEventData eventdata)
    {
        if (transform.GetComponentInParent<InventoryTile>().ContainingItem && !manager.holdingItemWithMouse)
        {
            manager.holdingItemWithMouse = GetComponentInParent<InventoryTile>().ItemHolding;
            image.raycastTarget = false;
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().raycastTarget = false;
            manager.tileclicked = GetComponentInParent<InventoryTile>();
            manager.tileclickedItemHolder = this;
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

            transform.SetParent(manager.selectedItemSlot.transform);
            manager.selectedItemSlot.transform.GetChild(0).SetParent(parentAfterDrag);
            manager.selectedItemSlot.GetComponent<InventoryTile>().ItemHolding = manager.holdingItemWithMouse;
        }

        manager.holdingItemWithMouse = null;
        manager.tileclicked = null;
        image.raycastTarget = true;
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().raycastTarget = true;


        holdingitem = false;
    }
}
