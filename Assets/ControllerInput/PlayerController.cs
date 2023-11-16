using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerController : MonoBehaviour
{
    [Header("Grabing other component")]
    public Rigidbody2D rb;
    [SerializeField] StateManager states;
    [SerializeField] HandleMovement handleMovement;
    private PlayerInputAction playerInput;

    [Header("Animation")]
    [SerializeField] HandleAnimations anim;
    public Animator animate;

    [Header("Movement Speed and Jump")]
    public float speed;
    private float horizontal;
    public float jumpForce = 5.0f; // Adjust this value as needed.
    private bool isJumping = false;

    [Header("Dashing")]
    public bool canDash = true;
    public bool keyPressed = false;
    [SerializeField] bool isDashing;
    [SerializeField] float dashTime = 0.2f;
    [SerializeField] float dashingPower = 10f;
    [SerializeField] float dashCoolDown = 1f;


    [Header("Crouch")]
    public bool isCrouch = false;



    void Awake()
    {
        animate = GetComponentInChildren<Animator>();
        playerInput = new PlayerInputAction();
        speed = 143f;
    }

    //Help/Updating moving
    private void FixedUpdate()
    {
        //Copy and Paste from HandleMovement Script
        if (states.onGround && !states.currentlyAttacking && !states.blocking)
        {
            rb.AddForce(new Vector2((horizontal * speed) - rb.velocity.x * handleMovement.acceleration, 0));
            float movement = Mathf.Abs(horizontal);
            animate.SetFloat("Movement", movement);

            if (states.onGround && states.lookRight && horizontal < 0)
            {
                states.guard = true;
            }
            else if (states.onGround && !states.lookRight && horizontal > 0)
            {
                states.guard = true;
            }
            else if (!states.onGround || horizontal == 0)
            {
                states.guard = false;
            }
        }

        //in case there's sliding
        if (horizontal == 0 && states.onGround)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        //Checking if the method activate the condition then adjust speed and crouch
        if(isCrouch)
        {
            states.crouch= true;
            speed = 70f;
        }
        else
        {
            states.crouch = false;
            speed = 143f;
        }

        if (isDashing)
        {
            return;
        }
    }

    //Call Jumping Method
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Jump();
        }
    }

    //Call Moving
    public void OnMove(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }

    public void OnAttackLight(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            states.attackL = true;
            Debug.Log("ATTACK!");
        }
    }
    public void OnAttackMedium(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            states.attackM = true;
            Debug.Log("ATTACK MEDIUAM!");
        }
    }
    public void OnAttackHeavy(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            states.attackH = true;
            Debug.Log("ATTACK HEAVY!");
        }
    }
    public void OnAttackSpecial(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            states.attackS = true;
            Debug.Log("ATTACK SPEACIAL!");
        }
    }

    //This method function Crouch
    private void OnEnable()
    {
        playerInput.Enable();
        playerInput.PlayerControl.Crouch.performed += OnCrouchPerform;
        playerInput.PlayerControl.Crouch.canceled += OnCrouchCancelled;

    }
    private void OnDisable()
    {
        playerInput.Disable();
        playerInput.PlayerControl.Crouch.performed -= OnCrouchPerform;
        playerInput.PlayerControl.Crouch.canceled -= OnCrouchCancelled;
    }
    private void OnCrouchPerform(InputAction.CallbackContext value)
    {
        isCrouch = true;
    }
    private void OnCrouchCancelled(InputAction.CallbackContext value)
    {
        isCrouch = false;
    }

    //Help Jumping and Check if the character hit ground
    private void Jump()
    {
        if(!isJumping)
        {
            Debug.Log("Jumping");
            anim.JumpAnim();
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumping = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }

    public void OnDashFoward(InputAction.CallbackContext value)
    {
        if (keyPressed && states.currentlyAttacking == false && states.crouch == false && states.onGround == false && canDash == true)
        {
            StartCoroutine(DashingFoward());
        }
        else
        {
            keyPressed = true;
        }
    }

    public void OnDashBackward(InputAction.CallbackContext value)
    {
        if (keyPressed && states.currentlyAttacking == false && states.crouch == false && states.onGround == false && canDash == true)
        {
            StartCoroutine(DashingBackward());
        }
        else
        {
            keyPressed = true;
        }
    }

    IEnumerator DashingFoward() //Call the dash function
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f; //Set character gravity to zero.

        rb.velocity = Vector2.right * dashingPower;

        yield return new WaitForSeconds(dashTime);
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashCoolDown);
        canDash = true;
        keyPressed = true;
    }

    IEnumerator DashingBackward() //Call the dash function
    {
        keyPressed = false;
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f; //Set character gravity to zero.

        rb.velocity = Vector2.left * dashingPower;

        yield return new WaitForSeconds(dashTime);
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashCoolDown);
        canDash = true;
        keyPressed = true;
    }
}
