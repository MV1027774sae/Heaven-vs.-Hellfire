using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleFireball : MonoBehaviour
{
    [SerializeField] private float lifeTime;
    
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
        DestroySelf();
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
