using System.Collections;
using UnityEngine;

[CreateAssetMenu (fileName = "BasicWeapon", menuName = "Weapons/BasicWeapon")]
public class BasicWeapon : RBWeapon {

    public GameObject pelletPrefab;
    public float despawnTimer = 5f;

    private BasicAttackTriggerable basicAttackTrigger;
    
    public override void Initialise(GameObject obj)
    {
        basicAttackTrigger = obj.AddComponent<BasicAttackTriggerable>();
        basicAttackTrigger.pelletPrefab = pelletPrefab;
        basicAttackTrigger.despawnTimer = despawnTimer;
        basicAttackTrigger.force = force;
        
        if (!barrelExit)
            barrelExit = GameObject.Find("Player/firePoint").transform;
        basicAttackTrigger.barrelExit = barrelExit;

        pelletPrefab.GetComponent<BulletCollider>().Damage = damage;

        triggerable = basicAttackTrigger;
    }

    public override void ButtonDown()
    {
        basicAttackTrigger.Shoot();
    }

    public override void ButtonUp()
    {
        
    }
}
