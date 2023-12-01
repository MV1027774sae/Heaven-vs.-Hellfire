using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnMenu : MonoBehaviour
{
    [SerializeField] DetectController detectControllerScript;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Home))
        {
            SceneManager.LoadScene("intro");
        }

        if(Input.GetKeyDown(KeyCode.End) && detectControllerScript.isConnect == false)
        {
            detectControllerScript.isConnect = true;
        }
        else if (Input.GetKeyDown(KeyCode.End) && detectControllerScript.isConnect == true)
        {
            detectControllerScript.isConnect = false;
        }
    }
}
