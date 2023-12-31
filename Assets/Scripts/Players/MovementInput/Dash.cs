using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [Header("Dashing")]
    public bool canDash = true;
    [SerializeField] bool isDashing;
    [SerializeField] float dashTime = 0.2f;
    [SerializeField] float dashingPower = 10f;
    [SerializeField] float dashCoolDown = 1f;

    [Header("Grabing other component")]
    [SerializeField] Rigidbody2D rb;
    [SerializeField] StateManager stateManagerScripts;
    [SerializeField] InputHandler inputHandlerScript;


    [Header("Check if key press twice")]
    //Check if the key is press twice
    public bool keyPressed = false;
    private float doublePressTime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Player 1???
        if (stateManagerScripts.horizontal != 0 && canDash && inputHandlerScript.playerInput == "") //Press horizontal input twice twice to dash
        {
            if (keyPressed && stateManagerScripts.currentlyAttacking == false && stateManagerScripts.crouch == false && stateManagerScripts.onGround == false)
            {
                StartCoroutine(Dashing());
                keyPressed = false;
            }
            else
            {
                keyPressed = true;
                Invoke("ResetKeyPressed", doublePressTime); //Reset the key when press twice
            }
        }
        //Player 2???
        else if (stateManagerScripts.horizontal != 0 && canDash && inputHandlerScript.playerInput == "1")
        {
            if (keyPressed && stateManagerScripts.currentlyAttacking == false && stateManagerScripts.crouch == false && stateManagerScripts.onGround == false)
            {
                StartCoroutine(Dashing2());
                keyPressed = false;
            }
            else
            {
                keyPressed = true;
                Invoke("ResetKeyPressed", doublePressTime); //Reset the key when press twice
            }
        }
    }

    private void FixedUpdate()
    {
        if(isDashing)
        {
            return;
        }
    }

    private void ResetKeyPressed()
    {
        keyPressed = false;
    }

    IEnumerator Dashing() //Call the dash function
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f; //Set character gravity to zero.
        if(stateManagerScripts.lookRight)
        {
            if (Input.GetKey(KeyCode.A) && stateManagerScripts.onGround == false) //Check if they in mid air and press backward
            {
                rb.velocity = Vector2.left * dashingPower;
            }
            else //If not then dash foward
            {
                rb.velocity = Vector2.right * dashingPower;
            }
        }
        else //Same goes to here
        {
            if (Input.GetKey(KeyCode.D) && stateManagerScripts.onGround == false)
            {
                rb.velocity = Vector2.right * dashingPower;
            }
            else
            {
                rb.velocity = Vector2.left * dashingPower;
            }
        }
        yield return new WaitForSeconds(dashTime);
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashCoolDown);
        canDash = true;
    }

    IEnumerator Dashing2() //Call the dash function for player 2
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f; 
        if (stateManagerScripts.lookRight)
        {
            if (Input.GetKey(KeyCode.LeftArrow) && stateManagerScripts.onGround == false) 
            {
                rb.velocity = Vector2.left * dashingPower;
            }
            else 
            {
                rb.velocity = Vector2.right * dashingPower;
            }
        }
        else 
        {
            if (Input.GetKey(KeyCode.RightArrow) && stateManagerScripts.onGround == false)
            {
                rb.velocity = Vector2.right * dashingPower;
            }
            else
            {
                rb.velocity = Vector2.left * dashingPower;
            }
        }
        yield return new WaitForSeconds(dashTime);
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashCoolDown);
        canDash = true;
    }
}
