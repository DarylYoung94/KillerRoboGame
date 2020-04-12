using System.Collections;
using UnityEngine;

[CreateAssetMenu (menuName = "Abilities/BombAbility")]
public class BombAbility : AbstractAbility {

    public GameObject bombPrefab;
    public GameObject projectorPrefab;

    public float maxRange = 5f;
    public float despawnTimer = 5f;
    public float projectileSpeed = 5f;

    private BombAbilityTriggerable bombTrigger;
    
    public override void Initialise(GameObject obj)
    {
        bombTrigger = obj.AddComponent<BombAbilityTriggerable>();
        bombTrigger.bombPrefab = bombPrefab;
        bombTrigger.projectorPrefab = projectorPrefab;
        bombTrigger.despawnTimer = despawnTimer;
        bombTrigger.projectileSpeed = projectileSpeed;
    }

    public override void ButtonDown()
    {
        bombTrigger.Hold();
    }

    public override void ButtonUp()
    {
        bombTrigger.Release();
    }
}
