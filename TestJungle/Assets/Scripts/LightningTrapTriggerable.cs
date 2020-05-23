using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningTrapTriggerable : MonoBehaviour
{
    [HideInInspector] public GameObject lightningCreatorPrefab;
    [HideInInspector] public GameObject chainLightningPrefab;

    public float despawnTimer;
    public float rangeToPlace;
    public float startToEndRange;
    public float chainRange;
    public float damage;
    public bool applyChains;

    public Transform barrelExit;

    public Color highlightColor = Color.cyan;
    public List<WallColor> highlightedWalls = new List<WallColor>();
    GameObject startSphere, endSphere;

    GameObject lightningCreatorGO;

    public void Hold()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        int layerMask = 1 << 11;
        if (!startSphere && Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            Vector3 startAim = hit.point;
            float aimDistance  = Vector3.Distance(this.gameObject.transform.position, startAim);

            if (aimDistance <= rangeToPlace)
            {
                ShowRange();

                startSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                startSphere.transform.position = startAim;
                
            }
        }
    }

    public void Release()
    {

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        int layerMask = 1 << 11;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            Vector3 endAim = hit.point;
            float aimDistance  = Vector3.Distance(startSphere.transform.position, endAim);

            if (aimDistance <= startToEndRange && IsHighlightedWall(hit.transform.gameObject))
            {
                endSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                endSphere.transform.position = endAim;

                lightningCreatorGO = Instantiate(lightningCreatorPrefab, startSphere.transform.position, Quaternion.identity);
                lightningCreatorGO.transform.LookAt(endSphere.transform.position);
                lightningCreatorGO.GetComponent<LightningCreator>()
                                  .Setup(endSphere.transform.position, aimDistance, applyChains, chainRange, damage);
            }
        }

        ReleaseRange();

        Destroy(startSphere, 3.0f);
        Destroy(endSphere, 3.0f);
        Destroy(lightningCreatorGO, 3.0f);
    }

    public void ShowRange()
    {
        int layerMask = 1 << 11;
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, startToEndRange, layerMask);

        foreach (Collider hit in colliders)
        {
            WallColor wc = new WallColor(hit.transform.gameObject,
                                         hit.transform.GetComponent<Renderer>().material.color);

            if (!WallColorExistsInList(highlightedWalls, wc))                             
            {
                highlightedWalls.Add(wc);
                hit.transform.GetComponent<Renderer>().material.color = highlightColor;
            }
        }
   }

    public void ReleaseRange()
    {
        if (highlightedWalls.Count > 0)
        {
            for(int i=0; i < highlightedWalls.Count; i++)
            {
                highlightedWalls[i].gameObject.GetComponent<Renderer>().material.color = highlightedWalls[i].color;
            }
        }

        highlightedWalls.Clear();
    }

    public bool IsHighlightedWall(GameObject hitGO)
    {
        bool ret = false;

        if (highlightedWalls.Count > 0)
        {
            for(int i=0; i < highlightedWalls.Count; i++)
            {
                if (highlightedWalls[i].gameObject == hitGO)
                    ret = true;
            }
        }

        return ret;
    }

    public bool WallColorExistsInList(List<WallColor> list, WallColor wc)
    {
        bool ret = false;
        for (int i=0; i<list.Count; i++)
        {
            if(list[i].gameObject == wc.gameObject)
            {
                ret = true;
            }
        }

        return ret;
    }
}

public struct WallColor {
    public Color color;
    public GameObject gameObject; 

    public WallColor (GameObject gameObject, Color color) {
        this.gameObject = gameObject;
        this.color = color;
    }
}
