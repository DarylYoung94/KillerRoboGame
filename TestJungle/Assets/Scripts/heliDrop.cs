using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heliDrop : MonoBehaviour
{
   public GameObject droppedItem;
   public float dropTimer = 4f;
   
    public GameObject dropPoint;

     void Start() 
    {
       
        StartCoroutine(ExecuteAfterTime(dropTimer,droppedItem));
    }
   
   IEnumerator ExecuteAfterTime(float dropTimer, GameObject droppedItem) 
    {
        yield return new WaitForSeconds(dropTimer) ;
        SpawnDrop();
    }

    void SpawnDrop()
    {
        GameObject droppedItemInstance = Instantiate(droppedItem, dropPoint.transform)as GameObject;
    }
}
