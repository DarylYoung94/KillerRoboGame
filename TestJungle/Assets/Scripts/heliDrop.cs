using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heliDrop : MonoBehaviour
{
    [Header ("Drop")]
    public GameObject droppedItem;
    public float dropTimer = 4f;
    public GameObject dropPoint;

    [Header ("Helicopter")]
    public float minHeight = 1.0f;
    public float minRotationX = -45.0f, maxRotationX = 45.0f;
    public bool clampRotationX = true;
    public LayerMask layerMask;
    public float heightLerpSpeed = 2.0f;
    private float lerpTime = 0.0f;
    private bool lerpingHeight = false;
    private float desiredHeight;
    private Transform heightChecker;
    

    void Start() 
    {
        StartCoroutine(ExecuteAfterTime(dropTimer,droppedItem));
        heightChecker = this.transform.Find("HeightChecker");
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(heightChecker.position, Vector3.down, out hit, minHeight, layerMask))
        {
            float yMagnitude = Mathf.Clamp(heightChecker.position.y - hit.point.y, minHeight, Mathf.Infinity);
            desiredHeight = this.transform.position.y + yMagnitude;
            lerpingHeight = true;
        }

        if (lerpingHeight)
        {
            if(this.transform.position.y < desiredHeight)
            {
                float currentHeight =  Mathf.Lerp(this.transform.position.y, desiredHeight, Time.deltaTime * heightLerpSpeed);
                this.transform.position = new Vector3(this.transform.position.x, currentHeight, this.transform.position.z);
            }
            else
            {
                lerpingHeight = false;
            }
        }

        if (clampRotationX)
        {
            float xRotation = Mathf.Clamp(this.transform.localEulerAngles.x, minRotationX, maxRotationX);
            this.transform.localEulerAngles = new Vector3 (xRotation, this.transform.localEulerAngles.y, this.transform.localEulerAngles.z);
        }
    }
   
    IEnumerator ExecuteAfterTime(float dropTimer, GameObject droppedItem) 
    {
        yield return new WaitForSeconds(dropTimer) ;
        SpawnDrop();
    }

    void SpawnDrop()
    {
        GameObject droppedItemInstance = Instantiate(droppedItem, dropPoint.transform) as GameObject;
    }
}
