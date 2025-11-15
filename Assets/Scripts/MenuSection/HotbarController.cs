using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HotbarController : MonoBehaviour
{
    public GameObject hotbarPanel;
    public GameObject slotPrefab;
    public int slotCount = 10; // 1-0 on the keyboard
    private ItemDictionary itemDictionary;
    private Key[] hotbarKeys;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
       itemDictionary = FindObjectOfType<ItemDictionary>();

        //Hotbar key  base onslot count
        hotbarKeys = new Key[slotCount];
        for (int i = 0; i < slotCount; i++)  
        {
            hotbarKeys[i] = i < 9 ? (Key)((int)Key.Digit1 + i) : Key.Digit0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < slotCount; i++)
        {
            if (Keyboard.current[hotbarKeys[i]].wasPressedThisFrame)
            {
                UseItemInSlot(i);
            }
        }
    }

    void UseItemInSlot(int index)
    {
       Slot slot = hotbarPanel.transform.GetChild(index).GetComponent<Slot>();
        if (slot.currentItem != null)
        {
          Item item = slot.currentItem.GetComponent<Item>();
            item.UseItem();

        }
        
    }

    public List<IventorySaveData> GetHotbarItems()
    {
        List<IventorySaveData> hotbarData = new List<IventorySaveData>();
        foreach (Transform slotTransform in hotbarPanel.transform)
        {
            Slot slot = slotTransform.GetComponent<Slot>();
            if (slot.currentItem != null)
            {
                Item item = slot.currentItem.GetComponent<Item>();
                hotbarData.Add(new IventorySaveData { itemID = item.ID, slotIndex = slotTransform.GetSiblingIndex() });
            }

        }
        return hotbarData;

    }
    public void SetHotbarItems(List<IventorySaveData> iventorySaveData)
    {
        foreach (Transform child in hotbarPanel.transform)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < slotCount; i++)
        {
            Instantiate(slotPrefab, hotbarPanel.transform);
        }
        foreach (IventorySaveData data in iventorySaveData)
        {
            if (data.slotIndex < slotCount)
            {
                Slot slot = hotbarPanel.transform.GetChild(data.slotIndex).GetComponent<Slot>();
                GameObject itemPrefab = itemDictionary.GetItemPrefab(data.itemID);
                if (itemPrefab != null)
                {
                    GameObject item = Instantiate(itemPrefab, slot.transform);
                    item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                    slot.currentItem = item;
                }
            }
        }
    }

}
