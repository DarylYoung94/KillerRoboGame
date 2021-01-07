using System.Collections;
using static System.Type;
using UnityEngine;

// Weapons should derive from one of the two types:
// 1 - Rigibody
// 2 - Raycast
public abstract class AbstractWeapon : AbstractAbility {

    public float damage = 1.0f;
    public WeaponType weaponType = WeaponType.ONE_HANDED;
    public GameObject weaponPrefab;
    public Transform barrelExit;

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

    public enum WeaponType {
        ONE_HANDED,
        TWO_HANDED
    };

    public string GetAnimationString()
    {
        string ret = "OneHanded";
        switch (weaponType)
        {
            case WeaponType.ONE_HANDED :
                ret = "OneHanded";
                break;
            case WeaponType.TWO_HANDED :
                ret = "TwoHanded";
                break;
            default:
                break;
        }

        return ret;
    }
}