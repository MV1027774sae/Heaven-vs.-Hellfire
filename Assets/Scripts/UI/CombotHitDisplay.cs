using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombotHitDisplay : MonoBehaviour
{
    [SerializeField] int comboHit = 0;
    [SerializeField] StateManager stateManagerScript;
    [SerializeField] float coolDown = 2f;
    private bool hasIncreased = false;   // Keep track of whether the number has been increased.

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        coolDown -= Time.deltaTime;
        if (stateManagerScript.gettingHit == true && hasIncreased == false)
        {
            comboHit += 1;
            coolDown = 5f;
            hasIncreased = true;
            if(comboHit >= 1)
            {
                Debug.Log("NICE!");
            }
            if(comboHit >= 20)
            {
                Debug.Log("HELL YEAH!");
            }
            StartCoroutine(ResetBoolean());
        }

        else if (coolDown <= 0)
        {
            comboHit = 0;
            coolDown = 2f;
        }
    }

    private IEnumerator ResetBoolean()
    {
        hasIncreased = false;
        yield return new WaitForSeconds(0.25f);
    }
}
