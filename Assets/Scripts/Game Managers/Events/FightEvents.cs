using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightEvents : MonoBehaviour
{
    // Start is called before the first frame update

    //FIGHT EVENT VARIABLES

    [SerializeField] float spawnerXRangeMin;
    [SerializeField] float spawnerXRangeMax;

    Vector3 spawnArea;

        //Health Event
             public GameObject healthPodPrefab;
             
   
        
    
    void OnEnable()
    {
        //Subscribe to the corresponded events in Event Manager
        EventManager.HealthPodEvent += HealthRegenerationPods;
        EventManager.MinionAttackEvent += MinionsAttack;
        EventManager.BossBattleEvent += BossBattleSpawn;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void HealthRegenerationPods()
    {
        //START EVENT
        Debug.Log("Health Regen Pod has spawned");

        // Spawn Health Object
        Instantiate(healthPodPrefab, new Vector3(Random.Range(spawnerXRangeMin, spawnerXRangeMax), 0), Quaternion.identity);

        //END EVENT in health pickup script
        
        
        

    }

    void MinionsAttack()
    {
        //START EVENT
        Debug.Log("Minions have spawned");

        //END EVENT
        EventManager.Instance.eventEnded = true; //End the event to reset all variables
        
        

    }

    void BossBattleSpawn()
    {
        //START EVENT
        Debug.Log("Boss has appeared");

        //END EVENT
        EventManager.Instance.eventEnded = true; //End the event to reset all variables
        
        

    }

    void OnDisable() 
    {
        //Unsubscribe to the corresponded events in Event Manager to avoid potential errors
        EventManager.HealthPodEvent -= HealthRegenerationPods;
        EventManager.MinionAttackEvent -= MinionsAttack;
        EventManager.BossBattleEvent -= BossBattleSpawn;
    }



}
