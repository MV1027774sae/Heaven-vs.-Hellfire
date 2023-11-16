using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnableControllerScript : MonoBehaviour
{
    [SerializeField] DetectController detectControllerScript;
    [SerializeField] PlayerController playerControllerScript;
    [SerializeField] InputHandler inputHandlerScript;
    [SerializeField] PlayerInput playerInput;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(detectControllerScript.isConnect && inputHandlerScript.playerInput == "" && inputHandlerScript.enabled == true)
        {
            playerControllerScript.enabled = true;
            playerInput.enabled = true;
        }
        else if (detectControllerScript.isConnect == false || inputHandlerScript.enabled == false)
        {
            playerControllerScript.enabled = false;
            playerInput.enabled = false;
        }
    }
}
