using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnMenu : MonoBehaviour
{
    private void Awake()
    {
        Time.timeScale= 1f;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("intro");
        }

        //if(Input.GetKeyDown(KeyCode.End) && detectControllerScript.isConnect == false)
        //{
        //    detectControllerScript.isConnect = true;
        //}
        //else if (Input.GetKeyDown(KeyCode.End) && detectControllerScript.isConnect == true)
        //{
        //    detectControllerScript.isConnect = false;
        //}
    }
}
