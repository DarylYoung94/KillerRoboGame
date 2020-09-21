using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliAbilityTriggerable : MonoBehaviour
{
   
    public GameObject droppedItem;
    public float dropTimer = 4f;
   
    public GameObject dropPoint;
    public bool Fired = false;
    public Rigidbody RB;
    [HideInInspector] public GameObject heliPrefab;
    public GameObject projectorPrefab;
    private GameObject projectorTarget;
    Vector3 heliAim = -Vector3.one;
    public float heliSpeed = 5f;
    public float despawnTimer = 7f;
    public void Hold()
    {
        UpdateProjector();
    }

    private void UpdateProjector()
    {
        Transform firepoint = this.gameObject.transform.Find("firePoint");
        if (projectorTarget == null)
        {
            projectorTarget = Instantiate(projectorPrefab, firepoint.position , Quaternion.identity);
        }
        else
        {
            if (!projectorTarget.gameObject.activeSelf)
            {
                projectorTarget.gameObject.SetActive(true);
            }
            
            projectorTarget.transform.position = firepoint.position;
        }
    }
    
        
    
     public void Release()
    {


        projectorTarget.SetActive(false);

        Transform firepoint = this.gameObject.transform.Find("firePoint");
        GameObject HeliInstance = Instantiate(heliPrefab, firepoint.position, Quaternion.identity) as GameObject;
        Rigidbody heliRB = HeliInstance.GetComponent<Rigidbody>();
        RB = heliRB;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            heliAim = hit.point;
            
        }

        HeliInstance.transform.LookAt(heliAim);

        heliRB.AddForce(heliRB.transform.forward * heliSpeed, ForceMode.Force);
        StartCoroutine(ExecuteAfterTime(despawnTimer,HeliInstance,Fired));
        
         
    }
    IEnumerator ExecuteAfterTime(float heliTime, GameObject heliInstance, bool Fired)
    {   
        yield return new WaitForSeconds( heliTime -2);
        EndHeli();
        yield return new WaitForSeconds(heliTime -3);
        Destroy(heliInstance);

    }
    
    void EndHeli()
     {
         RB.AddForce(RB.transform.up * heliSpeed*5, ForceMode.Force);
     }
 
}