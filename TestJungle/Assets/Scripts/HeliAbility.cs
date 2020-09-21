using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu (fileName = "HeliAbility", menuName = "Abilities/HeliAbility")]
public class HeliAbility : AbstractAbility
{
    public GameObject droppedItem;
    public float dropTimer = 4f;
    public GameObject dropPoint;

    public GameObject heliPrefab;
    public GameObject projectorPrefab;
    private HeliAbilityTriggerable heliTrigger;
    public float heliSpeed = 5f;
    public float despawnTimer = 5f;
     
    public override void Initialise(GameObject obj)
    {
        heliTrigger = obj.AddComponent<HeliAbilityTriggerable>();
        heliTrigger.heliPrefab = heliPrefab;
        heliTrigger.projectorPrefab = projectorPrefab;
        heliTrigger.heliSpeed = heliSpeed;
        heliTrigger.despawnTimer = despawnTimer;
        heliTrigger.droppedItem = droppedItem;
        heliTrigger.dropTimer = dropTimer;
        heliTrigger.dropPoint = dropPoint;
    }
      public override void ButtonDown()
    {
        heliTrigger.Hold();
    }

    public override void ButtonUp()
    {
        heliTrigger.Release();
    }
}
