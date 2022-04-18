using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class FighterScript : MonoBehaviour, iDamageable
{

    //Variables for controlling stats and damage
   int _health;
   [SerializeField] int maxHealth;
    public PlayerController playerControllerRef; // Property that will allow child classes to access player stats

   BoxCollider2D thisGameObjectsHitCollider; //Reference to the trigger box collider to set it inactive when the player is not attacking
   [SerializeField] GameObject winnerPlayerTag; //this object will be set inactive if the player dies, so the only player with a tag left will be the winner
    bool isDead = false; //Keeps track of if the player has died

    //Variables that will control the character's UI elements
    Slider HealthBar; //This slider will be fed the player's health
    

    //UI Elements and References
    public int Health
    {
        get
        {
            return _health;
        }

        set
        {
            _health = value;
        }
    }
    public int attackDamage { get; set; } //the animator will set the damage based on what kind of attack and what stage of the combo the player is in
    public int comboCounter { get; set; } //the animator will increase the combo counter as the chain progresses
    

    void Start()
    {
        playerControllerRef = gameObject.GetComponent<PlayerController>();
        thisGameObjectsHitCollider = gameObject.GetComponent<BoxCollider2D>();
        thisGameObjectsHitCollider.enabled =false;
        _health = maxHealth; // set the player's health to max at the start of the game

        HealthBar = GameObject.Find(playerControllerRef.name + " Health bar").GetComponent<Slider>(); //Set the healthbar of this player to the corresponding health bar and get the game object's slider component
        

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.hasGameStarted == true) //The player can only damage the other player while the game is active
        {
            if (playerControllerRef.isAttacking == true)
            {

                thisGameObjectsHitCollider.enabled = true;

            }

            else
            {
                thisGameObjectsHitCollider.enabled = false; //only turn on the hit collider when the player 
            }

            HealthBar.value = _health;



            Die();

            if (GameObject.FindGameObjectsWithTag("Winner").Length < 2 && isDead == false) 
            {
                GameManager.Instance.Winner = this.gameObject.name; //If there is only one winner game object in the scene after the game was started this game object becomes a winner
                GameManager.Instance.GameOver();
            }
        }

       
    }

   public void Damage(int damageTaken) //Implement this script's own variation of the IDamageable interface
    {
        _health -= damageTaken; //Decrease health by the amount of damage taken
        Mathf.Clamp(_health, 0, maxHealth); //Keep the health's value in between a specific range
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        iDamageable hit = collision.gameObject.GetComponent<iDamageable>(); //Check if the collision's gameobject implements an IDamageable Interface
        
        if(hit!= null) //If the object implements an IDamageable interface 
        {
            hit.Damage(attackDamage);
        }
        FighterScript otherPlayer = collision.gameObject.GetComponent<FighterScript>();
        if (otherPlayer != null && otherPlayer.gameObject.name != this.gameObject.name)
        {
           //damage the other player if they have a fighter component and if they do not share the same name as this game Object (to avoid confliction of game object identification)
            
            comboCounter++; //increase the counter everytime the player has hit another player
            
        }
    }

    //functions to be called in the player controller
    public virtual void LightAttackFighter()
    {


    }

    public virtual void HeavyAttackFighter()
    {

    }

    public void Die()
    {
        if (_health <= 0)
        {
            isDead = true;
            //Play death animation
            winnerPlayerTag.SetActive(false);
            gameObject.GetComponent<PlayerInput>().enabled = false; //The player cannot move after they are defeated
        }
    }

   

}
