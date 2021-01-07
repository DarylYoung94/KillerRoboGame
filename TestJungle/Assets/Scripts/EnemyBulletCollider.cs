using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletCollider : MonoBehaviour
{
    public Rigidbody bulletPrefab;
    public float autoDamage;
    public GameObject player;
    public GameObject hitVfx;

    private void OnCollisionEnter(Collision collision)
    {
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
