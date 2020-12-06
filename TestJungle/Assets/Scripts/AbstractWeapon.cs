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
        bool found = false;
        AbstractAbilityCooldown cooldown = null;
        AbstractAbilityCooldown[] cooldowns = player.GetComponents<AbstractAbilityCooldown>();

        for (int i=0; i < cooldowns.Length && !found; i++)
        {
            if (cooldowns[i].enabled && cooldowns[i].GetAbility() is AbstractWeapon)
            {
                cooldown = cooldowns[i];
                break;
            }
        }

        return cooldown;
    }
}