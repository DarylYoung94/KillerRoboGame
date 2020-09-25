using System.Collections;
using UnityEngine;

[CreateAssetMenu (fileName = "LightningWeapon", menuName = "Weapons/LightningWeapon")]
public class LightningWeapon : RayCastWeapon {

    public GameObject lightningPrefab;
    public GameObject chainLightningPrefab;
    public float despawnTimer = 0.5f;
    public float chainRange = 4.0f;
    public bool applyChains = false;

    private Transform barrelExit;
    private LightningTriggerable lightningTrigger;
    
    public override void Initialise(GameObject obj)
    {
        lightningTrigger = obj.AddComponent<LightningTriggerable>();
        lightningTrigger.lightningPrefab = lightningPrefab;
        lightningTrigger.despawnTimer = despawnTimer;
        lightningTrigger.range = range;
        lightningTrigger.damage = damage;
        lightningTrigger.chainRange = chainRange;
        lightningTrigger.applyChains = applyChains;
        lightningTrigger.chainLightningPrefab = chainLightningPrefab;
        
        barrelExit = GameObject.Find("Player/firePoint").transform;
        lightningTrigger.barrelExit = barrelExit;

        triggerable = lightningTrigger;
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