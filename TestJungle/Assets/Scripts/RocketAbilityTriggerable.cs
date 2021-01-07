using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketAbilityTriggerable : MonoBehaviour
{
    public GameObject rocketPrefab;
    
    [HideInInspector]public GameObject explosionParticles;
    [HideInInspector]public GameObject firePoint;
    [HideInInspector]public float upForce = 2f;
    [HideInInspector]public float rotSpeed = 1000f;

    public float radius = 20f;
    public float power = 10f;
    public float verticalForce = 10f;
    public float Damage;
    public float maxRange = 20f;

    private bool spawned = false;
    private Quaternion lookRotation;
    private Vector3 direction;
    private GameObject rocket;
    Vector3 rocketAim = -Vector3.one;

    public void Hold()
    {
        FireRocket();
    }


    public void Release()
    {
        spawned = false;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            rocketAim = hit.point;
            AimRocket();
        }
    }
    
    public void FireRocket()
    {
        if(spawned ==false)
        {
        Transform firepoint = this.gameObject.transform.Find("firePoint");
        GameObject rocketInstance = Instantiate(rocketPrefab, firepoint.position, Quaternion.identity) as GameObject ;
        rocketInstance.transform.Rotate(-90f,0f, 0f);
        rocketInstance.name = "rocket";
        Rigidbody rocketRB = rocketInstance.GetComponent<Rigidbody>();
        rocketRB.AddForce(rocketRB.transform.forward *verticalForce, ForceMode.Impulse); 
        spawned = true;
        }
    }
  
    public void AimRocket()
    {
        rocket = GameObject.Find("rocket");
        direction = (rocketAim - rocket.transform.position);
        lookRotation = Quaternion.LookRotation(direction, Vector3.up).normalized;
        rocket.transform.rotation = Quaternion.Lerp(rocket.transform.rotation, lookRotation, Time.deltaTime * rotSpeed);
        Rigidbody rocketRB1 = rocket.GetComponent<Rigidbody>();
        rocketRB1.AddForce(rocket.transform.forward *power, ForceMode.Impulse); 
    }     

    public void Detonate(GameObject rocketInstance)
    {
        if(rocketInstance != null)
        {
            GameObject expParticles;
            expParticles = Instantiate (explosionParticles.gameObject, rocketInstance.transform.position , Quaternion.identity );
            Vector3 explosionPosition = rocketInstance.transform.position;

            Collider[] colliders = Physics.OverlapSphere(explosionPosition, radius);
                
            foreach (Collider hit in colliders)
            {
                Enemy enemyHit = hit.transform.GetComponent<Enemy>();
                if (enemyHit != null)
                {
                    enemyHit.TakeDamage(Damage, this.transform);
                    Destroy(rocketInstance);
                }
                Rigidbody rb = hit.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(power, explosionPosition, radius, upForce, ForceMode.Impulse);
                }
            }                
        }
        
    }  
}



