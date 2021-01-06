using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyAttack : MonoBehaviour
{
    public Transform target;
    public GameObject enemyBulletPrefab;
    public Transform firePoint;
    public float bulletSpeed;
    public float shootTimer;
    public float attackRange;

    private float timer = 0;
    private float distanceFromPlayer;

    private void Start()
    {
        shootTimer = Random.Range(2, 5);
    }

    private void Update()
    {
        distanceFromPlayer = Vector3.Distance(target.position, transform.position);

        timer += Time.deltaTime;
        if (timer >= shootTimer && distanceFromPlayer <= attackRange)
        {
            ShootAtPlayer();
            timer = 0;
        }
    }

    public void SetTarget()
    {
        target = transform.GetComponent<EnemyAI>().target;
    }

    void ShootAtPlayer()
    {
        GameObject enemyBulletInstance;
        enemyBulletInstance = Instantiate(enemyBulletPrefab, firePoint.transform.position, Quaternion.identity);
        
        Rigidbody bulletRB = enemyBulletInstance.GetComponent<Rigidbody>();
        bulletRB.transform.LookAt(target.position);
        bulletRB.AddForce(bulletRB.transform.forward * bulletSpeed, ForceMode.Impulse);
    }
}
