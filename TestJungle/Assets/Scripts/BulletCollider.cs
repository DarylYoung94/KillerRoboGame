﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollider : MonoBehaviour
{
    public Turret turretscript;
    public GameObject player;
    public Rigidbody bulletPrefab;
    public float autoDamage;
    public float dmgScale;
    public float Damage;
     void Start()
    {
        player = GameManager.instance.player;
        dmgScale = player.GetComponent<PlayerXP>().level*2 - 1;
        autoDamage = Damage + dmgScale;
        Destroy(this.gameObject, 5);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "RangedEnemy1" || collision.gameObject.tag == "ChargeEnemy" )
        {
            if (bulletPrefab != null)
            {
                Enemy enemyHit = collision.transform.GetComponent<Enemy>();
                if (enemyHit != null)
                {
                    enemyHit.TakeDamage(autoDamage);
                }

                Destroy(this.gameObject);
            }
        }
        else if (collision.gameObject.tag == "Bullet")
        {
            //do nothing
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
