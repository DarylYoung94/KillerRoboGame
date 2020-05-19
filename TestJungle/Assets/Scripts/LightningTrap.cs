using System.Collections;
using UnityEngine;

[CreateAssetMenu (fileName = "LightningTrap", menuName = "Abilities/LightningTrap")]
public class LightningTrap : AbstractAbility {

    public GameObject lightningCreatorPrefab;
    public GameObject chainLightningPrefab;
    public float despawnTimer = 0.5f;

    public float range = 8.0f;
    public float startToEndRange = 20.0f;
    public float chainRange = 4.0f;
    public bool applyChains = false;

    public float damage = 0.5f;

    private Transform barrelExit;
    private LightningTrapTriggerable trapTriggerable;
    
    public override void Initialise(GameObject obj)
    {
        trapTriggerable = obj.AddComponent<LightningTrapTriggerable>();
        trapTriggerable.lightningCreatorPrefab = lightningCreatorPrefab;
        trapTriggerable.despawnTimer = despawnTimer;
        trapTriggerable.rangeToPlace = range;
        trapTriggerable.startToEndRange = startToEndRange;
        trapTriggerable.chainRange = chainRange;
        trapTriggerable.damage = damage;
        trapTriggerable.applyChains = applyChains;
        trapTriggerable.chainLightningPrefab = chainLightningPrefab;
    }

    public override void ButtonDown()
    {
        trapTriggerable.Hold();
    }

    public override void ButtonUp()
    {
        trapTriggerable.Release();
    }
}