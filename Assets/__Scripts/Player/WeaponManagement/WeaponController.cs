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
    [HideInInspector] public Vector3 oldPos, newPos, oldRot, newRot;
    private KeyCode[] hotbarKeys = new KeyCode[] { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6 };

    [Header("Farming Manager")]
    public Sprite hoveringSprite;
    public List<Sprite> seedSpriteList = new List<Sprite>();
    public float farmingRange = 5;

    [Header("Manager")]
    public int _dayCount;
    public int hotbarSlot =1 ;

    [Header("WeaponManager")]
    [SerializeField] private SwordSlashEffect slashEffect;
    [SerializeField] private Animator weaponAnim;
    public float slashMoveSpeed;
    public bool attacking = false;
    private float reloadSpeedForWeapon;
    private float attackspeed;
    [SerializeField] private bool alreadyAttacking = false;

    private void Awake()
    {
        instance = this;
        _dayCount = 0;
    }
    private void Start()
    {
        alreadyAttacking = false;
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
                if (Input.GetMouseButtonDown(0) && alreadyAttacking == false && currentHoldingItem.GetComponent<HotBarholder>().Item && !FindObjectOfType<InventoryManager>().InventoryActived)
                {
                    //LeftClick
                    StopAllCoroutines();
                    StartCoroutine(Attacking());
                }
                break;
        }

        if (Input.GetKeyDown(KeyCode.K))
            _dayCount++;
    }

    private IEnumerator Attacking()
    {
        alreadyAttacking = true;
        attacking = true;
        weaponAnim.speed = currentHoldingItem.GetComponent<HotBarholder>().Item.AnimationSpeed;
        Quaternion rotation = weaponRenderer.transform.rotation;
        rotation *= Quaternion.Euler(0, 0, 90);
        Instantiate(slashEffect, weaponRenderer.transform.position, rotation);
        if (weaponAnim.gameObject.activeSelf)
        {
            weaponAnim.SetTrigger("Attack");
        }
        yield return new WaitForSeconds(currentHoldingItem.GetComponent<HotBarholder>().Item.AttackSpeed);
        attacking = false;
        alreadyAttacking = false;
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
