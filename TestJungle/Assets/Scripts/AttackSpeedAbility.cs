using System.Collections;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu (fileName = "AttackSpeedAbility", menuName = "Abilities/AttackSpeedAbility")]
public class AttackSpeedAbility : AbstractAbility {

    public GameObject buffPrefab;
    public GameObject projectorPrefab;

    public float maxRange = 5f;
    public float despawnTimer = 8f;
    public float attackSpeedMultiplier = 2.0f;

    private AttackSpeedAbilityTriggerable attackSpeedTrigger;
    
    public override void Initialise(GameObject obj)
    {
        attackSpeedTrigger = obj.AddComponent<AttackSpeedAbilityTriggerable>();
        attackSpeedTrigger.maxRange = maxRange;
        attackSpeedTrigger.buffPrefab = buffPrefab;
        attackSpeedTrigger.projectorPrefab = projectorPrefab;
        attackSpeedTrigger.despawnTimer = despawnTimer;
        attackSpeedTrigger.attackSpeedMultiplier = attackSpeedMultiplier;
    }

    public override void ButtonDown()
    {
        attackSpeedTrigger.Hold();
    }

    public override void ButtonUp()
    {
        attackSpeedTrigger.Release();
    }
}
