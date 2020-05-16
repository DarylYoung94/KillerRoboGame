using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningCreator : MonoBehaviour
{
    public GameObject lightningPrefab;
    private GameObject[] lightningGOs = new GameObject[3];

    public GameObject rangePrefab;
    private GameObject sphere;
    private float range = 8.0f;
    public Color highlightColor = Color.blue;
    public List<WallColor> highlightedWalls;

    private float aimDist = 8.0f;
    private float chainRange = 20.0f;
    private bool applyChains = false;
    float damage = 0.5f;

    IEnumerator Start()
    {
        while(true)
        {
            lightningGOs[0] = Instantiate(lightningPrefab, this.transform.position,Quaternion.identity);
            lightningGOs[1] = Instantiate(lightningPrefab, this.transform.position,Quaternion.identity);
            lightningGOs[2] = Instantiate(lightningPrefab, this.transform.position,Quaternion.identity);

            for (int i=0; i<lightningGOs.Length; i++)
            {
                lightningGOs[i].GetComponent<Lightning>().Setup(null, range, applyChains, chainRange, lightningPrefab, damage);
                lightningGOs[i].transform.SetParent(this.gameObject.transform);
            }
            
            yield return null;
        }
    }

    public void ShowRange()
    {
        if (sphere == null)
        {
            sphere = Instantiate(rangePrefab, this.transform.position,Quaternion.identity);
            sphere.transform.parent = null;
            sphere.transform.localScale = new Vector3(range, range, range);
            sphere.transform.parent = this.transform;

            Collider[] colliders = Physics.OverlapSphere(this.transform.position, range);

            foreach (Collider hit in colliders)
            {
                if (hit.transform.tag == "Walls")
                {
                    highlightedWalls.Add(new WallColor(hit.transform.gameObject,
                                                       hit.transform.GetComponent<Renderer>().material.color));
                    hit.transform.GetComponent<Renderer>().material.color = highlightColor;
                }
            }
        }
   }

    public void ReleaseRange()
    {
        for(int i=0; i < highlightedWalls.Count; i++)
        {
            highlightedWalls[i].gameObject.GetComponent<Renderer>().material.color = highlightedWalls[i].color;
        }
        Destroy(sphere);
    }

    public void Setup(float aimDist, bool applyChains, float chainRange, GameObject chainPrefab, float damage)
    {
        this.aimDist = aimDist ;
        this.chainRange = chainRange;
        this.applyChains = applyChains;
        this.damage = damage;
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
