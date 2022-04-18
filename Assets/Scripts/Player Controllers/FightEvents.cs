using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightEvents : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        ChooseARandomFightEvent();
    }

    // Update is called once per frame
    void Update()
    {
        if(EventManager.Instance.eventEnded == true)
        {
            ChooseARandomFightEvent();
        }
    }

    void HealthRegenerationPods()
    {
        Debug.Log("Health Regen Pod has spawned");

        //END EVENT
        EventManager.Instance.eventEnded = true; //End the event to reset all variables
        EventManager.RandomFightEvent -= HealthRegenerationPods;
        

    }

    void MinionsAttack()
    {
        Debug.Log("Minions have spawned");

        //END EVENT
        EventManager.Instance.eventEnded = true; //End the event to reset all variables
        EventManager.RandomFightEvent -= MinionsAttack;
        

    }

    void BossBattleSpawn()
    {
        Debug.Log("Boss has appeared");

        //END EVENT
        EventManager.Instance.eventEnded = true; //End the event to reset all variables
        EventManager.RandomFightEvent -= BossBattleSpawn;
        

    }

    //Call a random event
    void ChooseARandomFightEvent()
    {
        int randomEventNumber = Random.Range(0, 3);

        if(randomEventNumber == 0)
        {
            EventManager.RandomFightEvent += HealthRegenerationPods;
        }

      else  if (randomEventNumber == 1)
        {
            EventManager.RandomFightEvent += MinionsAttack;
        }

        else if (randomEventNumber == 2)
        {
            EventManager.RandomFightEvent += BossBattleSpawn;
        }

        else if (randomEventNumber == 3)
        {
            EventManager.RandomFightEvent += BossBattleSpawn;
        }

    }

    
}
