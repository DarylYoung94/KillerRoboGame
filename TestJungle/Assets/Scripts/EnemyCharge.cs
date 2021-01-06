using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharge : MonoBehaviour
{
    public Transform target;
    private Vector3 enemyAim;
    public float timer;
    public float chargeTimer;
    public float distanceFromPlayer;
    public float chargeRange;
    public Rigidbody enemyRB;
    public float chargeSpeed;
    public float chargeDamage;
    public bool charging ;

    public float chargingTimer;
    public float timer2;

    private void Start()
    {
        chargeTimer = 5;
        charging = true;
        enemyRB = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (target)
            distanceFromPlayer = Vector3.Distance(target.position, transform.position);
        else
            distanceFromPlayer = Mathf.Infinity;

        timer += Time.deltaTime;
        if (timer >= chargeTimer && distanceFromPlayer <= chargeRange)
        {
            Charge();
            charging = true;
            timer = 0;
        }

        timer2 += Time.deltaTime;
        if(timer2 >= chargingTimer)
        {
            charging = false;
        }
    }

    public void SetTarget()
    {
        target = transform.GetComponent<EnemyAI>().target;
    }

    void Charge()
    {
        timer2 = 0;
        enemyAim = target.transform.position;
        enemyRB.transform.LookAt(enemyAim);
        enemyRB.AddForce(enemyRB.transform.forward * chargeSpeed, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && charging == true)
        {
            PlayerManager playerHit = collision.transform.GetComponent<PlayerManager>();
            if (playerHit != null)
            {
                Debug.Log("take damage");
                playerHit.TakeDamage(chargeDamage);
            }
        }
    }
}
