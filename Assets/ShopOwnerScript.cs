using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopOwnerScript : MonoBehaviour
{
    public Vector3 finishPlace;
    public Vector3 currentPos;
    // Start is called before the first frame update
    void Start()
    {
        currentPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(gameObject.transform.position, new Vector2(currentPos.x, finishPlace.y), .01f);
    }

    private void Awake()
    {
       
    }
}
