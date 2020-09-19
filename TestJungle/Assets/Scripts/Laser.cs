using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public GameObject vfxPrefab;
    private GameObject vfx;

    public float timer = 2.0f;
    
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {  
        lineRenderer.SetPosition(0,transform.position);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider)
            {
                lineRenderer.SetPosition(1, hit.point);
                if (vfx == null)
                {
                    vfx = Instantiate(vfxPrefab, hit.point, Quaternion.identity, this.transform);
                }
                else
                {
                    vfx.transform.position = hit.point;
                }
            }
        }
        else
        {
            lineRenderer.SetPosition(1, transform.forward * 100);
        }
    }
}
