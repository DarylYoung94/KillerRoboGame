﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu (fileName = "ThreeHitWeapon", menuName = "Weapons/ThreeHitWeapon")]
public class ThreeHitWeapon : RBWeapon
{
   public GameObject bulletPrefab;
   public GameObject healingBulletPrefab;
   public float despawnTimer = 5f;
   private ThreeHitTriggerable threeHitTrigger;
   // float force = 20f;


   public override void Initialise(GameObject Obj)
   {
        threeHitTrigger  = Obj.AddComponent<ThreeHitTriggerable>();
        threeHitTrigger.bulletPrefab = bulletPrefab;
        threeHitTrigger.healingBulletPrefab = healingBulletPrefab;
        threeHitTrigger.despawnTimer = despawnTimer;
        threeHitTrigger.force = force;

        if (!barrelExit)
            barrelExit = GameObject.Find("Player/firePoint").transform;

        threeHitTrigger.barrelExit = barrelExit;
        bulletPrefab.GetComponent<BulletCollider>().Damage = damage;

        triggerable = threeHitTrigger;
   }

    public override void ButtonDown()
    {
        threeHitTrigger.Shoot();
    }

    public override void ButtonUp()
    {
        
    }
}
