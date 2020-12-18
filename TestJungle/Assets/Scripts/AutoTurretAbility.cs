using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu (fileName = "AutoTurretAbility", menuName = "Abilities/AutoTurretAbility")]

public class AutoTurretAbility : AbstractAbility
{
  public float atkSpeed;
  private AutoTurretAbilityTriggerable autoTrigger;
  public GameObject bulletPrefab; 
  public GameObject firePoint;
  public float bulletSpeed;
  public float bulletDamage ;
  public float amountToHeal;
  public int abilityCost;
  public float payRate;

  public override void Initialise(GameObject obj)
    {
        autoTrigger = obj.AddComponent<AutoTurretAbilityTriggerable>();
        autoTrigger.atkSpeed = atkSpeed;
        autoTrigger.bulletPrefab = bulletPrefab;
        autoTrigger.firePoint = firePoint;
        autoTrigger.bulletSpeed = bulletSpeed;
        autoTrigger.bulletDamage = bulletDamage;
        autoTrigger.amountToHeal = amountToHeal;
        autoTrigger.abilityCost = abilityCost;
        autoTrigger.payRate = payRate;
        triggerable = autoTrigger;
    }

    public override void ButtonDown()
    {
        autoTrigger.Toggle();
    }

    public override void ButtonUp()
    {
        
    }
}
