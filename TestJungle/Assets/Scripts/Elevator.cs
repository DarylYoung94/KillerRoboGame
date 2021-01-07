using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Elevator : MonoBehaviour
{
    [SerializeField] UnityEvent arriveEvent = new UnityEvent();
    [SerializeField] UnityEvent startMovingEvent = new UnityEvent();

    [SerializeField] private Vector3 desiredPosition;
    [SerializeField] private List<Vector3>  positionQueue = new List<Vector3>();
    [SerializeField] private float lerpSpeed = 20.0f;
    [SerializeField] private float maxSpeed = 2.0f;
    public float waitTime = 8.0f;
    [SerializeField] private float timer = -1.0f;
    private bool isStationary = true;

    [Header ("Doors")]
    public Doors elevatorDoors;
    public List<GameObject> doors = new List<GameObject>(); 
    public float doorOffsetForward = 1.704f;
    public float doorOffsetHorizontal = -2.329f;

    [Header ("Buttons")]
    public List<GameObject> buttons;

    void Start()
    {
        desiredPosition = this.transform.position;

        arriveEvent.AddListener(OnArrival);
        startMovingEvent.AddListener(OnStartMoving);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(InputManager.Instance.GetInteractKey()))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                int index = buttons.IndexOf(hit.transform.gameObject);

                if (index != -1)
                {
                    AddDestinationByLevel(index);
                }
            }
        }

        if (Vector3.Distance(this.transform.position, desiredPosition) > 0.001f)
        {
            Vector3 directionVector = (desiredPosition - this.transform.position).normalized;
            float distance = Mathf.Lerp(0, (desiredPosition - this.transform.position).magnitude, lerpSpeed * Time.deltaTime);
            distance = Mathf.Clamp(distance, 0, maxSpeed * Time.deltaTime);

            this.transform.position += distance * directionVector;
        }
        else if (!isStationary)
        {
            isStationary = true;
            arriveEvent.Invoke();
        }
        else if (positionQueue.Count > 0)
        {
            timer += Time.deltaTime;
        }

        if (timer > waitTime && positionQueue.Count > 0)
        {
            isStationary = false;
            timer = 0.0f;
            startMovingEvent.Invoke();
        }
    }

    void OnArrival()
    {
        elevatorDoors.OpenDoor();

        GameObject go = FindNearestDoor();
        if (go)
        {
            go.GetComponent<Doors>().OpenDoor();
        }

       // Debug.Log("Arrived at: " + this.transform.position.ToString());
    }

    void OnStartMoving()
    {
        elevatorDoors.CloseDoor();

        GameObject go = FindNearestDoor();
        if (go)
        {
            go.GetComponent<Doors>().CloseDoor();
        }

        desiredPosition = positionQueue[0];
        positionQueue.RemoveAt(0); 
       // Debug.Log("Moving to: " + desiredPosition.ToString());
    }

    void AddDestination(Vector3 destination) { positionQueue.Add(destination); }

    private GameObject FindNearestDoor()
    {
        GameObject go = null;
        int currentIndex = -1;
        float closestDistance = 10.0f;

        for (int i=0; i<doors.Count; i++)
        {
            if ((doors[i].transform.position-this.transform.position).magnitude < closestDistance)
            {
                closestDistance = (doors[i].transform.position-this.transform.position).magnitude;
                currentIndex = i;
            }
        }

        if (currentIndex > -1)
        {
            go = doors[currentIndex];
        }

        return go;
    }

    public void AddDestinationByLevel(int index)
    {
        Vector3 transformDirectionForward = transform.TransformDirection(Vector3.forward);
        Vector3 transformDirectionRight = transform.TransformDirection(Vector3.right);
        positionQueue.Add(doors[index].transform.position +
                          doorOffsetHorizontal * transformDirectionForward +
                          doorOffsetForward * transformDirectionRight);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.transform.parent = null;
        }
    }
}
