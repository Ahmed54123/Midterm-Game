using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionScript : Enemy, iDamageable, iAttackable
{
    bool damagedColored; //set the amount of time the player becomes colored for
    [SerializeField] float timeColored;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        damagedColored = false;
        timer = timeColored;
    }

    // Update is called once per frame
    void Update()
    {
        if (damagedColored)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                timer = timeColored;
                damagedColored = false;
            }
        }

        if (_health <= 0)
        {
            Die();
        }
    }

    public void Damage(int damageTaken) //Implement this script's own variation of the IDamageable interface
    {
        if (GameManager.Instance.hasGameStarted == true)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.black;
            damagedColored = true;

            Debug.Log("Enemy health is " + Health);
            //Decrease health by the amount of damage taken
            _health = Mathf.Clamp(Health - damageTaken, 0, maxHealth); //Keep the health's value in between a specific range

        }
    }

    public void Die()
    {
        //Place this in update to constantly monitor Enemy Health


        gameObject.GetComponent<Animator>().SetTrigger("Die");
        Debug.Log("MinionDied");
        Destroy(gameObject);

    }

    public void MinionAttack()
    {
        gameObject.GetComponent<iAttackable>().Attack(attackPoint, attackRange, this.gameObject, attackDamage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<FighterScript>() != null)
        {
            playerTargeted = collision.gameObject.transform; //if the player gets close to the minion they will attack them
        }
    }
}