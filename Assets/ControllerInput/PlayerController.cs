using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.VersionControl.Asset;


public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    [SerializeField] StateManager states;
    [SerializeField] HandleMovement handleMovement;

    [SerializeField] HandleAnimations anim;
    public Animator animate;

    public float speed;
    private float horizontal;

    public float jumpForce = 5.0f; // Adjust this value as needed.
    private bool isJumping = false;


    void Awake()
    {
        animate = GetComponentInChildren<Animator>();
    }

    //Help moving
    private void FixedUpdate()
    {
        speed = 143f;

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
    }

    //Call Jumping
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
        Debug.Log("Moving");
        horizontal = context.ReadValue<Vector2>().x;
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
}
