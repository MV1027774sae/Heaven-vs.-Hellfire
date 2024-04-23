using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayAttackMove : MonoBehaviour
{
    [SerializeField] KeyCode keyToLight = KeyCode.Space;
    [SerializeField] KeyCode keyToMedium = KeyCode.Space;
    [SerializeField] KeyCode keyToSpecial = KeyCode.Space;
    [SerializeField] KeyCode keyToHeavy = KeyCode.Space;

    // Update is called once per frame
    void Update()
    {
        ShowMessage();
        HideMessage();
    }

    void ShowMessage()
    {
        if (Input.GetKeyDown(keyToLight))
        {
            Debug.Log("light");

        }
        else if (Input.GetKeyDown(keyToMedium))
        {
            Debug.Log("medium");

        }
        else if (Input.GetKeyDown(keyToSpecial))
        {
            Debug.Log("special");

        }
        else if (Input.GetKeyDown(keyToHeavy))
        {
            Debug.Log("heavy");
        }
        else
        {
            Debug.Log("turn off");
        }
    }

    void HideMessage()
    {
        if (Input.GetKeyUp(keyToLight))
        {
            Debug.Log("light out");

        }
        else if (Input.GetKeyUp(keyToMedium))
        {
            Debug.Log("medium out");

        }
        else if (Input.GetKeyUp(keyToSpecial))
        {
            Debug.Log("special out");

        }
        else if (Input.GetKeyUp(keyToHeavy))
        {
            Debug.Log("heavy out");
        }
        else
        {
            Debug.Log("turn off out");
        }
    }
}
