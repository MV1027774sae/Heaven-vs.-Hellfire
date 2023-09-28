using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoDamage : MonoBehaviour
{
    StateManager states;

    public HandleDamageColliders.DamageType damageType;

    [SerializeField] private float damage = 30;

    void Start()
    {
        states = GetComponentInParent<StateManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<StateManager>())
        {
            StateManager oState = collision.GetComponentInParent<StateManager>();

            if (oState != states)
            {
                if (!oState.currentlyAttacking) //remove?
                {
                    oState.TakeDamage(damage, damageType);
                }
            }
        }
    }
}
