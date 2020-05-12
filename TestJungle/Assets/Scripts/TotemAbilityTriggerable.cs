using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemAbilityTriggerable : MonoBehaviour
{
    [HideInInspector] public GameObject totemPrefab;
    [HideInInspector] public GameObject bulletPrefab;
    public float placementRange = 5f;
    public float totemRange = 5f;
    public float totemBulletSpeed = 50f;
    public float despawnTimer = 5f;

    // Update is called once per frame
    public void PlaceTotem()
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
    }
}
