using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HotBarholder : MonoBehaviour
{
    public Sprite ItemSprite;
    public Item Item;
    public ActionType state;
    public int count = 1;

    public TextMeshProUGUI countText;
    public void RefreshCount()
    {
        countText.text = count.ToString();
        bool textActive = count > 1;
        countText.gameObject.SetActive(textActive);
    }

}
