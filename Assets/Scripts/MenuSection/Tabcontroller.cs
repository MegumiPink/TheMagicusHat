using UnityEngine;
using UnityEngine.UI;

public class Tabcontroller : MonoBehaviour
{
    public UnityEngine.UI.Image[] tabimages;
    public GameObject[] pages;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ActivateTab(0);
    }

    // Update is called once per frame
   public void ActivateTab(int tabno)
    {
        for (int i = 0; i < pages.Length;i++)
        {
            pages[i].SetActive(false);
            tabimages[i].color = Color.grey;
        }
        pages[tabno].SetActive(true);
        tabimages[tabno].color = Color.white;
    }
}
