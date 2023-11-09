using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleFireball : MonoBehaviour
{
    [SerializeField] private float lifeTime;
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip projectileHitSFX; 
    [SerializeField] private AudioClip projectileMissSFX;
    public bool triggerFireball;

    void Start()
    {
        StartCoroutine(LifeTimer());
    }

    IEnumerator LifeTimer()
    {
        yield return new WaitForSeconds(lifeTime);
        DestroySelf();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //StartCoroutine(FireballHit());
        DestroySelf();
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }

    //IEnumerator FireballHit()
    //{
    //    source.PlayOneShot(projectileHitSFX);
    //    //play explosion animation
    //    yield return new WaitForSeconds(animationlength);
    //    DestroySelf();
    //}

    IEnumerator TempDestroySelf()
    {
        source.PlayOneShot(projectileHitSFX);
        yield return new WaitForSeconds(0.3f);
        DestroySelf();
    }

    public void CreateFireball()
    {
        
    }
}
