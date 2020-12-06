using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu (fileName = "DashAbility", menuName = "Abilities/DashAbility")]
public class DashAbility : AbstractAbility
{
   public float speed = 20f;
   private DashAbilityTriggerable dashAbilityTrigger;

   public override void Initialise(GameObject obj)
    {
        dashAbilityTrigger = obj.AddComponent<DashAbilityTriggerable>();
        dashAbilityTrigger.speed = speed;

        triggerable = dashAbilityTrigger;
    }
    public override void ButtonDown()
    {
        dashAbilityTrigger.Dash();
    }
    public override void ButtonUp()
    {
        
    }
}
