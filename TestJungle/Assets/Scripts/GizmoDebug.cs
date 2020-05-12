using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoDebug : MonoBehaviour
{
    public Color colour = Color.red;
    void OnDrawGizmosSelected()
    {
        // Draws a 5 unit long red line in front of the object
        Gizmos.color = colour;
        Vector3 direction = transform.TransformDirection(Vector3.forward) * 5;
        Gizmos.DrawRay(transform.position, direction);
    }
}
