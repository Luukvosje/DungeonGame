using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteControlling : MonoBehaviour
{
    [SerializeField] private LayerMask Collsion;
    [SerializeField] private Texture2D CursorSprite;

    private void Start()
    {
        //Cursor.SetCursor(CursorSprite, Vector2.zero, CursorMode.Auto);
    }
    void Update()
    {
        foreach (var item in FindObjectsOfType<SpriteRenderer>())
        {
            if (item.sortingLayerID != Collsion)
                item.sortingOrder = (int)(item.gameObject.transform.position.y * -100);
        }
    }
}
