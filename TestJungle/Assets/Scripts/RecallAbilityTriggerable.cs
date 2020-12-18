using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecallAbilityTriggerable : MonoBehaviour
{
    public GameObject recallPrefab;
    public GameObject marker;
    public float maxRange = 5f; 
    private bool firstPress = true;
    Vector3 telePos = - Vector3.one;
    Vector3 recallAim = -Vector3.one;

    public void Recall()
    {
        if(firstPress)
        {
           SetMarker(); 
           firstPress = false;
        }
        else
        {
            RecallToMarker();
            firstPress = true;
        }
    }
    
    public void SetMarker()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            recallAim = hit.point;
            float range =Vector3.Distance(this.gameObject.transform.position, recallAim);
            Vector3 direction = recallAim - this.transform.position;
            if(direction.magnitude > maxRange)
            {
                recallAim =this.transform.position +direction.normalized *maxRange;
            }
            Vector3 recallPos =  new Vector3(recallAim.x, this.gameObject.transform.position.y, recallAim.z);
            GameObject markerInstance = Instantiate(recallPrefab,recallPos, Quaternion.identity);
           markerInstance.name = "marker";
        }
    }

    public void RecallToMarker()
    {
        GameObject marker = GameObject.Find("marker");
        Vector3 markerPos = marker.gameObject.transform.position;
        Vector3 telePos = new Vector3 (markerPos.x, (markerPos.y + 0.7f), markerPos.z);
        this.gameObject.transform.position = telePos;
        Destroy(marker);
    } 
}



