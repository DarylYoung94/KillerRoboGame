using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliAbilityTriggerable : MonoBehaviour
{
   
    public GameObject droppedItem;
    public float dropTimer = 4f;
   
    public GameObject dropPoint;
    public Rigidbody RB;
    [HideInInspector] public GameObject heliPrefab;
    public GameObject projectorPrefab;
    private GameObject projectorTarget;
    Vector3 heliAim = -Vector3.one;
    public float heliSpeed = 5f;
    public float heightStart = 5.0f;
    public float despawnTimer = 7f;

    List<Rigidbody> rigidbodies = new List<Rigidbody>();

    public void Hold()
    {
        UpdateProjector();
    }

    private void UpdateProjector()
    {
        Transform firepoint = this.gameObject.transform.Find("firePoint");
        if (projectorTarget == null)
        {
            projectorTarget = Instantiate(projectorPrefab, firepoint.position + new Vector3(0,heightStart,0), Quaternion.identity);
        }
        else
        {
            if (!projectorTarget.gameObject.activeSelf)
            {
                projectorTarget.gameObject.SetActive(true);
            }
            
            projectorTarget.transform.position = firepoint.position + new Vector3(0,heightStart,0);
        }
    }
    
    public void Release()
    {
        projectorTarget.SetActive(false);

        Transform firepoint = this.gameObject.transform.Find("firePoint");
        GameObject HeliInstance = Instantiate(heliPrefab, firepoint.position + new Vector3(0,heightStart,0), Quaternion.identity) as GameObject;
        RB = HeliInstance.GetComponent<Rigidbody>();
        rigidbodies.Add(RB);

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            heliAim = hit.point;

        }

        HeliInstance.transform.LookAt(heliAim);

        RB.AddForce(RB.transform.forward * heliSpeed, ForceMode.Force);
        StartCoroutine(ExecuteAfterTime(despawnTimer,HeliInstance));
    }

    IEnumerator ExecuteAfterTime(float heliTime, GameObject heliInstance)
    {   
        yield return new WaitForSeconds(heliTime -2);
        EndHeli();
        yield return new WaitForSeconds(heliTime -3);
        Destroy(heliInstance);

    }
    
    void EndHeli()
    {
        rigidbodies[0].AddForce(RB.transform.up * heliSpeed*5, ForceMode.Force);
        rigidbodies.RemoveAt(0);
    }
 
}