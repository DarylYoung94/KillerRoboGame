using System.Collections;
using UnityEngine;

[CreateAssetMenu (fileName = "ShotgunWeapon", menuName = "Weapons/ShotgunWeapon")]
public class ShotgunWeapon : RBWeapon {

    public GameObject pelletPrefab;
    public float despawnTimer = 5f;
    public int pelletCount;
    public float verticalSpread;
    public float horizontalSpread;

    private Transform barrelExit;

    private ShotgunTriggerable sgTrigger;
    
    public override void Initialise(GameObject obj)
    {
        sgTrigger = obj.AddComponent<ShotgunTriggerable>();
        sgTrigger.pelletPrefab = pelletPrefab;
        sgTrigger.despawnTimer = despawnTimer;
        sgTrigger.pelletCount = pelletCount;
        sgTrigger.verticalSpread = verticalSpread;
        sgTrigger.horizontalSpread = horizontalSpread;
        sgTrigger.force = force;
        
        barrelExit = GameObject.Find("Player/firePoint").transform;
        sgTrigger.barrelExit = barrelExit;

        pelletPrefab.GetComponent<BulletCollider>().Damage = damage;

        sgTrigger.Initialise();
    }

    public override void ButtonDown()
    {
        sgTrigger.Shoot();
    }

    public override void ButtonUp()
    {
        
    }
}