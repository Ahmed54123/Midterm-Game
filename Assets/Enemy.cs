using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Define Enemy Traits

   public float _speed;
    public float Speed { get { return _speed; } }
    public int strength;

    //Timers for waiting for couratine
    public float timeToWaitBeforeSwitchingTargets;
    public float timerOffset;

    //Player Target that the animator can access

    int playerTargetIndex; 
    List<Transform> playersInGame = new List<Transform>();

    Transform playerTargeted;
    public Transform PlayerTarget { get { return playerTargeted; } }

    void Start()
    {
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            playersInGame.Add(player.transform);
        }

        StartCoroutine(SwitchPlayerTarget());
    }

    // Update is called once per frame
    void Update()
    {
        //Manage the players' positions continuously
        for (int playerNumber = 0; playerNumber < playersInGame.Count; playerNumber++) //Iterate through the length of how many slots are available in tne transform, then iterate through the players in the game and add them to the array
        {

            playersInGame[playerNumber] = GameObject.FindGameObjectsWithTag("Player")[playerNumber].GetComponent<Transform>();




        }
    }

    

     IEnumerator SwitchPlayerTarget()
    {
        while (true)
        { // loops forever...
            playerTargetIndex =Random.Range(0, playersInGame.Count); //Set the enemy's target to a random player
            playerTargeted = playersInGame[playerTargetIndex];
            //TESTER
            //Debug.Log("Player " + targetIndex + "IS THE CURRENT TARGET");


            yield return new WaitForSeconds(Random.Range(timeToWaitBeforeSwitchingTargets - timerOffset, timeToWaitBeforeSwitchingTargets)); //wait this amount of time before reiterating the IEnumerator

            
        }
    }
}
