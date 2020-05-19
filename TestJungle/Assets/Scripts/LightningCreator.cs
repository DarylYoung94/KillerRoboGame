using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningCreator : MonoBehaviour
{
    public GameObject lightningPrefab;
    private GameObject[] lightningGOs = new GameObject[3];

    private Vector3 aim = Vector3.forward;
    private float aimDist = 8.0f;
    private float chainRange = 0.0f;
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
                lightningGOs[i].GetComponent<Lightning>().Setup(null, aimDist, applyChains, chainRange, lightningPrefab, damage);
                lightningGOs[i].transform.SetParent(this.gameObject.transform);
                lightningGOs[i].transform.LookAt(aim);
            }
            
            yield return null;
        }
    }

    public void Setup(Vector3 aim, float aimDist, bool applyChains, float chainRange, float damage)
    {
        this.aim = aim;
        this.aimDist = aimDist ;
        this.chainRange = chainRange;
        this.applyChains = applyChains;
        this.damage = damage;

        StartCoroutine(Start());
    }
}
