using UnityEngine;

public class Cauldron : MonoBehaviour, IInteractable
{
    public bool IsBoiling;
    public GameObject ItemPrefab;
    public Sprite Boiling;
    public CraftingMenu craftingMenu;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public bool CanInteract()
    {
       return !IsBoiling;
    }

    public void Interact()
    {
        if (!CanInteract()) 
        StartBoiling();
        { 
        
        if (craftingMenu != null)
        {
                craftingMenu.Opencraftmenu();
            }
        else
        {
            Debug.LogWarning("esqueces te do obejto no inspetor");
        }
    }
}
    
    private void StartBoiling()
    {
        SetBoiling(true);
        if (ItemPrefab)
        {
            Instantiate(ItemPrefab, transform.position + Vector3.down, Quaternion.identity);
        }
    }

    public void SetBoiling(bool boiling)
    {
        if (IsBoiling = boiling)
        {
            GetComponent<SpriteRenderer>().sprite = Boiling;
        }
    }
}
