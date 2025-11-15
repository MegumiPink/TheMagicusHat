using UnityEngine;

public class CraftingMenu : MonoBehaviour,IInteractable
{
    public GameObject craftingMenu;
    public GameObject caldround;
    public GameObject hotbarController;
    public bool IsBoiling;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    public bool CanInteract()
    {
        Closecraftingmenu();
        return !IsBoiling;


    }

    public void Interact()
    {
        if (!CanInteract()) 
        Opencraftmenu(); // openmenucrafting
        Debug.LogWarning("craft its open");  


    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Closecraftingmenu();
        }

    }

    public void Opencraftmenu()
    {
        hotbarController.SetActive(false);
        craftingMenu.SetActive(true);
        caldround.SetActive(false);
        PauseController.SetPause(true);
    }

    public void Closecraftingmenu()
    {
            craftingMenu.SetActive(false);
            caldround.SetActive(true);
            PauseController.SetPause(false);
    }

}
