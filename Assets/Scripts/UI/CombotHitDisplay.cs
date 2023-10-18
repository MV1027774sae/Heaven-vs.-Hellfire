using System.Collections;
using UnityEngine;

public class CombotHitDisplay : MonoBehaviour
{
    [SerializeField] int numberOfHit = 0;
    [SerializeField] float coolDown = 1.5f;

    [SerializeField] StateManager stateManagerScript;

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
            coolDown = 1.5f;
            StartCoroutine(ResetBoolean());
        }

        else if (coolDown <= 0)
        {
            numberOfHit = 0;
            coolDown = 1.5f;
        }
    }

    private IEnumerator ResetBoolean()
    {
        yield return new WaitForSeconds(0.25f);
    }
}
