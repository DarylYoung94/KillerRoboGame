using System.Collections;

using UnityEngine;
[CreateAssetMenu (menuName = "Abilities/MineAbility")]
public class MineAbility : AbstractAbility
{
    public GameObject minePrefab;
    public GameObject projectorPrefab;
    private MineAbilityTriggerable mineTrigger;
    public float despawnTimer = 8f;
    public float radius = 10f;
    public float power = 10f;
    public float upForce = 2f;
    public float Damage = 10f;
    public GameObject explosionParticles;
    public GameObject timerParticles;
    public float maxRange = 20f;

    public override void Initialise(GameObject obj)
    {
        mineTrigger= obj.AddComponent<MineAbilityTriggerable>();
        mineTrigger.minePrefab = minePrefab;
        mineTrigger.projectorPrefab = projectorPrefab;
        mineTrigger.despawnTimer = despawnTimer;
        mineTrigger.radius = radius;
        mineTrigger.power =power;
        mineTrigger.maxRange = maxRange;
        mineTrigger.upForce= upForce;
        mineTrigger.Damage = Damage;
        mineTrigger.explosionParticles= explosionParticles;
        mineTrigger.timerParticles = timerParticles;

        triggerable = mineTrigger;
    }

    public override void ButtonDown()
    {
        mineTrigger.Hold();
    }

    public override void ButtonUp()
    {
        mineTrigger.Release();
    }
    
}
