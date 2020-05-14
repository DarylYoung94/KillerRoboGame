using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningTriggerable : MonoBehaviour
{
    [HideInInspector] public GameObject lightningPrefab;
    [HideInInspector] public GameObject chainLightningPrefab;

    public float despawnTimer;
    public float range;
    public float chainRange;
    public float damage;
    public bool applyChains;

    public Transform barrelExit;

    public void Hold()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 lightningAim = hit.point;
            float aimDistance  = Vector3.Distance(this.gameObject.transform.position, lightningAim);

            if (aimDistance <= range)
            {
                GameObject lightningGO = Instantiate(lightningPrefab, barrelExit.position, Quaternion.identity);
                lightningGO.transform.SetParent(this.gameObject.transform);
                lightningGO.transform.LookAt(lightningAim);

                Lightning lightning = lightningGO.GetComponent<Lightning>();
                lightning.Setup(null, aimDistance, applyChains, chainRange, chainLightningPrefab, damage);
            }
        }
    }

    public void Release()
    {

    }
}
