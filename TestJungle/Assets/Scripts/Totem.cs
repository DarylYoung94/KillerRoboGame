using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : MonoBehaviour
{
    public GameObject totemPrefab;
    public GameObject bulletPrefab;
    public GameObject totemFirePoint;

    Transform bestTarget = null;

    public float totemRange;
    public float totemBulletSpeed;

    public void Initialise (GameObject bulletPrefab,
                            GameObject totemPrefab,
                            float totemRange,
                            float totemBulletSpeed)
    {
        this.bulletPrefab = bulletPrefab;
        this.totemPrefab = totemPrefab;
        this.totemBulletSpeed = totemBulletSpeed;
        this.totemRange = totemRange;

        totemFirePoint = this.transform.Find("TotemFirePoint").gameObject;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            if (totemPrefab !=null)
            {
                FindClosestEnemy();
                TurretShot();
            }
        }
    }

    public void FindClosestEnemy()
    {
        Vector3 totemPosition = this.transform.position;
        Collider[] colliders = Physics.OverlapSphere(totemPosition, totemRange);

        float closestEnemy = Mathf.Infinity;

        foreach (Collider hit in colliders)
        {

            float distToEnemy = Vector3.Distance(hit.transform.position, totemPosition);
            Enemy enemyHit = hit.transform.GetComponent<Enemy>();

            if (enemyHit != null && distToEnemy < closestEnemy)
            {
                closestEnemy = distToEnemy;
                bestTarget = enemyHit.transform;
            }
        }

    }

    public void TurretShot()
    {
        GameObject turretBulletInstance;
        turretBulletInstance = Instantiate(bulletPrefab, totemFirePoint.transform.position, Quaternion.identity);

        Rigidbody bulletRB = turretBulletInstance.GetComponent<Rigidbody>();
        bulletRB.transform.LookAt(bestTarget);
        bulletRB.AddForce(bulletRB.transform.forward * totemBulletSpeed, ForceMode.Impulse);
    }
}
