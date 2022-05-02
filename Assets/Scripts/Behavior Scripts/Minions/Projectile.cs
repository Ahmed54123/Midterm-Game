using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
   [SerializeField] int projectileDamage;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //we also add a debug log to know what the projectile touch
        Debug.Log("Projectile Collision with " + other.gameObject);

        iDamageable hitDamage = other.gameObject.GetComponent<iDamageable>();
        if (hitDamage != null)
        {
            hitDamage.Damage(projectileDamage); //If the collision returns an idamageable component, damage the object

        }
        Destroy(gameObject);
    }

    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force); // shoot the projectile with this much force
    }

   
}
