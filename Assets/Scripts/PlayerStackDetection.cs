using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;

public class PlayerStackDetection : MonoBehaviour
{
    CharacterManager charM;

    [SerializeField] private LayerMask layerMask;

    public List<Transform> players = new List<Transform>();

    private void Start()
    {
        charM = CharacterManager.GetInstance();
    }

    private void FixedUpdate()
    {
        if (players[0].GetComponent<HandleMovement>().justJumped == false && players[1].GetComponent<HandleMovement>().justJumped == false)
        {
            players[0].GetComponentInChildren<CapsuleCollider2D>().excludeLayers -= layerMask;
        }
        else if (players[0].GetComponent<HandleMovement>().justJumped == true && players[1].GetComponent<HandleMovement>().justJumped == false)
        {
            players[0].GetComponentInChildren<CapsuleCollider2D>().excludeLayers = layerMask;
        }

        if (players[1].position.y >= players[0].position.y + 0.6)
        {
            players[1].GetComponentInChildren<CapsuleCollider2D>().excludeLayers = layerMask;
        }
        else
        {
            players[1].GetComponentInChildren<CapsuleCollider2D>().excludeLayers -= layerMask;
        }
    }

    public static PlayerStackDetection instance;
    public static PlayerStackDetection GetInstance()
    {
        return instance;
    }

    void Awake()
    {
        instance = this;
    }
}
