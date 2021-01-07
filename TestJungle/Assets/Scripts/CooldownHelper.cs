using System.Collections;
using UnityEngine;

public class CooldownHelper
{
    public static System.Type GetCooldownType(string cooldownString)
    {
        System.Type cooldownType;

        switch(cooldownString)
        {
            case "QuickCast":
                cooldownType = typeof(QuickCastAbilityCooldown);
                break;
            case "OnHold":
                cooldownType = typeof(OnHoldAbilityCooldown);
                break;
            case "OnRelease":
                cooldownType = typeof(OnReleaseAbilityCooldown);
                break;                
            default:
               // Debug.Log("Defaulting cooldown type to quick cast");
                cooldownType = typeof(QuickCastAbilityCooldown);
                break;
        }

        return cooldownType;
    }
}