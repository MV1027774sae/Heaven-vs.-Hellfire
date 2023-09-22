using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    private bool isOnGround()
    {

    }

    public void ResetStateInputs()
    {
        horizontal = 0;
        vertical = 0;
        attackL = false;
        attackM = false;
        attackH = false;
        attackS = false;
        crouch = false;
        gettingHit = false;
        currentlyAttacking = false;
        dontMove = false;

    }

    public void CloseMovementCollider(int index)
    {
        movementColliders[index].SetActive(false);
    }

    public void OpenMovementCollider(int index)
    {
        movementColliders[index].SetActive(true);
    }

    public void TakeDamage(float damage, HandleDamageColliders.DamageType damageType)
    {

    }

    IEnumerator CloseImmortality (float timer)
    {
        yield return new WaitForSeconds(timer);
        gettingHit = false;
    }
}
