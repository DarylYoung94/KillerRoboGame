using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{
    public GameObject basicBulletPrefab;
  
    public float cooldownTime;
    public float nextFireTime = 0;

    public GameObject firePoint;
    public Vector3 basicAtkAim;
    public float bulletSpeed ;
    public Rigidbody bulletRB;
    public float playerLevel;
    public GameObject player;
    public float attackSpeedMultipler = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 basicAtkAim = -Vector3.one;
        cooldownTime = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        SetCooldownByLevel(player.GetComponent<PlayerXP>().level);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            basicAtkAim = hit.point;
        }

        if (nextFireTime > 0)
        {
            nextFireTime -= Time.deltaTime;
        }

        if (nextFireTime < 0)
        {
            nextFireTime = 0;
        }

        if (Input.GetMouseButton(0) && nextFireTime == 0)
        {
            RBShoot();
            nextFireTime = cooldownTime * (1.0f/attackSpeedMultipler);
        }
    }

    void RBShoot()
    {
        GameObject basicBulletInstance = Instantiate(basicBulletPrefab, firePoint.transform.position, Quaternion.identity);
        bulletRB = basicBulletInstance.GetComponent<Rigidbody>();
        bulletRB.transform.LookAt(basicAtkAim);
        bulletRB.AddForce(bulletRB.transform.forward * bulletSpeed, ForceMode.Impulse);
    }

    public void SetASMultipler (float speed)
    {
        attackSpeedMultipler = speed;
    }

    public void SetCooldownByLevel (int level)
    {
        if (playerLevel == 1)
        {
            cooldownTime = 1.0f;
        }
        else if(playerLevel == 2)
        {
            cooldownTime = 0.9f;
        }
        else if (playerLevel == 3)
        {
            cooldownTime = 0.8f;
        }
        else if (playerLevel == 4)
        {
            cooldownTime = 0.7f;
        }
        else if (playerLevel == 5)
        {
            cooldownTime = 0.6f;
        }
    }
}
