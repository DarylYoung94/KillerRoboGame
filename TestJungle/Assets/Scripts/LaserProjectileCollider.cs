using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserProjectileCollider : MonoBehaviour
{
    public GameObject explosionVFX;
    public float radius = 10f;
    public float power = 10f;
    public float upForce = 2f;
    public float damage = 10f;

    void OnCollisionEnter(Collision collision)
    {
        Detonate();
        Destroy(this.gameObject);            
    }

    public void Detonate()
    {
        GameObject explosion = Instantiate(explosionVFX.gameObject, transform.position, Quaternion.identity);
        Destroy(explosion, 5.0f);

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider hit in colliders)
        {
            Enemy enemyHit = hit.transform.GetComponent<Enemy>();
            if (enemyHit != null)
            {
                enemyHit.TakeDamage(damage);
            }

            PlayerManager pm = hit.transform.GetComponent<PlayerManager>();
            if (pm != null)
            {
                pm.TakeDamage(damage);
            }

            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(power, transform.position, radius, upForce, ForceMode.Impulse);
            }
        }
    }
}