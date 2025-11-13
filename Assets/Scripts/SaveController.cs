using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    private string saveLocation;
    private InventoryController inventoryController;
    private Chest[] chests;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Define save location
        InitializeComponents();
        LoadGame();
    }

    private void InitializeComponents()
    {
        saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");
        inventoryController = FindObjectOfType<InventoryController>();
        chests = FindObjectsOfType<Chest>();
    }
    public void SaveGame()
    {
        Savedata saveData = new Savedata
        {
            playerPosition = GameObject.FindWithTag("Player").transform.position,
            iventorySaveData = inventoryController.GetInventoryItems(),
            chestSaveData = GetChestsState()

        };

        File.WriteAllText(saveLocation, JsonUtility.ToJson(saveData));
    }

    private List<ChestSaveData> GetChestsState()
    {
        List<ChestSaveData> chestStates = new List<ChestSaveData>();
        foreach (Chest chest in chests)
        {
            ChestSaveData chestSaveData = new ChestSaveData
            {
                chestID = chest.ChestID,
                isOpened = chest.IsOpened
            };
            chestStates.Add(chestSaveData);
        }
        return chestStates;
    }
    public void LoadGame()
    {
        if (File.Exists(saveLocation))
        {
            Savedata saveData = JsonUtility.FromJson<Savedata>(File.ReadAllText(saveLocation));
            GameObject.FindWithTag("Player").transform.position = saveData.playerPosition;
            inventoryController.SetInventoryItems(saveData.iventorySaveData);
            LoadChestState (saveData.chestSaveData);
        }
        else
        {
           SaveGame();
            inventoryController.SetInventoryItems(new List<IventorySaveData>());
        }
    }

    private void LoadChestState(List<ChestSaveData> chestStates)
    {
            foreach (Chest chest in chests)
            {
               ChestSaveData chestSaveData = chestStates.FirstOrDefault(c => c.chestID == chest.ChestID);
                if (chestSaveData != null)
                {
                     chest.SetOpened(chestSaveData.isOpened);
            }
        }
        
    }
}

