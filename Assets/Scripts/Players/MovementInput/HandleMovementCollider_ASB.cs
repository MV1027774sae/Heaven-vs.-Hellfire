using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleMovementCollider_ASB : StateMachineBehaviour
{
    StateManager states;

    public int index;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (states == null)
        {
            states = animator.transform.GetComponentInParent<StateManager>();
        }

        states.CloseMovementCollider(index);
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    //OnStateExit is called when a transition ends and the state machine finishes evaluation this state
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (states == null)
        {
            states = animator.transform.GetComponentInParent<StateManager>();
        }

        states.OpenMovementCollider(index);
    }
}
