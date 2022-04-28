using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Define Enemy Traits

    //HEALTH
    protected int _health;
    public int Health { get {return _health; } set {_health = value; } } //Declare health variable for iDamageable
    public int _maxHealth;
    public int maxHealth { get { return _maxHealth; } }
    


    //SPEED
    [SerializeField] protected float _speed;
    public float Speed { get { return _speed; } }

    //Attack Variables
    [SerializeField] protected Transform attackPoint; //The point the overlap circle will be drawn
    [SerializeField] protected float attackRange = 0.5f;
    [SerializeField] protected int attackDamage;

    public float AttackRange { get { return attackRange; } }


    //Timers for waiting for couratine
    public float timeToWaitBeforeSwitchingTargets;
    public float timerOffset;

    //Player Target that the animator can access

    
   protected List<Transform> playersInGame = new List<Transform>();

   protected Transform playerTargeted;
    public Transform PlayerTarget { get { return playerTargeted; } }

    bool isFlipped = false;

    public virtual void Init()
    {
        _health = maxHealth;
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            playersInGame.Add(player.transform);
        }

        StartCoroutine(SwitchPlayerTarget());
    }

    public virtual void UpdateFunc()
    {
        //Place this in the update function to constantly store all players' positions
        //Manage the players' positions continuously
        for (int playerNumber = 0; playerNumber < playersInGame.Count; playerNumber++) //Iterate through the length of how many slots are available in tne transform, then iterate through the players in the game and add them to the array
        {
            if (GameObject.FindGameObjectsWithTag("Player")[playerNumber].GetComponent<FighterScript>().isDead == false)
            {
                playersInGame[playerNumber] = GameObject.FindGameObjectsWithTag("Player")[playerNumber].GetComponent<Transform>(); //If the player isnt dead, add them to the list
            }
            

            else if(GameObject.FindGameObjectsWithTag("Player")[playerNumber].GetComponent<FighterScript>().isDead == true)
            {
                playersInGame.Remove(playersInGame[playerNumber]); // if the player found is dead, remove them from the list
            }


        }

      
    }
    void Start()
    {

        Init();
    }

    // Update is called once per frame
   void Update()
    {
        if (GameManager.Instance.isGameOver == false)
        {
            UpdateFunc();
        }
    }

    private void OnDrawGizmosSelected()
    { //Viusally set the attackPoints range and size
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }


    public virtual IEnumerator SwitchPlayerTarget()
    {
        while (true)
        { // loops forever...

            
            int playerTargetIndex =Random.Range(0, playersInGame.Count); //Set the enemy's target to a random player
            playerTargeted = playersInGame[playerTargetIndex];

            Debug.Log(playerTargeted + " is the current target");
            
            yield return new WaitForSeconds(Random.Range(timeToWaitBeforeSwitchingTargets - timerOffset, timeToWaitBeforeSwitchingTargets)); //wait this amount of time before reiterating the IEnumerator

            
        }
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if(transform.position.x > playerTargeted.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }

        else if(transform.position.x< playerTargeted.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }
}
