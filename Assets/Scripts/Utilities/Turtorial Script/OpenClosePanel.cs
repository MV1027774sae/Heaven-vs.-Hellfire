using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenClosePanel : MonoBehaviour
{
    [SerializeField] bool isOpen = false;
    [SerializeField] GameObject panel;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I)) isOpen = !isOpen;
        if(isOpen)
        {
            panel.SetActive(true);
        }
        else
        {
            panel.SetActive(false);
        }
    }
}
