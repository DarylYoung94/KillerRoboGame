using System.Collections;
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
    public float lifestealAmount =5f;
    public bool isHealingBullet = false;
    public bool isMarkBullet = false;
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
            Enemy enemyHit = collision.transform.GetComponent<Enemy>();
            if (enemyHit != null)
            {
                if (isMarkBullet)
                {
                    MarkBulletCollision(collision.gameObject, enemyHit);
                }
                else
                {
                    enemyHit.TakeDamage(autoDamage);
                }


                if (isHealingBullet)
                {
                    player.GetComponent<PlayerManager>().health += lifestealAmount;
                }
            }

            Destroy(this.gameObject);
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

    public void MarkBulletCollision(GameObject GO, Enemy enemy)
    {
        // If not marked then set inital mark
        // Else take damage and reset the mark
        if (!GO.GetComponent<Mark>().IsMarked())
        {
            GO.GetComponent<Mark>().MarkEnemy();
        }
        else
        {
            enemy.TakeDamage(autoDamage);
            GO.GetComponent<Mark>().ResetMark();
        }
    }
}
