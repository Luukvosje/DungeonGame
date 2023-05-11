using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmingManager : MonoBehaviour
{
    public List<FarmingScript> farmingtiles = new List<FarmingScript>();

    public void ResetfarmingGrid()
    {
        foreach (var item in farmingtiles)
        {
            item.gameObject.GetComponent<SpriteRenderer>().sprite = null;
        }
    }
}
