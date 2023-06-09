using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FarmingScript : MonoBehaviour
{
    private WeaponController controller;
    private bool occupied;
    [SerializeField] private bool grown;
    private GameObject seedSpriteHolder;

    [SerializeField] private int seedState;
    private Animator anim;
    private int growthCounter;
    private void Start()
    {
        anim = GetComponent<Animator>();
        GetComponent<SpriteRenderer>().sprite = null;
        controller = FindObjectOfType<WeaponController>();
        seedSpriteHolder = transform.GetChild(0).GetChild(0).gameObject;
    }
    private void OnMouseOver()
    {
        if (Vector2.Distance(controller.transform.position, transform.position) < controller.farmingRange)
        {
            //Planting 
            if (controller.currentToolState == ActionType.Plant && controller.currentHoldingItem.GetComponent<HotBarholder>().count > 0)
            {
                if (!occupied)
                    GetComponent<SpriteRenderer>().sprite = controller.hoveringSprite;
                if (Input.GetMouseButton(0) && !occupied)
                {
                    anim.SetBool("Harvesting", false);
                    anim.SetTrigger("SpawnAnim");
                    growthCounter = controller._dayCount;
                    seedState = 0;
                    grown = false;
                    occupied = true;
                    seedSpriteHolder.GetComponent<SpriteRenderer>().sprite = controller.seedSpriteList[seedState];
                    GetComponent<SpriteRenderer>().sprite = null;
                    FindObjectOfType<InventoryManager>().UseItem();
                }
            }
            //Harvesting Crops
            if (controller.currentToolState == ActionType.Farm && grown)
            {
                GetComponent<SpriteRenderer>().sprite = controller.hoveringSprite;
                if (Input.GetMouseButton(0) && occupied)
                {
                    anim.SetBool("Harvesting", true);
                    GetComponent<SpriteRenderer>().sprite = null;
                }
            }
        }
    }
    private void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().sprite = null;
    }

    private void Update()
    {
        if (occupied && !grown)
        {
            if (controller._dayCount - 1 == growthCounter && !grown)
            {
                growthCounter = controller._dayCount;
                if (seedState >= controller.seedSpriteList.Count -1&& !grown)
                {
                    Debug.Log("Fully grown");
                    grown = true;
                    seedSpriteHolder.GetComponent<SpriteRenderer>().sprite = controller.seedSpriteList[controller.seedSpriteList.Count - 1];
                }
                else if (!grown)
                {
                    seedState++;
                    seedSpriteHolder.GetComponent<SpriteRenderer>().sprite = controller.seedSpriteList[seedState];
                }
            }
        }
    }

    public void ResetTile()
    {
        grown = false;
        seedSpriteHolder.GetComponent<SpriteRenderer>().sprite = null;
        seedState = 0;
        occupied = false;
    }
}
