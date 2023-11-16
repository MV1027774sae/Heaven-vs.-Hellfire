using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotifyControllConnect : MonoBehaviour
{
    [SerializeField] DetectController detectControllerScript;
    [SerializeField] GameObject textOn;
    [SerializeField] GameObject textOff;

    // Update is called once per frame
    void Update()
    {
        if(detectControllerScript.isConnect == true)
        {
            textOn.SetActive(true);
            textOff.SetActive(false);
        }
        else
        {
            textOn.SetActive(false);
            textOff.SetActive(true);
        }
    }
}
