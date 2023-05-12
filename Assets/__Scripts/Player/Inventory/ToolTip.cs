using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToolTip : MonoBehaviour
{
    public bool isHovering;
    [SerializeField] private Canvas myCanvas;
    [SerializeField] private TextMeshProUGUI nameText, extraText;

    void Start()
    {
        isHovering = false; 
            GetComponentInChildren<Image>().raycastTarget = false;
    }
    void Update()
    {
        if (isHovering)
        {
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, Input.mousePosition, myCanvas.worldCamera, out pos);
            transform.position = myCanvas.transform.TransformPoint(pos);
            nameText.text = FindObjectOfType<InventoryManager>().selectedItemSlot.GetComponent<InventoryTile>().ItemHolding.name;
            extraText.text = FindObjectOfType<InventoryManager>().selectedItemSlot.GetComponent<InventoryTile>().ItemHolding.extraText;
        }
        gameObject.transform.GetChild(0).gameObject.SetActive(isHovering); 
    }
}
