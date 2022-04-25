using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionToNextCombo : StateMachineBehaviour
{
    public int whatTypeOfMoveIsThis; // Check what kind of move this is: Light or Heavy
    public bool canThisMoveBeChained; //Check if this move can be chained with an alternative move
    public string comboToPlayNext; //Input what move should be triggered next
    public string alternativeComboToPlayNext; // Input the alternative move this move can lead to if a different input is pressed
    PlayerController playerRef; //Reference to player object this animator is attached to.


    //Default combos to play if the move cannot be chained
    string defaultLightAttack = "light Attack 1";
    string defaultHeavyAttack = "Heavy Attack 1";

    

    

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CheckWhatsTheNextMove(animator);

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerRef.isAttacking = false; //if the player does not input a new move set isAttacking to false

        FighterScript fighterRef = animator.GetComponent<FighterScript>();
        fighterRef.comboCounter = 0; // set the combo counter to 0 if the player does not input an attack
        
    }

    void CheckWhatsTheNextMove(Animator animator)
    {
        playerRef = animator.GetComponent<PlayerController>(); //Get a reference to the player object this animator is attached to
        if (playerRef.isAttacking == true) //If the player has inputted another attack before the last move set isAttacking to false, move on to the next move in the combo
        {
            if (playerRef.whatTypeOfAttack == whatTypeOfMoveIsThis) //if this move is the same type as the previous move, continue the combo
            {
                animator.Play(comboToPlayNext);
            }

            else if(playerRef.whatTypeOfAttack != whatTypeOfMoveIsThis) //if the move inputted by the player is a different move check if this can be chained into another combo. If it cannot it will start another default combo based on the input
            {
                if(canThisMoveBeChained == true)
                {
                    animator.Play(alternativeComboToPlayNext);
                }

                else
                {
                   if(playerRef.whatTypeOfAttack == 1)
                    {
                        animator.Play(defaultLightAttack);
                    }

                   else if(playerRef.whatTypeOfAttack == 2)
                    {
                        animator.Play(defaultHeavyAttack);
                    }
                }
            }
        }

        
    }

    
    
}
