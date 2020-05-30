using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WallAbilityTriggerable : MonoBehaviour
{
    [HideInInspector] public GameObject wallPrefab;
    [HideInInspector] public GameObject projectorPrefab;
    public float maxRange = 5f;
    public float despawnTimer = 5f;
    private GameObject projectorTarget;
    Vector3 wallAim = -Vector3.one;
    public Image icon;
 
    void Update()
    {
        
    }

    public void Hold()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        int layerMask = 1 << 8;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            wallAim = hit.point;
            UpdateProjector();
        }

    }    

    public void Release()
    {
        projectorTarget.SetActive(false);

        GameObject wallInstance = Instantiate(wallPrefab, wallAim, Quaternion.identity) as GameObject;
        wallInstance.transform.LookAt(new Vector3(this.transform.position.x,
                                                  wallInstance.transform.position.y,
                                                  this.transform.position.z));
        StartCoroutine(ExecuteAfterTime(despawnTimer,wallInstance));
    }


     private void UpdateProjector()
    {
        Vector3 direction = wallAim - new Vector3(this.transform.position.x, 0, this.transform.position.z);

        if (direction.magnitude > maxRange)
        {
            wallAim = new Vector3(this.transform.position.x, 0, this.transform.position.z) + direction.normalized * maxRange;    
        }
        if (projectorTarget == null)
        {
            projectorTarget = Instantiate(projectorPrefab, wallAim, Quaternion.identity);
            
        }
        else
        {
            if (!projectorTarget.gameObject.activeSelf)
            {
                projectorTarget.gameObject.SetActive(true);
            }
            projectorTarget.transform.LookAt(new Vector3(this.transform.position.x,
                                                        projectorTarget.transform.position.y,
                                                        this.transform.position.z));
            projectorTarget.transform.position = wallAim;
        }
    }


    IEnumerator ExecuteAfterTime(float despawnTime, GameObject wallInstance)
    {
        yield return new WaitForSeconds(despawnTime) ;
        Destroy(wallInstance);
        

    }


}


    // Update is called once per frame
   /* public void Place()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 wallAim = hit.point;
            float range = Vector3.Distance(this.gameObject.transform.position, wallAim);
            
            if (range <= maxRange)
            {
                SpawnWall(wallAim);
            }
        }
    }
    
    void SpawnWall(Vector3 wallAim)
    {
        
        GameObject wall = Instantiate(wallPrefab, wallAim, Quaternion.identity);
        wall.transform.LookAt(this.transform.position);
        wall.transform.eulerAngles = new Vector3(0, wall.transform.eulerAngles.y, 0);
        
        Destroy(wall, despawnTimer);
    }
}*/

