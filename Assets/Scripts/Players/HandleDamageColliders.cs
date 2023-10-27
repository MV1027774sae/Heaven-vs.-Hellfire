using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleDamageColliders : MonoBehaviour
{
    //public DCtype dcType;

    public GameObject[] damageCollidersLeft;
    public GameObject[] damageCollidersRight;
    [SerializeField] private GameObject fireballObject;
    [SerializeField] private float dpVelocityX = 0.5f;
    [SerializeField] private float dpVelocityY = 4.5f;

    public enum DamageType
    {
        light,
        medium,
        heavy,
        projectile
    }

    public enum DCtype
    {
        strike,
        grab,
        fireball,
        dp
    }

    StateManager states;
    AudioManager audioManager;

    void Start()
    {
        states = GetComponent<StateManager>();
        audioManager = GetComponent<AudioManager>();
        CloseColliders();
    }

    public void OpenCollider(DCtype type, float damage, float delay, DamageType damageType)
    {
        if (!states.lookRight)
        {
            switch (type)
            {
                case DCtype.strike:
                    StartCoroutine(OpenCollider(damageCollidersLeft, 0, damage, delay, damageType));
                    break;
                case DCtype.grab:
                    StartCoroutine(OpenCollider(damageCollidersLeft, 1, damage, delay, damageType));
                    break;
                case DCtype.fireball:
                    StartCoroutine(CreateFireball(damageCollidersLeft, 2, delay, damageType, fireballObject, -3));
                    break;
                case DCtype.dp:
                    StartCoroutine(DragonPunch(damageCollidersLeft, 0, damage, delay, damageType));
                    break;
            }
        }
        else
        {
            switch (type)
            {
                case DCtype.strike:
                    StartCoroutine(OpenCollider(damageCollidersRight, 0, damage, delay, damageType));
                    break;
                case DCtype.grab:
                    StartCoroutine(OpenCollider(damageCollidersRight, 1, damage, delay, damageType));
                    break;
                case DCtype.fireball:
                    StartCoroutine(CreateFireball(damageCollidersRight, 2, delay, damageType, fireballObject, 3));
                    break;
                case DCtype.dp:
                    StartCoroutine(DragonPunch(damageCollidersLeft, 0, damage, delay, damageType));
                    break;
            }
        }
    }

    IEnumerator OpenCollider(GameObject[] array, int index, float damage, float delay, DamageType damageType)
    {
        yield return new WaitForSeconds(delay);
        array[index].SetActive(true);
        array[index].GetComponent<DoDamage>().damage = damage;
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

    IEnumerator DragonPunch(GameObject[] array, int index, float damage, float delay, DamageType damageType)
    {
        yield return new WaitForSeconds(delay);
        array[index].SetActive(true);
        array[index].GetComponent<DoDamage>().damage = damage;
        array[index].GetComponent<DoDamage>().damageType = damageType;

        states.onGround = false;

        if (states.lookRight)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(dpVelocityX, dpVelocityY);
        }
        else
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-dpVelocityX, dpVelocityY);
        }

    }
}
