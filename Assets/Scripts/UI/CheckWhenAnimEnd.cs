using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWhenAnimEnd : MonoBehaviour
{
    private Animator myAnimator;
    [SerializeField] private GameObject mainMenu;

    private void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Get the current state information
        AnimatorStateInfo stateInfo = myAnimator.GetCurrentAnimatorStateInfo(0);

        // Check if the animation is finished playing
        if (stateInfo.normalizedTime >= 1.0f)
        {
            // Animation has finished playing
            Debug.Log("Animation finished playing");
            StartCoroutine(TurnOnTheMenu());
        }
    }

    private IEnumerator TurnOnTheMenu()
    {
        yield return new WaitForSeconds(2.0f);
        mainMenu.SetActive(true);
    }
}
