using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu (menuName = "Abilities/RocketAbility")]
public class RocketAbility : AbstractAbility
{
    public GameObject rocketPrefab;
    private GameObject firePoint;
    private RocketAbilityTriggerable  rocketTrigger;
    public float radius = 10f;
    public float power = 10f;
    public float upForce = 2f;
    public float Damage = 10f;
    public GameObject explosionParticles;
    public float maxRange = 20f;
    public float verticalForce;

    public override void Initialise(GameObject obj)
    {
        rocketTrigger= obj.AddComponent<RocketAbilityTriggerable>();
        rocketTrigger.rocketPrefab = rocketPrefab;
        rocketTrigger.radius = radius;
        rocketTrigger.power =power;
        rocketTrigger.maxRange = maxRange;
        rocketTrigger.upForce= upForce;
        rocketTrigger.Damage = Damage;
        rocketTrigger.explosionParticles= explosionParticles;
        rocketTrigger.firePoint = firePoint;
        rocketTrigger.verticalForce = verticalForce;
        triggerable = rocketTrigger;
    }

    public override void ButtonDown()
    {
        rocketTrigger.Hold();
    }

    public override void ButtonUp()
    {
        rocketTrigger.Release();
    }
    
}
