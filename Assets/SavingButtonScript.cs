using System.IO;
using UnityEngine;

public class ButtonSavingScript : MonoBehaviour
{
    private string savePath; // Path to save the data file

    private void Awake()
    {
        savePath = Path.Combine(Application.persistentDataPath, "saveData.txt");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveData();
        }
    }

    private void SaveData()
    {
        // Get the player's money and inventory data
        Player player = FindObjectOfType<Player>();
        int money = player.GetMoney();
        string[] inventory = player.GetInventory();

        // Create a data object to hold the save data
        SaveData saveData = new SaveData(money, inventory);

        // Serialize the data to JSON format
        string jsonData = JsonUtility.ToJson(saveData);

        // Save the data to a file
        File.WriteAllText(savePath, jsonData);

        Debug.Log("Data saved!");
    }
}

[System.Serializable]
public class SaveData
{
    public int money;
    public string[] inventory;

    public SaveData(int money, string[] inventory)
    {
        this.money = money;
        this.inventory = inventory;
    }
}

public class Player : MonoBehaviour
{
    private int money;
    private string[] inventory;

    public int GetMoney()
    {
        return money;
    }

    public string[] GetInventory()
    {
        return inventory;
    }

    // Example method to update money and inventory
    public void UpdateData(int newMoney, string[] newInventory)
    {
        money = newMoney;
        inventory = newInventory;
    }
}
