using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Run : StateMachineBehaviour
{
    List<Transform> playerTransforms = new List<Transform>(); //Variable that constantly stores both players' transforms and will be selected at random

    //Variables to call random player in Enemy Script coroutine
    Transform playerTarget;

    
    Rigidbody2D enemyRb; //Reference to enemy's rigidbody

    BossScript boss;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        playerTarget = animator.GetComponent<BossScript>().PlayerTarget;

        enemyRb = animator.GetComponent<Rigidbody2D>();

        boss = animator.GetComponent<BossScript>();

        boss.BossShootLaser();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        playerTarget = animator.GetComponent<BossScript>().PlayerTarget; //set the player target to whatever is set in the enemy controller script

        boss.LookAtPlayer();
        //Set the enemy to target the player

        Vector2 target = new Vector2(playerTarget.position.x, enemyRb.position.y);
        Vector2 newPos = Vector2.MoveTowards(enemyRb.position, target,animator.GetComponent<BossScript>().Speed * Time.fixedDeltaTime);
        enemyRb.MovePosition(newPos);

      if(Vector2.Distance(playerTarget.position, enemyRb.position)<= boss.AttackRange)
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



