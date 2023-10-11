using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleDamageColliders : MonoBehaviour
{
    //public DCtype dcType;

    public GameObject[] damageCollidersLeft;
    public GameObject[] damageCollidersRight;
    [SerializeField] private GameObject fireballObject;

    public enum DamageType
    {
        light,
        medium,
        heavy,
        projectile
    }

    public enum DCtype
    {
        high,
        low,
        fireball
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
                case DCtype.fireball:
                    StartCoroutine(CreateFireball(damageCollidersLeft, 2, delay, damageType, fireballObject, -3));
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
                case DCtype.fireball:
                    StartCoroutine(CreateFireball(damageCollidersRight, 2, delay, damageType, fireballObject, 3));
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
        for (int i = 0; i <damageCollidersLeft.Length; i++)
        {
            damageCollidersLeft[i].SetActive(false);
            damageCollidersRight[i].SetActive(false);
        }
    }

    IEnumerator CreateFireball(GameObject[] array, int index, float delay, DamageType damageType, GameObject fireball, float velocity)
    {
        yield return new WaitForSeconds(delay);
        Rigidbody2D fball = Instantiate(fireball, array[index].transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
        fball.velocity = new Vector2(velocity, 0);
    }
}
