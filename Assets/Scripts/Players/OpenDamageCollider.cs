using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDamageCollider : StateMachineBehaviour
{
    StateManager states;
    public HandleDamageColliders.DamageType damageType;
    public HandleDamageColliders.DCtype dcType;
    public float damage;
    public float hitStunFrames;
    public float attackStartUpFrames;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (states == null)
        {
            states = animator.transform.GetComponentInParent<StateManager>();
        }

        states.currentlyAttacking = true;
        states.handleDC.OpenCollider(dcType, damage, attackStartUpFrames, damageType, hitStunFrames);
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //{

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (states == null)
        {
            states = animator.transform.GetComponentInParent<StateManager>();
        }

        states.currentlyAttacking = false;
        states.handleDC.CloseColliders();
    }

    //OnStateMove is called right after Animator.OnAnimatorMove(). code that processes and affects root motion should be implemented here
    //public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //{

    //OnStateIK is called right after Animator.OnAnimatorIK(). code that sets up animation IK (inverse kinematics) should be implemented here
    //public override void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //{
}
