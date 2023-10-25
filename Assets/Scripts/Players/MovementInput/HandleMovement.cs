using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandleMovement : MonoBehaviour
{
    Rigidbody2D rb;
    StateManager states;
    HandleAnimations anim;
    InputHandler inputHandler;

    //Controller Support
    PlayerController playerController;
    [SerializeField] PlayerInput playerInput;

    public float acceleration = 30;
    public float airAcceleration = 15;
    public float maxSpeed = 60;
    public float jumpSpeed = 5;
    public float jumpDuration = 5;
    public bool justJumped;
    public float actualSpeed;
    public bool canVariableJump;
    public float jmpTimer;

    public bool useController;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        states = GetComponent<StateManager>();
        anim = GetComponent<HandleAnimations>();
        inputHandler = GetComponent<InputHandler>();
        playerController = GetComponent<PlayerController>();
        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        if(inputHandler.playerInput == "")
        {
            useController = true;
            if (useController == true)
            {
                playerController.enabled = true;
                playerInput.enabled = true;
            }
        }
        else
        {
            playerInput.enabled = false;
            playerController.enabled = false;
        }

        if (!states.dontMove)
        {
            HorizontalMovement();
            Jump();
        }
    }

    public void HorizontalMovement()
    {
        actualSpeed = this.maxSpeed;

        if (states.onGround && !states.currentlyAttacking && !states.blocking)
        {
            rb.AddForce(new Vector2((states.horizontal * actualSpeed) - rb.velocity.x * this.acceleration, 0));

            if (states.onGround && states.lookRight && states.horizontal < 0)
            {
                states.guard = true;
            }
            else if (states.onGround && !states.lookRight && states.horizontal > 0)
            {
                states.guard = true;
            }
            else if (!states.onGround || states.horizontal == 0)
            {
                states.guard = false;
            }  
        }

        //in case there's sliding
        if (states.horizontal == 0 && states.onGround)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    public void Jump()
    {
        if (states.vertical > 0)
        {
            if (!justJumped)
            {
                justJumped = true;

                if (states.onGround)
                {
                    anim.JumpAnim();

                    rb.velocity = new Vector3(rb.velocity.x, this.jumpSpeed);
                    jmpTimer = 0;
                    canVariableJump = true;
                }
            }
            else
            {
                if (canVariableJump)
                {
                    jmpTimer += Time.deltaTime;

                    if (jmpTimer < this.jumpDuration / 1000)
                    {
                        rb.velocity = new Vector3(rb.velocity.x, this.jumpSpeed);
                    }
                }
            }
        }
        else
        {
            justJumped = false;
        }
    }

    public void AddVelocityOnCharacter(Vector3 direction, float timer)
    {
        StartCoroutine(AddVelocity(timer, direction));
    }

    IEnumerator AddVelocity(float timer, Vector3 direction)
    {
        float t = 0;

        while (t < timer)
        {
            t += Time.deltaTime;

            rb.velocity = direction;
            yield return null;
        }
    }
}
