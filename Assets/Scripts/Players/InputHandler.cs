using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public string playerInput;

    private float horizontal;
    private float vertical;
    private bool attack1;
    private bool attack2;
    private bool attack3;

    StateManager states;

    void Start()
    {
        states = GetComponent<StateManager>();
    }

    void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal" + playerInput);
        vertical = Input.GetAxis("Vertical" + playerInput);
        attack1 = Input.GetButton("Fire1" + playerInput);
        attack2 = Input.GetButton("Fire2" + playerInput);
        attack3 = Input.GetButton("Fire3" + playerInput);

        states.horizontal = horizontal;
        states.vertical = vertical;
        states.attackL = attack1;
        states.attackH = attack2;
        states.attackS = attack3;
    }
}
