using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAbilityTriggerable : MonoBehaviour
{
    [HideInInspector] public GameObject turretPrefab;
    [HideInInspector] public GameObject projectorPrefab;    
 
    private GameObject projectorTarget;
    Vector3 turretAim = Vector3.one;
    public float maxRange = 20f;
    public float despawnTimer = 5f;
    public float damage = 10f;
    
    
    public void Hold()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        int layerMask = 1 << 8;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            turretAim = hit.point;
            UpdateProjector();
        }
        
    }

     public void Release()
    {
        projectorTarget.SetActive(false);

     RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

       
        if (Physics.Raycast(ray, out hit))
        {
           
            float range = Vector3.Distance(this.gameObject.transform.position, turretAim);
            GameObject turretInstance = Instantiate(turretPrefab, turretAim, Quaternion.identity) as GameObject;
            StartCoroutine(ExecuteAfterTime(despawnTimer,turretInstance));
        }   

       
        
    }
    private void UpdateProjector()
    {
        Vector3 direction = turretAim - this.transform.position;
        if (direction.magnitude > maxRange)
        {
            turretAim = this.transform.position + direction.normalized * maxRange;
        }
        if (projectorTarget == null)
        {
            projectorTarget = Instantiate(projectorPrefab, turretAim, Quaternion.identity);
        }
        else
        {
            if (!projectorTarget.gameObject.activeSelf)
            {
                projectorTarget.gameObject.SetActive(true);
                projectorTarget.gameObject.GetComponent<Totem>();
                
            }
            
            projectorTarget.transform.position = turretAim;
        }
    }
    IEnumerator ExecuteAfterTime(float turretTime, GameObject turretInstance)
    {
        yield return new WaitForSeconds(turretTime) ;
        Destroy(turretInstance);
    }
    /*void PlaceTurret()
    {
                GameObject turretInstance = Instantiate(turretPrefab, turretAim, Quaternion.identity) as GameObject;

    }*/
}
   
   