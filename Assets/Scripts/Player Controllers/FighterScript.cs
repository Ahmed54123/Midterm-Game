using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class FighterScript : MonoBehaviour, iDamageable, iAttackable
{

    //Variables for controlling stats and damage
    int _health;
    [SerializeField] int _maxHealth;
    public int maxHealth { get { return _maxHealth; } } //return the value of the maximum health of character
    public PlayerController playerControllerRef; // Property that will allow child classes to access player stats

    //ATTACK VARIABLES
    [SerializeField] Transform attackPoint; //The point the overlap circle will be drawn
    [SerializeField] float attackRange = 0.5f;

    

    public bool isDead { get; set; } //Keeps track of if the player has died

    //Variables that will control the character's UI elements
    Slider HealthBar; //This slider will be fed the player's health
    Image healthbarSlider;
    Image healthbarBackground;
    [SerializeField] Color fullHealthColor;
    [SerializeField] Color midHealthColor;
    [SerializeField] Color lowHealthColor;





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

    public bool isInvunerable { get; set; }
    [SerializeField] float timeInvunerable;
    float timer;

    void Start()
    {
        isDead = false; //the player is alive
        playerControllerRef = gameObject.GetComponent<PlayerController>();

        GameManager.Instance.CharacterSpawned(gameObject); //add this character to the list of characters alive

        _health = maxHealth; // set the player's health to max at the start of the game

        
        HealthBar = GameObject.Find(playerControllerRef.name + " Health bar").GetComponent<Slider>(); //Set the healthbar of this player to the corresponding health bar and get the game object's slider component
        HealthBar.maxValue = maxHealth;
        HealthBar.value = _health;
        healthbarSlider = GameObject.Find(playerControllerRef.name + " Fill").GetComponent<Image>();
        healthbarBackground = GameObject.Find(playerControllerRef.name + " Border").GetComponent<Image>();

        isInvunerable = false;
        timer = timeInvunerable;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.hasGameStarted == true) //The player can only damage the other player while the game is active
        {


            HealthBar.value = _health;

            //Set healthbar color depending on health
            if (_health > maxHealth / 2)
            {
                healthbarSlider.color = fullHealthColor;
                healthbarBackground.color = fullHealthColor;
            }

            else if (_health <= maxHealth / 2 && _health > maxHealth / 3)
            {
                healthbarSlider.color = midHealthColor;
                healthbarBackground.color = midHealthColor;
            }

            else if (_health <= maxHealth / 3)
            {
                {
                    healthbarSlider.color = lowHealthColor;
                    healthbarBackground.color = lowHealthColor;
                }

            }

            if (isInvunerable == true)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    timer = timeInvunerable;
                    
                    gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                    isInvunerable = false;
                }
            }

            if (_health <= 0)
            {

                Die();
            }

        }
    }

        public void Damage(int damageTaken) //Implement this script's own variation of the IDamageable interface
        {
            if (GameManager.Instance.hasGameStarted == true)
            {
                if (isInvunerable == false)
                {
                    //Make the player go into a hit state where they are invunerable and cannot chain thier current combo
                    gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                    
                    isInvunerable = true;

                    //Decrease health by the amount of damage taken
                    _health = Mathf.Clamp(_health - damageTaken, 0, maxHealth); //Keep the health's value in between a specific range

                    
                   
                }
            }
        }

    public void Heal(int healAmount)
    {
        if (GameManager.Instance.hasGameStarted == true)
        {
            
                //Make the player go into a hit state where they are invunerable and cannot chain thier current combo
                gameObject.GetComponent<SpriteRenderer>().color = Color.green;

               

                //Decrease health by the amount of damage taken
                _health = Mathf.Clamp(_health + healAmount, 0, maxHealth); //Keep the health's value in between a specific range

            isInvunerable = true;

            
        }
    }


        public void LightAttack(int moveDamage)
        {
            gameObject.GetComponent<iAttackable>().Attack(attackPoint, attackRange, this.gameObject, moveDamage);

        }

        public void HeavyAttack(int moveDamage)
        {
            gameObject.GetComponent<iAttackable>().Attack(attackPoint, attackRange, this.gameObject, moveDamage);

        }
        //functions to be called in the player controller



        private void OnDrawGizmosSelected()
        { //Viusally set the attackPoints range and size
            if (attackPoint == null)
            {
                return;
            }
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }

        public void Die()
        {

            isDead = true;
            GameManager.Instance.CharacterDied(gameObject);
            //Play death animation
            
            gameObject.GetComponent<PlayerInput>().enabled = false; //The player cannot move after they are defeated
            gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            gameObject.GetComponent<Collider2D>().enabled = false;
        }



    }

