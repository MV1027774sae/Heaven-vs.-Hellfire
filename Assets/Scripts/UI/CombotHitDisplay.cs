using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class CombotHitDisplay : MonoBehaviour
{
    [SerializeField] int numberOfHit = 0;

    [SerializeField] StateManager stateManagerScript;
    [SerializeField] float coolDown = 2f;

    private void Update()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        coolDown -= Time.deltaTime;
        if (stateManagerScript.gettingHit == true)
        {
            numberOfHit += 1;
            coolDown = 2f;
            StartCoroutine(ResetBoolean());
        }

        else if (coolDown <= 0)
        {
            numberOfHit = 0;
            coolDown = 2f;
        }
    }

    private IEnumerator ResetBoolean()
    {
        yield return new WaitForSeconds(0.25f);
    }
}
