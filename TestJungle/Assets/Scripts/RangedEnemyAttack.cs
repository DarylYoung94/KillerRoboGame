using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyAttack : MonoBehaviour
{
    public GameObject player;
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
        player = GameManager.instance.player;
    }

    private void Update()
    {
        distanceFromPlayer = Vector3.Distance(player.transform.position, this.gameObject.transform.position);

        timer += Time.deltaTime;
        if (timer >= shootTimer && distanceFromPlayer <= attackRange)
        {
            ShootAtPlayer();
            timer = 0;
        }
    }
    void ShootAtPlayer()
    {
        GameObject enemyBulletInstance;
        enemyBulletInstance = Instantiate(enemyBulletPrefab, firePoint.transform.position, Quaternion.identity);
        
        Rigidbody bulletRB = enemyBulletInstance.GetComponent<Rigidbody>();
        bulletRB.transform.LookAt(player.transform.position);
        bulletRB.AddForce(bulletRB.transform.forward * bulletSpeed, ForceMode.Impulse);
    }
}
