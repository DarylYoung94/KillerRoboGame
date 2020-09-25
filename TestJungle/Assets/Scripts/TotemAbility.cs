using System.Collections;
using UnityEngine;

[CreateAssetMenu (fileName = "TotemAbility", menuName = "Abilities/TotemAbility")]
public class TotemAbility : AbstractAbility {

    public GameObject totemPrefab;
    public GameObject bulletPrefab;
    public float placementRange = 20f;
    
    public float totemBulletSpeed = 50f;
    public float despawnTimer = 5f;
    public GameObject projectorPrefab;
    public float maxRange = 20f;
    private TotemAbilityTriggerable totemPlacement;
    
    public override void Initialise(GameObject obj)
    {
        totemPlacement = obj.AddComponent<TotemAbilityTriggerable>();

        // Stats
        totemPlacement.placementRange = placementRange;
        totemPlacement.maxRange = maxRange;
        totemPlacement.despawnTimer = despawnTimer;
        totemPlacement.totemBulletSpeed = totemBulletSpeed;

        // Prefabs
        totemPlacement.totemPrefab = totemPrefab;
        totemPlacement.bulletPrefab = bulletPrefab;
        totemPlacement.projectorPrefab = projectorPrefab;

        triggerable = totemPlacement;
    }

    public override void ButtonDown()
    {
        totemPlacement.Hold();
    }

    public override void ButtonUp()
    {
        totemPlacement.Release();

    }
}