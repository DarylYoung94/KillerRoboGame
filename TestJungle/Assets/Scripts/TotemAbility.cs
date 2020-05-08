using System.Collections;
using UnityEngine;

[CreateAssetMenu (fileName = "TotemAbility", menuName = "Abilities/TotemAbility")]
public class TotemAbility : AbstractAbility {

    public GameObject totemPrefab;
    public GameObject bulletPrefab;
    public float placementRange = 20f;
    public float totemRange = 20f;
    public float totemBulletSpeed = 50f;
    public float despawnTimer = 5f;

    private TotemAbilityTriggerable totemPlacement;
    
    public override void Initialise(GameObject obj)
    {
        totemPlacement = obj.AddComponent<TotemAbilityTriggerable>();

        // Stats
        totemPlacement.placementRange = placementRange;
        totemPlacement.totemRange = totemRange;
        totemPlacement.despawnTimer = despawnTimer;
        totemPlacement.totemBulletSpeed = totemBulletSpeed;

        // Prefabs
        totemPlacement.totemPrefab = totemPrefab;
        totemPlacement.bulletPrefab = bulletPrefab;
    }

    public override void ButtonDown()
    {
        totemPlacement.PlaceTotem();
    }

    public override void ButtonUp()
    {
        
    }
}