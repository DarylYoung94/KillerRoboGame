using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarController : MonoBehaviour
{
    Transform BarHorizontal;
    Transform BarVertical;
    Transform BarCentre;

    private Vector3 startPosition;
    private Vector3 desiredPosition;

    public GameObject destructibles;
    public GameObject currentCube;
    private List<GameObject> cubes;

    public float timer = 2.0f;

    public float lerpTimeToMove = 0.5f;
    private float lerpTimeTaken = 0.0f;
    bool lerping = false;

    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform t_child in transform)
        {
            if (t_child.name == "BarHorizontal")
                BarHorizontal = t_child.transform;
            if (t_child.name == "BarVertical")
                BarVertical = t_child.transform;
            if (t_child.name == "BarCentre")
                BarCentre = t_child.transform;
        }
        if (destructibles)
            cubes = destructibles.GetComponent<CubeFiller>().cubes;
    }

    void Update()
    {
        if (BarHorizontal == null || BarVertical == null || BarCentre == null)
            return;
        
        if (currentCube == null && destructibles.transform.childCount > 0 && !lerping)
            UpdateLaser();
        
        if (lerping)
        {
            lerpTimeTaken += Time.deltaTime;
            BarCentre.transform.position = Vector3.Lerp(startPosition, desiredPosition, lerpTimeTaken/lerpTimeToMove);

            if (BarCentre.transform.position == desiredPosition)
            {
                lerpTimeTaken = 0.0f;
                lerping = false;
            }
        }
    
        CentreMovement();
    }

    private void CentreMovement()
    {
        BarHorizontal.localPosition = new Vector3 (BarHorizontal.localPosition.x,
                                                   BarHorizontal.localPosition.y,
                                                   BarCentre.localPosition.z);

        BarVertical.localPosition = new Vector3 (BarCentre.localPosition.x,
                                                 BarVertical.localPosition.y,
                                                 BarVertical.localPosition.z);
    }

    void UpdateLaser()
    {
        int lastIndex = cubes.Count - 1;
        currentCube = cubes[lastIndex];
        cubes.RemoveAt(lastIndex);
        
        startPosition = BarCentre.transform.position;
        desiredPosition = new Vector3 (currentCube.transform.position.x,
                                       BarCentre.transform.position.y,
                                       currentCube.transform.position.z);
        lerping = true;

        Destroy(currentCube, timer);
    }
}
