using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoDamage : MonoBehaviour
{
    StateManager states;

    public HandleDamageColliders.DamageType damageType;
    public float damage;
    public float hitStun;

    AudioManager audioManager;

    void Start()
    {
        states = GetComponentInParent<StateManager>();
        audioManager = GetComponentInParent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<StateManager>() && collision.tag == "Hurtbox")
        {
            StateManager oState = collision.GetComponentInParent<StateManager>();

            if (oState != states)
            {
                oState.TakeDamage(damage, damageType, hitStun);

                switch (damageType)
                {
                    case HandleDamageColliders.DamageType.light:
                        oState.gameObject.GetComponentInParent<AudioSource>().
                            PlayOneShot(audioManager.lightHitSFX[Random.Range(0, audioManager.lightHitSFX.Length)]);
                        break;
                    case HandleDamageColliders.DamageType.medium:
                        oState.gameObject.GetComponentInParent<AudioSource>().
                            PlayOneShot(audioManager.lightHitSFX[Random.Range(0, audioManager.lightHitSFX.Length)]);
                        break;
                    case HandleDamageColliders.DamageType.heavy:
                        oState.gameObject.GetComponentInParent<AudioSource>().
                            PlayOneShot(audioManager.heavyHitSFX[Random.Range(0, audioManager.heavyHitSFX.Length)]);
                        break;
                    case HandleDamageColliders.DamageType.projectile:
                        //oState.gameObject.GetComponentInParent<AudioSource>().
                        //    PlayOneShot(audioManager.lightHitSFX[Random.Range(0, audioManager.heavyHitSFX.Length)]);
                        break;
                }
            }
        }
    }
}
