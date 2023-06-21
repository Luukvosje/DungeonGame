using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [Header("Linking")]
    public WeaponController weaponController;
    public GameObject selectedItemSlot = null;
    public Sprite hoveringTile, normalTile;

    public bool DungeonMode = false;

    public List<Item> _Items = new List<Item>();

    [Header("Inventory")]
    public List<GameObject> InventorySlots = new List<GameObject>();
    public Item holdingItemWithMouse = null;
    public InventoryTile tileclicked;
    public ItemHolder tileclickedItemHolder;
    public int maxStackedItems = 50;
    public ToolTip tooltip;
    public bool hoveringOverItem;

    public bool InventoryActived;
    private Transform inventoryCanvas;

    //Temp
    [Header("Hotbar")]
    public List<GameObject> HotBarSlots = new List<GameObject>();
    //private KeyCode[] hotbarKeys = new KeyCode[] { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6};


    private void Start()
    {
        if (DungeonMode)
        {
            InventoryActived = false;
        SyncInventoryHotBar();
            SyncHotBar();
        }
        else
        {
            //Giving Hoe to player
            GiveItem(11);

            for (int i = 0; i < 5; i++)
            {
                GiveItem(4);
            }
        }

        AssignItems(_Items);
        weaponController = FindObjectOfType<WeaponController>();
        inventoryCanvas = transform.GetChild(0);
        inventoryCanvas.gameObject.active = false;
    }

    private void AssignItems(List<Item> items)
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i].id = i;
        }
    }

    private void Update()
    {
        //for (int i = 0; i < hotbarKeys.Length; i++)
        //{
        //    if (Input.GetKeyDown(hotbarKeys[i]))
        //    {
        //        GiveItem(_Items[i].id);
        //        return;
        //    }
        //}

        if (Input.GetKeyDown(KeyCode.Tab) && FindObjectOfType<PlayerMovement>().InOverWorld)
        {
            if (InventoryActived)
            {
                //Closing Inventory
                SyncHotBar();
                FindObjectOfType<PlayerMovement>().canWalk = true;
                inventoryCanvas.gameObject.active = false;
                InventoryActived = false;
            }
            else
            {
                //Opening Inventory
                FindObjectOfType<PlayerMovement>().StopWalking();
                SyncInventoryHotBar();
                inventoryCanvas.gameObject.active = true;
                InventoryActived = true;
            }
        }
        //ToolTip
        if (hoveringOverItem && selectedItemSlot.GetComponent<InventoryTile>().ContainingItem && holdingItemWithMouse == null)
            tooltip.isHovering = true;
        else
            tooltip.isHovering = false;
    }

    private void GiveItem(int id)
    {
        int inventoryNumber = GetFirstEmptyInventorySlot(id) ;
        Debug.Log(inventoryNumber);
        if (inventoryNumber == 100)
        {
            Debug.Log("Inventory Full");
            return;
        }
        InventorySlots[inventoryNumber].GetComponent<InventoryTile>().AssignNewItemToSlot(_Items[id]);
    }

    private int GetFirstEmptyInventorySlot(int id)
    {
        int inventoryNumber = 0;
        foreach (var item in InventorySlots)
        {
            if (item.GetComponent<InventoryTile>().ContainingItem && item.GetComponent<InventoryTile>().ItemHolding.id == id && item.GetComponent<InventoryTile>().ItemHolding.Stackable && item.transform.GetChild(0).GetComponent<ItemHolder>().count < maxStackedItems)
            {
                item.transform.GetChild(0).GetComponent<ItemHolder>().count++;
                item.transform.GetChild(0).GetComponent<ItemHolder>().RefreshCount();
                return inventoryNumber;
            }
            inventoryNumber++;
        }
        inventoryNumber = 0;
        foreach (var item in InventorySlots)
        {
            if (!item.GetComponent<InventoryTile>().ContainingItem)
                return inventoryNumber;
            inventoryNumber++;
        }
        return 100;
    }


    public void SyncHotBar()
    {
        //ClosingInventory
        for (int i = 0; i < HotBarSlots.Count; i++)
        {
            HotBarholder hotbar = weaponController.hotbarItems[i].GetComponent<HotBarholder>();
            hotbar.Item = HotBarSlots[i].GetComponent<InventoryTile>().ItemHolding;


            if (HotBarSlots[i].GetComponent<InventoryTile>().ItemHolding == null)
            {
                hotbar.Item = null;
                hotbar.ItemSprite = null;
                hotbar.state = ActionType.Attack;
                hotbar.transform.GetChild(0).GetComponent<Image>().color = new Color(1,1,1,0);
                hotbar.transform.GetChild(0).GetComponent<Image>().sprite = null;
                hotbar.count = 1;
                hotbar.RefreshCount();
            }
            else
            {
                hotbar.ItemSprite = hotbar.Item.itemSprite;
                hotbar.state = hotbar.Item.actionType;
                hotbar.transform.GetChild(0).GetComponent<Image>().sprite = hotbar.ItemSprite;
                hotbar.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                hotbar.count = HotBarSlots[i].transform.GetComponentInChildren<ItemHolder>().count;
                hotbar.RefreshCount();

                weaponController.SwitchHotBarItem(i);
            }
        }
        weaponController.SwitchHotBarItem(weaponController.hotbarSlot);
    }

    public void SyncInventoryHotBar()
    {
        foreach (var item in InventorySlots)
        {
            item.GetComponent<Image>().sprite = normalTile;
            item.GetComponent<InventoryTile>().Activated = false;
        }
        foreach (var item in HotBarSlots)
        {
            item.GetComponent<Image>().sprite = normalTile;
            item.GetComponent<InventoryTile>().Activated = false;
        }
        //OpeningInventory
        for (int i = 0; i < HotBarSlots.Count; i++)
        {
            Debug.Log(weaponController.hotbarItems[i].GetComponent<HotBarholder>());


            HotBarholder hotbar = weaponController.hotbarItems[i].GetComponent<HotBarholder>();
            HotBarSlots[i].GetComponent<InventoryTile>().ItemHolding = hotbar.Item;
            if (!hotbar.Item)
            {
                hotbar.ItemSprite = null;
                HotBarSlots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
                HotBarSlots[i].transform.GetChild(0).GetComponent<ItemHolder>().count = 1;
                HotBarSlots[i].transform.GetChild(0).GetComponent<ItemHolder>().RefreshCount();
                HotBarSlots[i].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0);
                HotBarSlots[i].GetComponent<InventoryTile>().ContainingItem = false;
            }
            else
            {

                HotBarSlots[i].GetComponent<InventoryTile>().ItemHolding.itemSprite = hotbar.Item.itemSprite;
                HotBarSlots[i].transform.GetChild(0).GetComponent<Image>().sprite = hotbar.Item.itemSprite;
                HotBarSlots[i].transform.GetChild(0).GetComponent<ItemHolder>().count = hotbar.count;
                HotBarSlots[i].transform.GetChild(0).GetComponent<ItemHolder>().RefreshCount();
            }
        }

    }

    public void UseItem()
    {
        HotBarholder hotbarScript = weaponController.currentHoldingItem.GetComponentInChildren<HotBarholder>();
        hotbarScript.count--;
        hotbarScript.RefreshCount();

        if(hotbarScript.count <= 0)
        {
            hotbarScript.count = 1;
            weaponController.currentHoldingItem.transform.GetChild(0).GetComponent<Image>().sprite = null;
            weaponController.currentHoldingItem.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0);
            weaponController.currentHoldingItem = null;
            hotbarScript.Item = null;
            hotbarScript.ItemSprite = null;
            hotbarScript.state = ActionType.Attack;
            weaponController.currentToolState = ActionType.Attack;
            weaponController.weaponRenderer.GetComponentInChildren<SpriteRenderer>().sprite = null;
        }
    }
}
