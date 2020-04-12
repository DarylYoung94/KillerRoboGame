using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallAbilityTriggerable : MonoBehaviour
{
    [HideInInspector] public GameObject wallPrefab;
    public float maxRange = 5f;
    public float despawnTimer = 5f;

    // Update is called once per frame
    public void Place()
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
}
