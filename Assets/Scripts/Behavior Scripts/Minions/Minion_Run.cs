using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion_Run : StateMachineBehaviour
{
    //Variables to call random player in Enemy Script coroutine
    Transform playerTarget;


    Rigidbody2D minionRb; //Reference to enemy's rigidbody

    MinionScript minion;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        playerTarget = animator.GetComponent<MinionScript>().PlayerTarget;

        minionRb = animator.GetComponent<Rigidbody2D>();

        minion = animator.GetComponent<MinionScript>();




    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerTarget = animator.GetComponent<MinionScript>().PlayerTarget; //set the player target to whatever is set in the enemy controller script

        minion.LookAtPlayer();
        //Set the enemy to target the player

        Vector2 target = new Vector2(playerTarget.position.x, minionRb.position.y);
        Vector2 newPos = Vector2.MoveTowards(minionRb.position, target, -1* animator.GetComponent<MinionScript>().Speed * Time.fixedDeltaTime);
        minionRb.MovePosition(newPos);

       
            if (Vector2.Distance(playerTarget.position, minionRb.position) <= minion.AttackRange)
            {
                animator.SetTrigger("Attack");
            }
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }


}
