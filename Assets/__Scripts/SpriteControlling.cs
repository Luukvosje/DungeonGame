using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteControlling : MonoBehaviour
{
    [SerializeField] private LayerMask Collsion;
    void Update()
    {
        foreach (var item in FindObjectsOfType<SpriteRenderer>())
        {
            if (item.sortingLayerID != Collsion)
                item.sortingOrder = (int)(item.gameObject.transform.position.y * -100);
        }
    }
}
