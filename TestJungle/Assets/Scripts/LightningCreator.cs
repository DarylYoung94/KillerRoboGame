using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningCreator : MonoBehaviour
{
    public Lightning lightningPrefab;

    IEnumerator Start() {

        while(true)
        {
            Instantiate(lightningPrefab, this.transform.position,Quaternion.identity);
            Instantiate(lightningPrefab, this.transform.position,Quaternion.identity);
            Instantiate(lightningPrefab, this.transform.position,Quaternion.identity);
            
            yield return null;
        }
    }
}
