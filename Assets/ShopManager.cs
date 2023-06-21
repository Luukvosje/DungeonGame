using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public GameObject shop;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseShop()
    {
        shop.SetActive(false);
    }

    public void BuyHelmet1()
    {
        // check if enough gold then remove gold
        if (VariableScript.instance.money > 0)
        {
            VariableScript.instance.money -= 1;
            
        }
        // add to invertory

    }
    public void BuyHelmet2()
    {
        // check if enough gold then remove gold

        // add to invertory
    }
    public void BuyHelmet3()
    {
        // check if enough gold then remove gold

        // add to invertory
    }


}
