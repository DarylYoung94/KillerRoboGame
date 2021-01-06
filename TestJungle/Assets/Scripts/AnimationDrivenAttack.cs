using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationDrivenAttack : MonoBehaviour
{
    public Transform target;
    public GameObject enemyBulletPrefab;
    public GameObject muzzlePrefab;
    public Transform firePoint;
    public float bulletSpeed;
    public float shootTolerance = 0.01f;
    public float rotSpeed = 1.0f;

    private bool shotReady = true;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(animator.GetFloat("ShootCurve") > shootTolerance)
        {
            if (shotReady && target != null)
            {
                ShootAtTarget();
                shotReady = false;
            }
        }
        else
        {
            shotReady = true;
        }
    }

    public void SetTarget()
    {
        target = transform.GetComponent<EnemyAI>().target;
    }

    void ShootAtTarget()
    {
        GameObject enemyBulletInstance = Instantiate(enemyBulletPrefab, firePoint.transform.position, Quaternion.identity);
        
        Rigidbody bulletRB = enemyBulletInstance.GetComponent<Rigidbody>();
        bulletRB.transform.LookAt(target.position);
        bulletRB.AddForce(bulletRB.transform.forward * bulletSpeed, ForceMode.Impulse);

        TriggerVFX();
    }

    public void TriggerVFX()
    {
        GameObject muzzleVFX = Instantiate(muzzlePrefab, firePoint.transform.position, Quaternion.identity);
        muzzleVFX.transform.LookAt(target.position);
    }
}
