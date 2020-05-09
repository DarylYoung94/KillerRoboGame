using System.Collections;
using UnityEngine;

[CreateAssetMenu (fileName = "LightningWeapon", menuName = "Weapons/LightningWeapon")]
public class LightningWeapon : RayCastWeapon {

    public GameObject lightningPrefab;
    public float despawnTimer = 0.5f;
    private Transform barrelExit;

    private LightningTriggerable lightningTrigger;
    
    public override void Initialise(GameObject obj)
    {
        lightningTrigger = obj.AddComponent<LightningTriggerable>();
        lightningTrigger.lightningPrefab = lightningPrefab;
        lightningTrigger.despawnTimer = despawnTimer;
        lightningTrigger.range = range;
        lightningTrigger.damage = damage;
        
        barrelExit = GameObject.Find("Player/firePoint").transform;
        lightningTrigger.barrelExit = barrelExit;

    }

    public override void ButtonDown()
    {
        lightningTrigger.Hold();
    }

    public override void ButtonUp()
    {
        lightningTrigger.Release();
    }
}