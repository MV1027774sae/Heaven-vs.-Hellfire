using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public bool canDash = true;
    [SerializeField] bool isDashing;
    [SerializeField] float dashTime = 0.2f;
    [SerializeField] float dashingPower = 10f;
    [SerializeField] float dashCoolDown = 1f;

    [SerializeField] Rigidbody2D rb;
    [SerializeField] StateManager stateManagerScripts;

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
        if (isDashing)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Y) && canDash) //Press Y twice to dash
        {
            if(keyPressed)
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
                rb.velocity = new Vector2(-transform.localScale.x * dashingPower, 0f);
            }
            else //If not then dash foward
            {
                rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
            }
        }
        else //Same goes to here
        {
            if (Input.GetKey(KeyCode.D) && stateManagerScripts.onGround == false)
            {
                rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
            }
            else
            {
                rb.velocity = new Vector2(-transform.localScale.x * dashingPower, 0f);
            }
        }
        yield return new WaitForSeconds(dashTime);
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashCoolDown);
        canDash = true;
    }
}
