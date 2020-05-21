using System.Collections;
using static System.Type;
using UnityEngine;

// Weapons should derive from one of the two types:
// 1 - Rigibody
// 2 - Raycast
public abstract class AbstractWeapon : AbstractAbility {

    public float damage = 1.0f;

    public static AbstractAbilityCooldown GetWeaponCooldownHolder (GameObject player)
    {
        AbstractAbilityCooldown cooldown;
        do 
        {
            cooldown = player.GetComponent<AbstractAbilityCooldown>();
            if (cooldown.enabled && cooldown.GetAbility() is AbstractWeapon)
            {
                return cooldown;
            }

        } while (cooldown != null);

        return null;
    }
}