using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu (fileName = "RecallAbility", menuName = "Abilities/RecallAbility")]

public class RecallAbility : AbstractAbility
{
    public GameObject recallPrefab;
    public float maxRange = 5f;

 private RecallAbilityTriggerable recallTrigger;

 public override void Initialise(GameObject obj)
    {
        recallTrigger = obj.AddComponent<RecallAbilityTriggerable>();
        recallTrigger.maxRange = maxRange;
        recallTrigger.recallPrefab = recallPrefab;
    }

    public override void ButtonDown()
    {
        recallTrigger.Recall();
    }
    public override void ButtonUp()
    {
        
    }

}
