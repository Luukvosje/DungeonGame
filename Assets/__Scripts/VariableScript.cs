using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableScript : MonoBehaviour
{
    public static VariableScript instance;

    public int money;
    public int crops;
    public int day;

    private void Awake()
    {

        DontDestroyOnLoad(this);
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void Start()
    {
        money = PlayerPrefs.GetInt("Money");
        crops = PlayerPrefs.GetInt("Crops");
        day = PlayerPrefs.GetInt("Day");
    }

    public void Update()
    {

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            money++;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            crops++;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            day++;
        }

    }
    
    public void Save()
    {
        PlayerPrefs.SetInt("Money", money);
        PlayerPrefs.SetInt("Crops", crops);
        PlayerPrefs.SetInt("Day", day);
    }

    public void Load()
    {
        money = PlayerPrefs.GetInt("Money");
        crops = PlayerPrefs.GetInt("Crops");
        day = PlayerPrefs.GetInt("Day");
    }

    public void Sell()
    {
        if (crops > 0)
        {
            crops--;
            money += 5;
        }
        
    }
}
