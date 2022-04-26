using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : Enemy, iDamageable, iAttackable
{
   [SerializeField] ShootLaser laserShooting;
    
    
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

    public void Die()
    {
        //Place this in update to constantly monitor Enemy Health
        EventManager.Instance.CanStartNewEvent(); //End the event to reset all variables

        gameObject.GetComponent<Animator>().SetTrigger("Die");
        Debug.Log("EnemyDied");
        Destroy(gameObject);

    }
    

    public void BossShootLaser()
    {
        laserShooting.GetComponent<ShootLaser>().isShooting = true;
    }

}
