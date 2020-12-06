using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoHitAttackTriggerable : MonoBehaviour
{
      public float despawnTimer = 5f;

    public float force;
    public GameObject pelletPrefab;
   
    public Transform barrelExit;
    public bool firstShot =true;
    Vector3 aim;
    public float currentTimer =0f;
    public float maxTimer = 2f;
    int i = 0;

    void Update()
    {
        if(currentTimer < maxTimer)
        {
         currentTimer += Time.deltaTime;   
        }
        
    }
    public void Shoot(float damageMult =1f) 
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            
            aim = hit.point;

           
            Fire(pelletPrefab, damageMult);
          
        }
    }

    void Fire(GameObject firePrefab, float damageMult =1f)
    {
        GameObject basicBulletInstance = Instantiate(firePrefab, barrelExit.position, Quaternion.identity);
        basicBulletInstance.GetComponent<BulletCollider>().SetMultiplier (damageMult);
        Rigidbody rb = basicBulletInstance.GetComponent<Rigidbody>();
        rb.transform.LookAt(aim);
        rb.AddForce(rb.transform.forward * force, ForceMode.Impulse);
       
        
        Destroy(basicBulletInstance,3);
    }

    public void ShootFirst()
    {
        if(firstShot)
        {
            Shoot();
            firstShot = false;
            currentTimer =0f;
        }
    }
     public void ShootSecond()
    {
        if(!firstShot)
        {
           
            float multiplier = currentTimer/maxTimer;

            Shoot(multiplier);
            firstShot = true;
            
        }
    }


}
