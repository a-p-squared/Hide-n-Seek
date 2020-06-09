using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPeak : AIBase
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        currentHidingSpot = ai.GetComponent<AIScript>().getHidingSpot();
        peakSpot = currentHidingSpot.GetComponent<HidingSpotScript>().getRandomPeakSpot();
        agent.SetDestination(peakSpot.transform.position);
        animationController.SetBool("isRunning", true);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector3.Distance(ai.transform.position, peakSpot.transform.position) <= 2.0f)
        {
            animator.SetTrigger("StartPeaking");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
