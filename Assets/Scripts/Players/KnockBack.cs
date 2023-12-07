using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public float knockbackForce = 0.5f;
    public float knockupForce = 0f;

    [SerializeField] StateManager stateManagerScript;
    [SerializeField] InputHandler inputHandlerScript;

    private void Update()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        // Check if the colliding object has a Rigidbody2D
        if (stateManagerScript.gettingHit == true)
        {
            if (rb != null && inputHandlerScript.playerInput == "")
            {
                Debug.Log("Player 1 in!");
                if(stateManagerScript.lookRight)
                {
                    // Apply knockback force
                    rb.velocity = new Vector2(-knockbackForce, knockupForce);
                }
                else if (!stateManagerScript.lookRight)
                {
                    rb.velocity = new Vector2(knockbackForce, knockupForce);
                }
            }
            else if (rb != null && inputHandlerScript.playerInput == "1")
            {
                Debug.Log("Player 2 in!");
                if (stateManagerScript.lookRight)
                {
                    // Apply knockback force
                    rb.velocity = new Vector2(-knockbackForce, knockupForce);
                }
                else if (!stateManagerScript.lookRight)
                {
                    rb.velocity = new Vector2(knockbackForce, knockupForce);
                }
            }
        }
    }
}
