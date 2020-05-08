using System.Collections;
using static System.Type;
using UnityEngine;

public abstract class AbstractWeapon : AbstractAbility {

    public float damage = 1.0f;

    public static AbstractAbilityCooldown GetWeaponCooldownHolder (GameObject player)
    {
        AbstractAbilityCooldown cooldown;
        do 
        {
            cooldown = player.GetComponent<AbstractAbilityCooldown>();
            if (cooldown.GetAbility() is AbstractWeapon)
            {
                return cooldown;
            }

        } while (cooldown != null);

        return null;
    }
}

// Weapons should derive from one of the two types below:
// 1 - Rigibody
// 2 - Raycast

// Rigibody type weapons e.g. projectiles with applied force
public abstract class RBWeapon : AbstractWeapon {
    public float force = 5.0f;
}

// Raycast type weapons e.g. damage applied off of a raycast
public abstract class RayCastWeapon : AbstractWeapon {
    public float range = 10.0f;
}