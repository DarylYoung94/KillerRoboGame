using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletCollider : MonoBehaviour
{
    public Rigidbody bulletPrefab;
    public float autoDamage;
    public GameObject player;
    public GameObject hitVfx;
    public FactionType.Faction thisFaction;

    private void Start()
    {
            thisFaction = this.GetComponent<FactionType>().faction;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.gameObject.GetComponent<FactionType>().faction != thisFaction)
        {
            Debug.Log("faction isnt my faction");
            if(collision.transform.GetComponent<Enemy>())
            {
                Enemy enemyHit = collision.transform.GetComponent<Enemy>();
                enemyHit.TakeDamage(autoDamage, this.transform);
                TriggerHitVFX(collision);
                Destroy(this.gameObject);
            }
            if(collision.transform.GetComponent<PlayerManager>())
            {
                PlayerManager playerHit = collision.transform.GetComponent<PlayerManager>();
                playerHit.TakeDamage(autoDamage);
                TriggerHitVFX(collision);
                Destroy(this.gameObject);
            }

        }

/*
        if (collision.gameObject.tag == "Player")
        {
            PlayerManager playerHit = collision.transform.GetComponent<PlayerManager>();
            if (playerHit != null)
            {
               // Debug.Log("take damage");
                playerHit.TakeDamage(autoDamage);
            }
            
            TriggerHitVFX(collision);
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.tag != "Enemy")
        {
            TriggerHitVFX(collision);
            Destroy(this.gameObject);
        }
        */
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
