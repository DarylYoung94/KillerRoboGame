using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu (fileName = "WallAbility", menuName = "Abilities/WallAbility")]
public class WallAbility : AbstractAbility {

    public GameObject wallPrefab;
    public float maxRange = 5f;
    public float despawnTimer = 5f;
    public GameObject projectorPrefab;
    private WallAbilityTriggerable wallPlace;
    //public Image icon;
    
    public override void Initialise(GameObject obj)
    {
        wallPlace = obj.AddComponent<WallAbilityTriggerable>();
        wallPlace.maxRange = maxRange;
        wallPlace.wallPrefab = wallPrefab;
        wallPlace.despawnTimer = despawnTimer;
        wallPlace.projectorPrefab = projectorPrefab;
    }

    public override void ButtonDown()
    {
        wallPlace.Hold();
    }

    public override void ButtonUp()
    {
        wallPlace.Release();
    }
}