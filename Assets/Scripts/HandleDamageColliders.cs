using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleDamageColliders : MonoBehaviour
{
    //public DCtype dcType;

    public GameObject[] damageCollidersLeft;
    public GameObject[] damageCollidersRight;

    public enum DamageType
    {
        light,
        //medium,
        heavy
    }

    public enum DCtype
    {
        high,
        low
    }

    StateManager states;

    void Start()
    {
        states = GetComponent<StateManager>();
        CloseColliders();
    }

    public void OpenCollider(DCtype type, float delay, DamageType damageType)
    {
        if (!states.lookRight)
        {
            switch (type)
            {
                case DCtype.low:
                    StartCoroutine(OpenCollider(damageCollidersLeft, 0, delay, damageType));
                    break;
                case DCtype.high:
                    StartCoroutine(OpenCollider(damageCollidersLeft, 1, delay, damageType));
                    break;
            }
        }
        else
        {
            switch (type)
            {
                case DCtype.low:
                    StartCoroutine(OpenCollider(damageCollidersRight, 0, delay, damageType));
                    break;
                case DCtype.high:
                    StartCoroutine(OpenCollider(damageCollidersRight, 1, delay, damageType));
                    break;
            }
        }
    }

    IEnumerator OpenCollider(GameObject[] array, int index, float delay, DamageType damageType)
    {
        yield return new WaitForSeconds(delay);
        array[index].SetActive(true);
        array[index].GetComponent<DoDamage>().damageType = damageType;
    }

    public void CloseColliders()
    {

    }
}