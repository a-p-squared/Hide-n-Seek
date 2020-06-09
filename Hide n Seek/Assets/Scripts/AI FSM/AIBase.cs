using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIBase : StateMachineBehaviour
{
    public GameObject player;
    public GameObject ai;
    public GameObject[] hidingSpots;
    public GameObject peakSpot;
    public GameObject currentHidingSpot;
    public GameObject spawnPoint;

    public Animator animationController;
    public NavMeshAgent agent;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ai = animator.gameObject;
        player = ai.GetComponent<AIScript>().getPlayer();
        animationController = ai.GetComponent<AIScript>().getAnimator();
        agent = ai.GetComponent<NavMeshAgent>();
    }
}
