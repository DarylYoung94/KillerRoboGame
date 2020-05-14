using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemAbilityTriggerable : MonoBehaviour
{
    [HideInInspector] public GameObject totemPrefab;
    [HideInInspector] public GameObject projectorPrefab;
    [HideInInspector] public GameObject bulletPrefab;
    public float placementRange = 5f;
    public float totemRange = 5f;
    public float totemBulletSpeed = 50f;
    public float despawnTimer = 5f;
    private GameObject projectorTarget;
    Vector3 totemAim = -Vector3.one;
    public float maxRange = 20f;
    public void Hold()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        int layerMask = 1 << 8;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            totemAim = hit.point;
            UpdateProjector();
        }

    }
    public void Release()
    {
        projectorTarget.SetActive(false);

        GameObject totemInstance = Instantiate(totemPrefab, totemAim, Quaternion.identity) as GameObject;
        
        StartCoroutine(ExecuteAfterTime(despawnTimer,totemInstance));

    }
   private void UpdateProjector()
    {
        Vector3 direction = totemAim - this.transform.position;
        if (direction.magnitude > maxRange)
        {
            totemAim = this.transform.position + direction.normalized * maxRange;
        }
        if (projectorTarget == null)
        {
            projectorTarget = Instantiate(projectorPrefab, totemAim, Quaternion.identity);
        }
        else
        {
            if (!projectorTarget.gameObject.activeSelf)
            {
                projectorTarget.gameObject.SetActive(true);
            }
            
            projectorTarget.transform.position = totemAim;
        }
    }
    IEnumerator ExecuteAfterTime(float despawnTimer, GameObject totemInstance)
    {
        yield return new WaitForSeconds(despawnTimer) ;
        Destroy(totemInstance);
        

    }
}
 // Update is called once per frame
    /*public void PlaceTotem()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 totemAim = hit.point;
            float range = Vector3.Distance(this.gameObject.transform.position, totemAim);
            
            if (range <= placementRange)
            {
                SpawnTotem(totemAim);
            }
        }
    }

    void SpawnTotem (Vector3 totemAim) 
    {
        GameObject turretInstance = Instantiate(totemPrefab, totemAim, Quaternion.identity);
        turretInstance.GetComponent<Totem>()
                      .Initialise(bulletPrefab,
                                  totemPrefab, 
                                  totemRange, 
                                  totemBulletSpeed);

        turretInstance.transform.LookAt(this.transform.position);
        turretInstance.transform.eulerAngles = new Vector3(0, turretInstance.transform.eulerAngles.y, 0);

        Destroy(turretInstance, despawnTimer);
    }*/