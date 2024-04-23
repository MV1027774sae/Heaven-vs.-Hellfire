using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectWhatButtonPress : MonoBehaviour
{
    // List of keyboard buttons to check
    public KeyCode[] keyboardButtons;
    public GameObject[] displayText;

    void Update()
    {
        // Check each keyboard button in the list
        foreach (KeyCode key in keyboardButtons)
        {
            if (Input.GetKeyDown(keyboardButtons[2]))
            {
                displayText[0].SetActive(true);
                displayText[1].SetActive(false);
                displayText[2].SetActive(false);
                displayText[3].SetActive(false);
            }
            else if (Input.GetKeyDown(keyboardButtons[3]))
            {
                displayText[0].SetActive(false);
                displayText[1].SetActive(true);
                displayText[2].SetActive(false);
                displayText[3].SetActive(false);
            }
            else if (Input.GetKeyDown(keyboardButtons[5]))
            {
                displayText[0].SetActive(false);
                displayText[1].SetActive(false);
                displayText[2].SetActive(true);
                displayText[3].SetActive(false);
            }
            else if (Input.GetKeyDown(keyboardButtons[4]))
            {
                displayText[0].SetActive(false);
                displayText[1].SetActive(false);
                displayText[2].SetActive(false);
                displayText[3].SetActive(true);
            }
        }
    }
}
