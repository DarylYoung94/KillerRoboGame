using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollider : MonoBehaviour
{
    public Turret turretscript;
    public GameObject player;
    public GameObject hitVfx;
    public Rigidbody bulletPrefab;
    public float autoDamage;
    public float dmgScale;
    public float Damage;
    public float lifestealAmount =5f;
    public bool isHealingBullet = false;
    public bool isMarkBullet = false;

    float multiplier = 1f;

     void Start()
    {
        player = GameManager.instance.player;
        dmgScale = player.GetComponent<PlayerXP>().level*2 - 1;
        autoDamage = (Damage + dmgScale)*multiplier;
        Destroy(this.gameObject, 5);
    }

    public void SetMultiplier(float multiplier)
    {
        
        this.multiplier = multiplier;
    }

    void Update()
    {         
        dmgScale = player.GetComponent<PlayerXP>().level*2 - 1;
        autoDamage = (Damage + dmgScale)*multiplier;
    }

    public void OnCollisionEnter(Collision collision)
    {
        Enemy enemyHit = collision.transform.GetComponent<Enemy>();
        if (enemyHit)
        {
            if (isMarkBullet)
            {
                MarkBulletCollision(collision.gameObject, enemyHit);
            }
            else
            {
                enemyHit.TakeDamage(autoDamage, player.transform);
            }


            if (isHealingBullet)
            {
                player.GetComponent<PlayerManager>().health += lifestealAmount;
            }

            TriggerHitVFX(collision);
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
            enemy.TakeDamage(autoDamage, player.transform);
            GO.GetComponent<Mark>().ResetMark();
        }
    }

    private void TriggerHitVFX(Collision collision)
    {
        ContactPoint contactPoint = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contactPoint.normal);
        
        if (hitVfx != null)
        {
            GameObject vfx = Instantiate(hitVfx, contactPoint.point, Quaternion.identity);
        }
    }
}

