using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveDamage : StateMachineBehaviour
{
    //Fighter Script references
    FighterScript fighterReference;
    [SerializeField] int howMuchDamageDoesThisMoveDo;

    [SerializeField] int whatTypeOfMoveIsThis;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        fighterReference = animator.GetComponent<FighterScript>();
        if (whatTypeOfMoveIsThis == 1)
        {
            fighterReference.LightAttack(howMuchDamageDoesThisMoveDo);
        }

        if (whatTypeOfMoveIsThis == 2)
        {
            fighterReference.HeavyAttack(howMuchDamageDoesThisMoveDo);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }


    

}
