using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonToChangeGameobject : MonoBehaviour
{
    public GameObject page1Panel;
    public GameObject page2Panel;
    public GameObject page3Panel;


    public bool isPage1;
    public bool isnotPage1Nor2;

    // Update is called once per frame
    private void Start()
    {
        isPage1 = true;
    }
    void Update()
    {
        if(isPage1 == true)
        {
            page1Panel.SetActive(true);
            page2Panel.SetActive(false);
            page3Panel.SetActive(false);
        }
        else if (isnotPage1Nor2 == true)
        {
            page1Panel.SetActive(false);
            page2Panel.SetActive(false);
            page3Panel.SetActive(true);
        }
        else if (isPage1 == false && isnotPage1Nor2 == false)
        {
            page1Panel.SetActive(false);
            page2Panel.SetActive(true);
            page3Panel.SetActive(false);
        }
    }

    public void Open1()
    {
        isPage1 = true;
        isnotPage1Nor2 = false;
    }

    public void Open2()
    {
        isPage1 = false;
        isnotPage1Nor2 = false;
    }

    public void Open3()
    {
        isPage1 = false;
        isnotPage1Nor2 = true;
    }
}
