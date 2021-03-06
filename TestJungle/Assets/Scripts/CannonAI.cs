﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CannonAI : MonoBehaviour
{
    Transform target;
    public GameObject bombPrefab;
    public Transform firepoint;
    public Transform cannon;
    public Transform cannonBase;
    public NavMeshAgent agent;

    public float lookRadius = 25f;
    public float shootDistance = 15f;

    public float rotSpeed = 200f;
    public float newRot;
    
    
    public float attackTime = 3f;
    public float nextAttackTime = 1.0f;
    public float power = 10f;
    

    void Start() 
    {
        lookRadius = GetComponent<EnemyAI>().lookRadius;
        cannon = this.transform.Find("CannonBase/Cannon");
        cannonBase = this.transform.Find("CannonBase");
    }

    public void Update() 
    {
        float distance = Mathf.Infinity;

        if(target)
        {
            if(target.GetComponent<Enemy>() || target.GetComponent<PlayerManager>())
            {
                agent = GetComponent<NavMeshAgent>();
                agent.stoppingDistance = 20f;
            }
            else if (!target.GetComponent<Enemy>() || target.GetComponent<PlayerManager>() )
            {
                agent.stoppingDistance = 1f;
            }
        }

        if (target)
            distance = Vector3.Distance(this.transform.position, target.position);

        if (nextAttackTime > 0)
        {
            nextAttackTime -= Time.deltaTime;
        }

        if (nextAttackTime < 0)
        {
            nextAttackTime = 0;
        }

        if (distance <= shootDistance && nextAttackTime == 0)
        { 
            ShootBomb();
            nextAttackTime = attackTime;
        }

        if (distance > shootDistance)
        {
            nextAttackTime = attackTime;
        }


        if (distance <= lookRadius)
        {  
            Vector3 targetDir = target.position - transform.position;
            Vector3 newDir = Vector3.RotateTowards(transform.forward,targetDir, rotSpeed * Time.deltaTime, 100f);
            cannonBase.rotation = Quaternion.LookRotation(newDir);
            cannonBase.transform.eulerAngles = new Vector3(0f,  cannonBase.transform.eulerAngles.y, 0f);
        }
        else
        {
          Vector3 restRot = new Vector3(this.transform.eulerAngles.x, this.transform.eulerAngles.y ,this.transform.eulerAngles.z);
          cannonBase.transform.eulerAngles = Vector3.Lerp(cannonBase.transform.eulerAngles, restRot, Time.deltaTime);
        }

        if (distance <= shootDistance)
        {
            Vector3 arcDestination = new Vector3(45, cannon.transform.eulerAngles.y, cannon.transform.eulerAngles.z);
            cannon.transform.eulerAngles = Vector3.Lerp(cannon.transform.rotation.eulerAngles, arcDestination, Time.deltaTime);   
        }
        else
        {
            Vector3 arcDestination = new Vector3(90, cannon.transform.eulerAngles.y, cannon.transform.eulerAngles.z);
            cannon.transform.eulerAngles = Vector3.Lerp(cannon.transform.rotation.eulerAngles, arcDestination, Time.deltaTime);
        }
       
    }

    public void SetTarget()
    {
        target = transform.GetComponent<EnemyAI>().target;
    }

    void ShootBomb()
    {
        Transform firepoint = this.transform.Find("CannonBase/Cannon/CannonExit");
        firepoint.transform.rotation = cannon.transform.rotation;
        GameObject bombInstance = Instantiate(bombPrefab, firepoint.position, cannon.transform.rotation);
        bombInstance.GetComponent<EnemyBombCollider>().bombFaction = this.transform;
        Rigidbody bombRB = bombInstance.GetComponent<Rigidbody>();
        
        bombRB.AddForce(bombRB.transform.up * power, ForceMode.Impulse);
    }
}
