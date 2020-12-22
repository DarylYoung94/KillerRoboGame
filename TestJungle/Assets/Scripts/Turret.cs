using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject turret;
    public float turretRange;
    public float closestEnemy;
    Transform bestTarget = null;
    public GameObject bullet;
    public GameObject turretFirePoint;
    private Rigidbody bulletRB;
    public float turretBulletSpeed;
    private float timer ;
    public float turretAttackSpeed = 1f;
    public GameObject turretGun;
    public float atkMultiplier = 0.5f;
    public void Update()
    {

        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }

        if(timer < 0 )
        {
            timer = 0;
        }

        if (timer == 0)
        {
            FindClosestEnemy();
            if (bestTarget != null)
            {
                turretGun.transform.LookAt(bestTarget);
                TurretShot();
                Debug.Log(bestTarget.name);
                
            }
          timer = turretAttackSpeed;
           
        }

         turretGun.transform.LookAt(bestTarget);

        


       if( Input.GetKeyDown(KeyCode.G))
        {
            FindClosestEnemy();
            TurretShot();
           
        }
    }
    public void FindClosestEnemy()
    {
        
        Vector3 turretPosition = turret.transform.position;
        closestEnemy = Mathf.Infinity;
        Collider[] colliders = Physics.OverlapSphere(turretPosition, turretRange);

        foreach (Collider hit in colliders)
        {
            
            float distToEnemy = Vector3.Distance(hit.transform.position, turretPosition);
            Enemy enemyHit = hit.transform.GetComponent<Enemy>();

            
            if (enemyHit != null && distToEnemy < closestEnemy)
            {
                closestEnemy = distToEnemy;
                bestTarget = enemyHit.transform;
            }
            turretGun = GameObject.Find("TurretLad/TurretGun") ;
            
        }


      
    }
    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "AtkSpd")
        {
            turretAttackSpeed = atkMultiplier;
           
        }
    }

    public void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "AtkSpd")
        {
            turretAttackSpeed = 1f;
        }
    }
    void TurretShot()
    {
        GameObject turretBulletInstance;
        turretBulletInstance = Instantiate(bullet, turretFirePoint.transform.position, Quaternion.identity);
        bulletRB = turretBulletInstance.GetComponent<Rigidbody>();
        bulletRB.transform.LookAt(bestTarget);
        bulletRB.AddForce(bulletRB.transform.forward * turretBulletSpeed, ForceMode.Impulse);

    }
}
