using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWhenAnimEnd : MonoBehaviour
{
    private Animator myAnimator;

    private void Start()
    {
        myAnimator = GetComponent<Animator>();
        StartCoroutine(PlaySequentialAnimations());
    }

    private IEnumerator PlaySequentialAnimations()
    {
        yield return StartCoroutine(PlayAnimation("title"));

        yield return StartCoroutine(PlayAnimation("title2"));

        yield return StartCoroutine(PlayAnimation("title3"));

        Debug.Log("All animations finished playing.");
    }

    private IEnumerator PlayAnimation(string animationName)
    {
        myAnimator.Play(animationName);

        // Wait until the current animation is finished playing
        while (myAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f)
        {
            yield return null;
        }

        // Optionally wait for an additional delay if needed
        yield return new WaitForSeconds(0.5f); // Adjust the delay as needed
    }
}
