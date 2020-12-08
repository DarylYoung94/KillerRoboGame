using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class DataCollector : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] Transform target;
    [SerializeField] Transform collectionPoint;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] private float currentSpeed = 1.0f;
    [SerializeField] private float maxSpeed = 5.0f;

    [Header("Collection")]
    [SerializeField] DataManager data;
    [SerializeField] float collectRange = 5.0f;
    [SerializeField] float collectTime = 0.0f;
    [SerializeField] float collectCooldown = 0.5f;
    [SerializeField] bool collecting = false;
    [SerializeField] Transform antennaTransform;

    [Header("Events")]
    [SerializeField] UnityEvent foundTargetEvent = new UnityEvent();
    [SerializeField] UnityEvent collectDataEvent = new UnityEvent();
    [SerializeField] UnityEvent finishCollectingEvent =new UnityEvent();

    void Start()
    {
        foundTargetEvent.AddListener(FoundTarget);
        agent = GetComponent<NavMeshAgent>();

        GetNearestDataConsole();

        if (target != null)
        {
            foundTargetEvent.Invoke();
        }

        if (data == null)
        {
            this.gameObject.GetComponent<DataManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            GetNearestDataConsole();
        }
        else if (target != null && 
                 Vector3.Distance(target.position, this.transform.position) < collectRange)
        {
            if (collecting)
            {
                CollectDataFromConsole();
            }
            else
            {
                collecting = true;
                collectDataEvent.Invoke();
                target.GetComponent<DataConsole>().StartStream(antennaTransform);
            }
        }
        else
        {
            this.transform.LookAt(target);
        }

        collectTime += Time.deltaTime;
    }

    public void FoundTarget()
    {
        agent.SetDestination(collectionPoint.position);
    }

    public void CollectDataFromConsole()
    {
        DataConsole dc = target.GetComponent<DataConsole>();
        if (collectTime > collectCooldown)
        {
            if (dc != null && dc.GetData() > 0)
            {
                if (dc.CollectData(antennaTransform))
                {
                    collectTime = 0.0f;
                    data.CollectData(1);
                }
            }
            else
            {
                finishCollectingEvent.Invoke();
                
                if (dc != null)
                {
                    dc.CloseStream(antennaTransform);
                }

                collecting = false;
                target = null;
                collectionPoint = null;
            }
        }
    }

    private void GetNearestDataConsole()
    {
        DataConsole[] dataConsoles = Object.FindObjectsOfType<DataConsole>();
        float closestDistance = -1.0f;
        int index = -1;

        for (int i=0; i<dataConsoles.Length; i++)
        {
            if (dataConsoles[i].GetData() > 0 &&
                (Vector3.Distance(transform.position, dataConsoles[i].transform.position) < closestDistance ||
                 closestDistance < 0))
               
            {
                closestDistance = Vector3.Distance(this.transform.position, dataConsoles[i].transform.position);
                index = i;
            }
        }

        if (index >= 0)
        {
            target = dataConsoles[index].gameObject.transform;
            collectionPoint = dataConsoles[index].GetCollectionPoint();
            foundTargetEvent.Invoke();
        }
        else
        {
            target = null;
            collectionPoint = null;
        }
    }
}
