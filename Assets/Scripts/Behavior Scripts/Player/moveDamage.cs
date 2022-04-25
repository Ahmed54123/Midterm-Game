using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveDamage : StateMachineBehaviour
{
    //Fighter Script references
    FighterScript fighterReference;
    public int howMuchDamageDoesThisMoveDo;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        DamageOtherPlayer(animator);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }


    void DamageOtherPlayer(Animator animator)
    {
        fighterReference = animator.GetComponent<FighterScript>();
        fighterReference.attackDamage = howMuchDamageDoesThisMoveDo; //set the damage of this move in the game object's fighter script
        
       
    }

}
