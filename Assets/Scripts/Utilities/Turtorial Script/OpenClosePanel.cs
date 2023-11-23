using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenClosePanel : MonoBehaviour
{
    [SerializeField] bool isOpen = false;
    [SerializeField] GameObject panel;

    [SerializeField] GameObject keyboardPanel;
    [SerializeField] GameObject controlPanel;

    [SerializeField] DetectController detectController;
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

        if(detectController.isConnect)
        {
            keyboardPanel.SetActive(false);
            controlPanel.SetActive(true);
        }
        else
        {
            keyboardPanel.SetActive(true);
            controlPanel.SetActive(false);
        }
    }
}
