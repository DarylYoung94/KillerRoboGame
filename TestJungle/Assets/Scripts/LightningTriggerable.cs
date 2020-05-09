using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningTriggerable : MonoBehaviour
{
    [HideInInspector] public GameObject lightningPrefab;

    public float despawnTimer;
    public float range;
    public float damage;

    public Transform barrelExit;

    // Update is called once per frame
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
                GameObject lightning = Instantiate(lightningPrefab, this.transform.position, Quaternion.identity);
                lightning.transform.SetParent(this.gameObject.transform);
                lightning.GetComponent<Lightning>().SetTarget(lightningAim);
            }
        }
    }

    public void Release()
    {

    }
}
