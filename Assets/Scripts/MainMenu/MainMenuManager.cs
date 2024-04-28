using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private int activeElement;
    [SerializeField] ButtonRef[] menuOptions;
    [SerializeField] GameObject[] pages;


    void Start()
    {
        
    }
    
    void Update()
    {
        //indicate the selected option
        menuOptions[activeElement].selected = true;
        LoadPanel();

        //change the selected option based on input
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            menuOptions[activeElement].selected = false;
            if (activeElement > 0)
            {
                activeElement--;
            }
            else
            {
                activeElement = menuOptions.Length - 1;
            }
        }

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            menuOptions[activeElement].selected = false;
            if (activeElement < menuOptions.Length - 1)
            {
                activeElement++;
            }
            else
            {
                activeElement = 0;
            }
        }
    }

    public void LoadPanel()
    {
        menuOptions[activeElement].transform.localScale *= 1f;

        if (activeElement == 0)
        {
            pages[0].gameObject.SetActive(true);
            pages[1].gameObject.SetActive(false);
            pages[2].gameObject.SetActive(false);
        }
        else if (activeElement == 1)
        {
            pages[0].gameObject.SetActive(false);
            pages[1].gameObject.SetActive(true);
            pages[2].gameObject.SetActive(false);
        }
        else if (activeElement == 2)
        {
            pages[0].gameObject.SetActive(false);
            pages[1].gameObject.SetActive(false);
            pages[2].gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("????");
        }
    }
}
