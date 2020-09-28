using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    public float openDistance = 1.5f;
    public GameObject door1, door2;

    private float distance = 0.0f;
    [SerializeField] private float desiredDistance = 0.0f;

    private Vector3 start1, start2;

    void Start()
    {
        start1 = door1.transform.localPosition;
        start2 = door2.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Mathf.Lerp(distance, desiredDistance, 2.0f * Time.deltaTime);

        door1.transform.localPosition =  start1 - new Vector3(0,0, distance);
        door2.transform.localPosition =  start2 + new Vector3(0,0, distance);
    }

    public void OpenDoor()
    {
        desiredDistance = openDistance;
    }

    public void CloseDoor()
    {
        desiredDistance = 0.0f;
    }
}
