using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    //FIGHT EVENT DECLARATIONS
    public delegate void FightEvent(); //Declare the delegate action type

    //Declare the type of fight events that other game objects can subscribe to
    public static event FightEvent RandomFightEvent;

    //DECLARING AN INSTANCE
    private static EventManager _instance; // make an instance of this class that can be accessed by all the scripts in the scene
    public static EventManager Instance
    {
        get
        {
            if (_instance is null) Debug.LogError("Event Manager is Null");

            return _instance; //referenced from https://medium.com/nerd-for-tech/implementing-a-game-manager-using-the-singleton-pattern-unity-eb614b9b1a74
        }
    }
    

   

    //Declaring Timer and Bool Variables

    //TIMERS
    public int timeBetweenFightEvents;
    public int timeOffset; // Time to be offsetted by the random range function

    float timeSinceLastEvent;

    //TRACKING STATES
    public bool eventEnded { get; set; } //bool to keep track of when events start and finish so there aren't any bugs

    ////Event to reset the variables
    //public delegate void EndEvent();
    //public static event EndEvent ResetFightEventVariables; //This event resets all the variables of all the fight events (enemy health, timers, position, etc.)

    private void Awake()
    {
        //TESTER
        RandomFightEvent += testFighting;
        //
        _instance = this; //initialize the instance
    }
    void Start()
    {
        eventEnded = false;
        timeSinceLastEvent = Random.Range(timeBetweenFightEvents-timeOffset, timeBetweenFightEvents); //set the timer to a value
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.hasGameStarted == true && GameManager.Instance.isGameOver == false)
        {

            if (eventEnded == false)
            {
                timeSinceLastEvent -= Time.deltaTime;

                if (timeSinceLastEvent <= 0) //when the timer runs out run the code runs
                {
                    if (RandomFightEvent != null)
                    {
                        RandomFightEvent(); //Call a random fight event to occur 

                    }

                }
            }

            if (eventEnded == true) //Once the event is over, start the timer till the next event again
            {
                //if (ResetFightEventVariables != null)
                //{
                //    ResetFightEventVariables();
                //}
                eventEnded = false;
                timeSinceLastEvent = Random.Range(timeBetweenFightEvents - timeOffset, timeBetweenFightEvents);
            }
        }

    }

    //Tester to make sure event still runs
    void testFighting()
    {
        Debug.Log("I still work");
    }
}
