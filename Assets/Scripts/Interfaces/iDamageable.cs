using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface iDamageable //This interface will be implemented on any object that can be damaged. This way the player can call upon any object's iDamageable rather than having to reference each script.
{
    int Health { get; set; }

    void Damage(int damageTaken);
}