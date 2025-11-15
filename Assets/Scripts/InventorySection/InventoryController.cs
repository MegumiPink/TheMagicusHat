using UnityEngine;
using System.Collections.Generic;


public class InventoryController : MonoBehaviour
{
    private ItemDictionary itemDictionary;

    public GameObject inventoryPanel;
    public GameObject slotPrefab;
    public int slotCount;
    public GameObject[] itemPrefabs;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        itemDictionary = FindObjectOfType<ItemDictionary>();

        //for (int i = 0; i < slotCount; i++)
        //{
        //    Slot slot = Instantiate(slotPrefab, inventoryPanel.transform).GetComponent<Slot>();
        //  if (i < itemPrefabs.Length)
        //    {
        //        GameObject item = Instantiate(itemPrefabs[i],slot.transform);
        //        item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        //       slot.currentItem = item;
        //   }
        //}
    }
    public bool AddItem(GameObject itemPrefab)
    {
        foreach (Transform slotTransform in inventoryPanel.transform)
        {
            Slot slot = slotTransform.GetComponent<Slot>();
            if (slot != null && slot.currentItem == null)
            {
                GameObject newitem = Instantiate(itemPrefab, slot.transform);
                newitem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                slot.currentItem = newitem;
                return true;
            }
        }
        Debug.Log("FULL");
        return false;
    }
    public List<IventorySaveData> GetInventoryItems()
    {
     List<IventorySaveData> invData = new List<IventorySaveData>();
        foreach (Transform slotTransform in inventoryPanel.transform)
        {
            Slot slot = slotTransform.GetComponent<Slot>();
            if (slot.currentItem != null)
            {
                Item item = slot.currentItem.GetComponent<Item>();
                invData.Add(new IventorySaveData { itemID = item.ID, slotIndex = slotTransform.GetSiblingIndex() });
            }

        }
        return invData;

    }
    public void SetInventoryItems(List<IventorySaveData> iventorySaveData)
    {
        foreach (Transform child in inventoryPanel.transform)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < slotCount; i++)
        {
            Instantiate(slotPrefab, inventoryPanel.transform);
        }
        foreach (IventorySaveData data in iventorySaveData)
        {
            if (data.slotIndex < slotCount)
            {
              Slot slot = inventoryPanel.transform.GetChild(data.slotIndex).GetComponent<Slot>();
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
