using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [Header("Linking")]
    private WeaponController weaponController;
    public GameObject selectedItemSlot = null;
    public Sprite hoveringTile, normalTile;

    public List<Item> _Items = new List<Item>();

    [Header("Inventory")]
    public List<GameObject> InventorySlots = new List<GameObject>();
    public Item holdingItemWithMouse = null;
    public InventoryTile tileclicked;

    public bool InventoryActived;
    private Transform inventoryCanvas;

    //Temp
    [Header("Hotbar")]
    public List<GameObject> HotBarSlots = new List<GameObject>();
    private KeyCode[] hotbarKeys = new KeyCode[] { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6};


    private void Start()
    {
        AssignItems(_Items);
        InventoryActived = false;
        inventoryCanvas = transform.GetChild(0);
        inventoryCanvas.gameObject.active = false;
        weaponController = FindObjectOfType<WeaponController>();
        SyncHotBar();
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
        for (int i = 0; i < hotbarKeys.Length; i++)
        {
            if (Input.GetKeyDown(hotbarKeys[i]))
            {
                GiveItem(_Items[i].id);
                return;
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab))
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
    }

    private void GiveItem(int id)
    {
        int inventoryNumber = GetFirstEmptyInventorySlot() ;
        if (inventoryNumber == 100)
        {
            Debug.Log("Inventory Full");
            return;
        }
        InventorySlots[inventoryNumber].GetComponent<InventoryTile>().AssignNewItemToSlot(_Items[id]);
    }

    private int GetFirstEmptyInventorySlot()
    {
        int inventoryNumber = 0;
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
                Debug.Log("SLotFilled");
            }
            else
            {
                hotbar.ItemSprite = hotbar.Item.itemSprite;
                hotbar.state = hotbar.Item.actionType;
                hotbar.transform.GetChild(0).GetComponent<Image>().sprite = hotbar.ItemSprite;
                hotbar.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                Debug.Log("Slot Empty");
                weaponController.SwitchHotBarItem(i);
            }
        }
    }

    public void SyncInventoryHotBar()
    {
        //OpeningInventory
        for (int i = 0; i < HotBarSlots.Count; i++)
        {
            HotBarholder hotbar = weaponController.hotbarItems[i].GetComponent<HotBarholder>();
            HotBarSlots[i].GetComponent<InventoryTile>().ItemHolding = hotbar.Item;
            if (!hotbar.Item)
            {
                hotbar.ItemSprite = null;
                return;
            }
            HotBarSlots[i].GetComponent<InventoryTile>().ItemHolding.itemSprite = hotbar.Item.itemSprite;
            HotBarSlots[i].transform.GetChild(0).GetComponent<Image>().sprite = hotbar.Item.itemSprite;
        }
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

    }
}
