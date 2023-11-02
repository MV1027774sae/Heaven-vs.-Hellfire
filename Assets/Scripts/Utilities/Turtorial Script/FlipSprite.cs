using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipSprite : MonoBehaviour
{
    public GameObject player; // Reference to the player or the object you want to detect
    private Transform spriteRenderer; //this object
    [SerializeField] StateManager stateManager;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = transform;
    }

    void Update()
    {
        // Check if the target object is behind this object
        if (IsObjectBehindTarget())
        {
            // Flip the object in the x-axis
            stateManager.lookRight= false;
        }
        else
        {
            stateManager.lookRight = true;
        }
    }

    // Function to check if the target object is behind this object
    bool IsObjectBehindTarget()
    {
        Vector3 thisPosition = spriteRenderer.position;
        Vector3 targetPosition = player.transform.position;

        // Compare the x-position of this object and the target object
        return thisPosition.x > targetPosition.x;
    }
}
