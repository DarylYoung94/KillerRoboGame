 using UnityEngine;
 using System.Collections;
 
 public class CameraController : MonoBehaviour
 {
    public Transform target;

    public float targetHeight = 1.7f;
    public float distance = 5.0f;
    public float offsetFromWall = 0.1f;

    public float maxDistance = 20;
    public float minDistance = .6f;

    public float xSpeed = 200.0f;
    public float ySpeed = 200.0f;

    public int yMinLimit = -40;
    public int yMaxLimit = 80;

    public int zoomRate = 40;

    public float rotationDampening = 3.0f;
    public float zoomDampening = 5.0f;

    public LayerMask collisionLayers = -1;

    private float xDeg = 0.0f;
    private float yDeg = 0.0f;
    private float currentDistance;
    private float desiredDistance;
    private float correctedDistance;

    private bool sprinting = false;
    public float sprintZoom = 0.85f;

    [Header ("Highlighting")]
    public Material highlightMat;
    private GameObject highlightedGO;

    void Start ()
    {
        Vector3 angles = transform.eulerAngles;
        xDeg = angles.x;
        yDeg = angles.y;

        currentDistance = distance;
        desiredDistance = distance;
        correctedDistance = distance;
    }

    /**
    * Camera logic on LateUpdate to only update after all character movement logic has been handled.
    */
    void LateUpdate ()
    {
        Vector3 vTargetOffset;

        // Don't do anything if target is not defined
        if (!target)
            return;
 
        if (Input.GetMouseButton(1))
        {
            xDeg += Input.GetAxis ("Mouse X") * xSpeed * 0.02f;
            yDeg -= Input.GetAxis ("Mouse Y") * ySpeed * 0.02f;
        }
 
 
        // calculate the desired distance
        desiredDistance -= Input.GetAxis ("Mouse ScrollWheel") * Time.deltaTime * zoomRate * Mathf.Abs(desiredDistance);
        desiredDistance = Mathf.Clamp (desiredDistance, minDistance, maxDistance);
        correctedDistance = desiredDistance;

        yDeg = ClampAngle(yDeg, yMinLimit, yMaxLimit);
 
        // set camera rotation
        Quaternion rotation = Quaternion.Euler(yDeg, xDeg, 0);
 
        // calculate desired camera position
        vTargetOffset = new Vector3 (0, targetHeight, 0);
        Vector3 position = target.position - (rotation * Vector3.forward * desiredDistance) + vTargetOffset;
 
        // check for collision using the true target's desired registration point as set by user using height
        RaycastHit collisionHit;
        Vector3 trueTargetPosition = target.position + vTargetOffset;
 
        // if there was a collision, correct the camera position and calculate the corrected distance
        bool isCorrected = false;
        if (Physics.Linecast (trueTargetPosition, position, out collisionHit, collisionLayers.value))
        {
            // calculate the distance from the original estimated position to the collision location,
            // subtracting out a safety "offset" distance from the object we hit.  The offset will help
            // keep the camera from being right on top of the surface we hit, which usually shows up as
            // the surface geometry getting partially clipped by the camera's front clipping plane.
            correctedDistance = Vector3.Distance (trueTargetPosition, collisionHit.point) - offsetFromWall;
            isCorrected = true;
        }

        if (sprinting)
        {
            correctedDistance *= sprintZoom;
        }
 
        // For smoothing, lerp distance only if either distance wasn't corrected, or correctedDistance is more than currentDistance
        currentDistance = !isCorrected || correctedDistance > currentDistance ? 
                            Mathf.Lerp (currentDistance, correctedDistance, Time.deltaTime * zoomDampening) :
                            correctedDistance;
 
        // keep within legal limits
        currentDistance = Mathf.Clamp (currentDistance, minDistance, maxDistance);
 
        // recalculate position based on the new currentDistance
        position = target.position - (rotation * Vector3.forward * currentDistance) + vTargetOffset;
 
        transform.rotation = rotation;
        transform.position = position;

        DoHighlighting();
    }

    public void StartSprint()
    {
        sprinting = true;
    }

    public void StopSprint()
    {
        sprinting = false;
    }
 
    private static float ClampAngle (float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp (angle, min, max);
    }

    private void DoHighlighting()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Reset last highlighted object if no longer highlighted.
            if (highlightedGO != null && highlightedGO != hit.transform.gameObject)
            {
                highlightedGO.GetComponent<Renderer>().materials = 
                    new Material[] { highlightedGO.GetComponent<Renderer>().materials[0] };

                highlightedGO = null;
            }

            if (highlightedGO != hit.transform.gameObject && hit.transform.tag == "Interactable")
            {
                highlightedGO = hit.transform.gameObject;
                Material[] mats = highlightedGO.GetComponent<Renderer>().materials;
                highlightMat.SetColor("_Color", mats[0].GetColor("_Color"));
                mats = new Material[] { mats[0], highlightMat };

                highlightedGO.GetComponent<Renderer>().materials = mats;
            }

        }
    }
 }
