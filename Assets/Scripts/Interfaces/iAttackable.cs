using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public interface iAttackable 
{
   public void Attack(Transform attackPoint, float attackRange, GameObject thisGameObject , int attackDamage)
    {
        //Detect objects/enemies in range of attack
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);
        //Damage them
        foreach (Collider2D hit in hitObjects)
        {
            if (hit.gameObject != thisGameObject.gameObject) //If the object attacked is not this gameObject's colldier, damage it
            {
                iDamageable hitDamage = hit.gameObject.GetComponent<iDamageable>();
                if (hitDamage != null)
                {
                    hitDamage.Damage(attackDamage);
                    
                }
            }
        }
    }
    
    

}
