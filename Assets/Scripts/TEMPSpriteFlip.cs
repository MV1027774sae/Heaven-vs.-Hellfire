using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEMPSpriteFlip : MonoBehaviour
{
    private SpriteRenderer sRenderer;
    private StateManager stateManager;

    void Start()
    {
        sRenderer = GetComponentInChildren<SpriteRenderer>();
        stateManager = GetComponentInParent<StateManager>();
    }

    void Update()
    {
        if (!stateManager.lookRight)
            sRenderer.transform.localScale = new Vector3(-1, 1, 1);
        else if (stateManager.lookRight)
            sRenderer.transform.localScale = new Vector3(1, 1, 1);
    }
}
