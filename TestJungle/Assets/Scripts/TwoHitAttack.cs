using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu (fileName = "TwoHitWeapon", menuName = "Weapons/TwoHitWeapon")]

public class TwoHitAttack : RBWeapon
{
    public GameObject pelletPrefab;
    public float despawnTimer = 5f;
    private Transform barrelExit;
    private TwoHitAttackTriggerable twoHitAttackTrigger;

    public override void Initialise(GameObject obj)
    {
        twoHitAttackTrigger = obj.AddComponent<TwoHitAttackTriggerable>();
        twoHitAttackTrigger.pelletPrefab = pelletPrefab;
        twoHitAttackTrigger.despawnTimer = despawnTimer;
        twoHitAttackTrigger.force = force;
        
        barrelExit = GameObject.Find("Player/firePoint").transform;
        twoHitAttackTrigger.barrelExit = barrelExit;

        pelletPrefab.GetComponent<BulletCollider>().Damage = damage;
    }

    public override void ButtonDown()
    {
        twoHitAttackTrigger.ShootFirst();
    }

    public override void ButtonUp()
    {
        twoHitAttackTrigger.ShootSecond();
    }
}
