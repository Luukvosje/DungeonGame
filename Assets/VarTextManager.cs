using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VarTextManager : MonoBehaviour
{
    public TMP_Text moneytext;
    public TMP_Text croptext;
    public TMP_Text daytext;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moneytext.text = "Money: " + VariableScript.instance.money.ToString();
        croptext.text = "Crops: " + VariableScript.instance.crops.ToString();
        daytext.text = "Day: " + VariableScript.instance.day.ToString();
    }
}
