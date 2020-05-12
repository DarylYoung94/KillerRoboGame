using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu (menuName = "Abilities/TurretAbility")]


//TODO
//turret is attacking every frame
//turret attacks when in projector mode
public class TurretAbility : AbstractAbility
{
    public GameObject turretPrefab;
    public GameObject projectorPrefab;
    private TurretAbilityTriggerable turretTrigger;
    public float despawnTimer = 5f;
    public float turretAttackSpeed = 1f;
    public float damage = 10f;
    public float maxRange = 20f;
    public float attackSpeed = 1f;
    


    public override void Initialise(GameObject obj)
    {
        turretTrigger= obj.AddComponent<TurretAbilityTriggerable>();
        turretTrigger.turretPrefab = turretPrefab;
        turretTrigger.projectorPrefab = projectorPrefab;
        turretTrigger.despawnTimer = despawnTimer;
        turretTrigger.damage = damage;
        turretTrigger.maxRange = maxRange;
        turretPrefab.GetComponent<Turret>().turretAttackSpeed = attackSpeed;
        
    }
    public override void ButtonDown()
    {
        turretTrigger.Hold();
    }

    public override void ButtonUp()
    {
        turretTrigger.Release();
    }
}
