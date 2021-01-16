using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class DataConsole : MonoBehaviour
{
    [SerializeField] int data = 10;
    [SerializeField] Transform collectionPoint;

    [Header ("VFX")]
    [SerializeField] Transform vfxSpawnPoint;
    [SerializeField] GameObject dataCubePrefab;
    [SerializeField] GameObject dataStreamPrefab;
    [SerializeField] List<GameObject> dataStreams = new List<GameObject>();
    [SerializeField] List<Transform> targets = new List<Transform>();

    void Update()
    {

        for (int i=0; i<targets.Count; i++)
        {
            if (targets[i] == null)
            {
                Destroy(dataStreams[i]);
                targets.RemoveAt(i);
                dataStreams.RemoveAt(i);  
            }
            else
            {
                dataStreams[i].GetComponent<VisualEffect>().SetVector3("Target Vector3", targets[i].position);
            }          
        }
    }

    public bool CollectData(Transform target)
    {
        bool ret = false;
        if (data > 0)
        {
            data--;
            ret = true;
        }

        if (ret)
        {
            GameObject go = Instantiate(dataCubePrefab, vfxSpawnPoint.position, Quaternion.identity);
            go.GetComponent<DataCubeVFX>().SetTarget(target);
        }

        return ret;
    }

    public int GetData() { return data; }

    public Transform GetCollectionPoint() { return collectionPoint; }

    public void StartStream(Transform target)
    {
        GameObject ds = Instantiate(dataStreamPrefab, vfxSpawnPoint.position, Quaternion.identity);
        // Set 'Target Transform' in property binder to 
        ds.GetComponent<VisualEffect>().SetVector3("Target Vector3", target.position);
        dataStreams.Add(ds);
        targets.Add(target);
    }

    public void CloseStream(Transform target)
    {
        // Get index of target
        int index = targets.IndexOf(target);

        // Clean up and remove effects and targets.
        Destroy(dataStreams[index]);
        targets.RemoveAt(index);
        dataStreams.RemoveAt(index);
    }
}
