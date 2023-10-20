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
    private bool attack4;
    private bool dash;

    StateManager states;

    void Start()
    {
        states = GetComponent<StateManager>();
    }

    void FixedUpdate()
    {
        horizontal = Input.GetAxisRaw("Horizontal" + playerInput);
        vertical = Input.GetAxisRaw("Vertical" + playerInput);
        attack1 = Input.GetButton("Fire1" + playerInput);
        attack2 = Input.GetButton("Fire2" + playerInput);
        attack3 = Input.GetButton("Fire3" + playerInput);
        attack4 = Input.GetButton("Fire4" + playerInput);

        states.horizontal = horizontal;
        states.vertical = vertical;
        states.attackL = attack1;
        states.attackM = attack2;
        states.attackH = attack3;
        states.attackS = attack4;
    }
}
