using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBehavior : StateMachineBehaviour
{
    PlayerController playerRef;
    FighterScript fighterRef;
    // This script makes the player invunerable for a few seconds while they have been hit and resets their combo counter to zero
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerRef = animator.GetComponent<PlayerController>();
        fighterRef = animator.GetComponent<FighterScript>();

        playerRef.isAttacking = false; //End the player's stream of combos
        fighterRef.isInvunerable = true; //make the player invunerable to attacks while this is active
        fighterRef.comboCounter = 0;
    }



    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        fighterRef.isInvunerable = false;
    }
}
    
