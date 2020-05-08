using System.Collections;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu (fileName = "WallAbility", menuName = "Abilities/WallAbility")]
public class WallAbility : AbstractAbility {

    public GameObject wallPrefab;
    public float maxRange = 5f;
    public float despawnTimer = 5f;

    private WallAbilityTriggerable wallPlace;
    
    public override void Initialise(GameObject obj)
    {
        wallPlace = obj.AddComponent<WallAbilityTriggerable>();
        wallPlace.maxRange = maxRange;
        wallPlace.wallPrefab = wallPrefab;
        wallPlace.despawnTimer = despawnTimer;
    }

    public override void ButtonDown()
    {
        wallPlace.Place();
    }

    public override void ButtonUp()
    {
        
    }
}