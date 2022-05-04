using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightEvents : MonoBehaviour
{
    // Start is called before the first frame update

    //FIGHT EVENT VARIABLES

    //Spawn Points on map
    //Random Spawn Point Ranges
    [SerializeField] float spawnerXRangeMin;
    [SerializeField] float spawnerXRangeMax;

    //Middle of Arena
    [SerializeField] Transform middleSpawnPoint;


    [SerializeField] Transform healthPodMinSpawn;
    [SerializeField] Transform healthPodMaxSpawn;


    //Health Event
    public GameObject healthPodPrefab;


        //Boss Battle
             public GameObject bossCharacter;
             

        
    
    void OnEnable()
    {
        //Subscribe to the corresponded events in Event Manager
        EventManager.HealthPodEvent += HealthRegenerationPods;
        EventManager.MinionAttackEvent += MinionsAttack;
        EventManager.BossBattleEvent += BossBattleSpawn;
    }

   
    void HealthRegenerationPods()
    {
        //START EVENT
        Debug.Log("Health Regen Pod has spawned");

        // Spawn Health Object
        Instantiate(healthPodPrefab, new Vector3(Random.Range(healthPodMinSpawn.position.x, healthPodMaxSpawn.position.x), middleSpawnPoint.position.y), Quaternion.identity);

        //END EVENT in health pickup script
        
        
        

    }

    void MinionsAttack()
    {
        //START EVENT
        Debug.Log("Minions have spawned");

        //END EVENT
        EventManager.Instance.CanStartNewEvent(); //End the event to reset all variables



    }

    void BossBattleSpawn()
    {
        //START EVENT
        Debug.Log("Boss has appeared");

        //Spawn Boss Character

       GameObject bossSpawned =  Instantiate(bossCharacter, new Vector3(middleSpawnPoint.position.x, middleSpawnPoint.position.y), Quaternion.identity);
        bossSpawned.GetComponent<Animator>().SetTrigger("Intro");


        //END EVENT        


    }

    void OnDisable()
    {
        //Unsubscribe to the corresponded events in Event Manager to avoid potential errors
        EventManager.HealthPodEvent -= HealthRegenerationPods;
        EventManager.MinionAttackEvent -= MinionsAttack;
        EventManager.BossBattleEvent -= BossBattleSpawn;
    }



}
