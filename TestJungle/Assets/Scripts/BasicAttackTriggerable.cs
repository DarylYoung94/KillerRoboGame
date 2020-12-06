using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttackTriggerable : MonoBehaviour
{
    public float despawnTimer = 5f;

    public float force;
    public GameObject pelletPrefab;
   
    public Transform barrelExit;

    Vector3 aim;
    int i = 0;

    public void Shoot() 
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            
            aim = hit.point;

           
            Fire(pelletPrefab);
          
        }
    }

    void Fire(GameObject firePrefab)
    {
        GameObject basicBulletInstance = Instantiate(firePrefab, barrelExit.position, Quaternion.identity);
        Rigidbody rb = basicBulletInstance.GetComponent<Rigidbody>();
        rb.transform.LookAt(aim);
        rb.AddForce(rb.transform.forward * force, ForceMode.Impulse);

        Destroy(basicBulletInstance,3);
    }
}