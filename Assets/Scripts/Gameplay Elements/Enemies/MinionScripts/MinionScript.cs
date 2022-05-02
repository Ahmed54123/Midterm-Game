using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionScript : Enemy, iDamageable, iAttackable
{
    bool damagedColored; //set the amount of time the player becomes colored for
    [SerializeField] float timeColored;
    float timer;


    //PROJECTILE SETTINGS
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileForce;
    [SerializeField] float projectileShootRate;

    public bool isNearPlayer { get; set; } //Only shoot bullets if enemy is not near player

    void Awake()
    {
        damagedColored = false;
        timer = timeColored;
        isNearPlayer = false;

        StartCoroutine(ShootBullets());
       
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

    void LaunchProjectile()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, transform.right, Quaternion.identity);
        Projectile projectileRef = projectileObject.GetComponent<Projectile>();
        Vector2 directionPlayer = (this.gameObject.transform.position-playerTargeted.position).normalized;

        projectileRef.Launch(directionPlayer , projectileForce);

    }

    IEnumerator ShootBullets()
    {

        while (true)
        {
            if (isNearPlayer == false)
            {
                LaunchProjectile();
            }

            yield return new WaitForSeconds(projectileShootRate); //wait before trying to shoot again
        }
        
    }

}
