using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Update()
    {
        foreach (var item in FindObjectsOfType<SpriteRenderer>())
        {
            item.sortingOrder = (int)(item.gameObject.transform.position.y * -100);
        }
    }
}
