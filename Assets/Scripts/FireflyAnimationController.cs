using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyAnimationController : MonoBehaviour
{
    [SerializeField] private bool isGlowing;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();

        if (isGlowing)
            anim.Play("Firefly_Glow");

    }
}
