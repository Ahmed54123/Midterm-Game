using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleScript : StateMachineBehaviour
{
    PlayerController player;// Reference to alter player properties
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      //  CheckAttackType(animator);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CheckAttackType(animator);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = animator.GetComponent<PlayerController>();
        player.isAttacking = false;
        
    }

    void CheckAttackType(Animator animator)
    {
        player = animator.GetComponent<PlayerController>();
        if (player.isAttacking == true && player.whatTypeOfAttack == 1) //if the player has inputted the light attack button and they are currently in a state of attacking, start a light attack combo
        {
            
            animator.Play("light attack 1");
        }

        if (player.isAttacking == true && player.whatTypeOfAttack == 2) //if the player has inputted the heavy attack button and they are currently in a state of attacking, start a heavy attack combo
        {
            
            animator.Play("Heavy Attack 1");
        }
    }

    
}
