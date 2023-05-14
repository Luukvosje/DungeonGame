using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;


public class WeaponController : MonoBehaviour
{
    [Header("Linking")]
    [SerializeField] private GameObject hotbar;
    public GameObject weaponRenderer;
    public List<GameObject> hotbarItems = new List<GameObject>();
    public static WeaponController instance;

    [Header("Tool Manager")]
    public ActionType currentToolState;
    public GameObject currentHoldingItem;
    [SerializeField] private int hotbarNumber;
    private Vector3 oldPos, newPos, oldRot, newRot;
    private KeyCode[] hotbarKeys = new KeyCode[] { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6 };

    [Header("Farming Manager")]
    public Sprite hoveringSprite;
    public List<Sprite> seedSpriteList = new List<Sprite>();
    public float farmingRange = 5;

    [Header("Manager")]
    public int _dayCount;
    public int hotbarSlot =1 ;

    private void Awake()
    {
        instance = this;
        _dayCount = 0;
    }
    private void Start()
    {
        if (!currentHoldingItem)
            SwitchHotBarItem(0);

    }

    private void Update()
    {
        //Hotbar
        for (int i = 0; i < hotbarKeys.Length; i++)
        {
            if (Input.GetKeyDown(hotbarKeys[i]))
            {
                SwitchHotBarItem(i);
                return;
            }
        }

        //States
        switch(currentToolState)
        {
            case ActionType.Plant:
                Debug.Log("Farming Mode");
                break;
            case ActionType.Farm:
                Debug.Log("Harversting Mode");
                break;
            case ActionType.Attack:
                Debug.Log("Fighting Mode");
                break;
        }

        if (Input.GetKeyDown(KeyCode.K))
            _dayCount++;
    }

    public void ChangeWeaponPos(int newLook)
    {
        oldPos = weaponRenderer.transform.localPosition;
        oldRot = weaponRenderer.transform.eulerAngles;
        switch (newLook)
        {
            case 0:

                //Left
                newPos = new Vector3(-1, 0);
                newRot = new Vector3(0, 0, 90);
                break;
            case 1:
                //Right
                newPos = new Vector3(1, 0);
                newRot = new Vector3(0, 0, -90);
                break;
            case 2:
                //front
                newPos = new Vector3(0, 1);
                newRot = new Vector3(0, 0, 0);
                break;
            case 3:
                //back
                newPos = new Vector3(0, -1);
                newRot = new Vector3(0, 0, 180);
                break;
        }
        StartCoroutine(ChangePos());
    }

    private IEnumerator ChangePos()
    {
        float duration = 0.1f;
        float currentTime = 0f;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            weaponRenderer.transform.localPosition = Vector2.Lerp(oldPos, newPos, currentTime / duration);
            weaponRenderer.transform.eulerAngles = Vector3.Lerp(oldRot, newRot, currentTime / duration);
            yield return null;
        }
    }
    public void SwitchHotBarItem(int hotBarInt)
    {
        hotbarSlot = hotBarInt;
        currentHoldingItem = hotbarItems[hotBarInt];
        weaponRenderer.GetComponentInChildren<SpriteRenderer>().sprite = currentHoldingItem.GetComponent<HotBarholder>().ItemSprite;
        currentToolState = currentHoldingItem.GetComponent<HotBarholder>().state;
        hotbarNumber = hotBarInt;
        foreach (var item in hotbarItems)
        {
            item.GetComponent<Image>().sprite = FindObjectOfType<InventoryManager>().normalTile;
        }
        currentHoldingItem.GetComponent<Image>().sprite = FindObjectOfType<InventoryManager>().hoveringTile;
        if(FindObjectOfType<PlayerMovement>().InOverWorld)
            FindObjectOfType<FarmingManager>().ResetfarmingGrid();
    }
}
