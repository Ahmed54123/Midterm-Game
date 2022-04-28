using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : Enemy, iDamageable, iAttackable
{
   [SerializeField] ShootLaser laserShooting;

    public string whatBossStateToBeIn { get; set; } //Set a random boss state and assign it to the animator
    [SerializeField] string[] bossStates;

    [SerializeField] float timeToWaitBeforeSwitchingStates;
    

    private void Awake()
    {
        GameManager.Instance.CharacterSpawned(gameObject); //add this character to the list of characters alive

        gameObject.name = "Samurai Bot";

        whatBossStateToBeIn = bossStates[0];
        StartCoroutine(SwitchBossState());
    }
    private void Update()
    {
       

        if(_health<= 0)
        {
            Die();
        }

    }
    public void Damage(int damageTaken) //Implement this script's own variation of the IDamageable interface
    {
        if (GameManager.Instance.hasGameStarted == true)
        {
            Debug.Log("Enemy health is " + Health);
            //Decrease health by the amount of damage taken
            _health = Mathf.Clamp(Health - damageTaken, 0, maxHealth); //Keep the health's value in between a specific range
        }
    }

    //SET A RANDOM ANIMATOR STATE
    public IEnumerator SwitchBossState()
    {
        while (true)
        {
            whatBossStateToBeIn = bossStates[Random.Range(0, bossStates.Length)];

            Debug.Log(whatBossStateToBeIn);

            yield return new WaitForSeconds(timeToWaitBeforeSwitchingStates);
        }
    }

    public void BossAttack()
    {
        gameObject.GetComponent<iAttackable>().Attack(attackPoint,attackRange, this.gameObject, attackDamage);
    }
    public void Die()
    {
        //Place this in update to constantly monitor Enemy Health
        EventManager.Instance.CanStartNewEvent(); //End the event to reset all variables
        GameManager.Instance.CharacterDied(gameObject); //remove this character to the list when it dies


        gameObject.GetComponent<Animator>().SetTrigger("Die");
        Debug.Log("EnemyDied");
        Destroy(gameObject);

    }
    

    //Shooting laser functions
    public void BossShootLaser()
    {
        laserShooting.GetComponent<ShootLaser>().isShooting = true;
    }

    public void StopShootLaser()
    {
        laserShooting.GetComponent<ShootLaser>().isShooting = false;
    }

}
