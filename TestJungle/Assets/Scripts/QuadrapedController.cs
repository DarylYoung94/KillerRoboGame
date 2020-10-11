using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadrapedController : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Transform headBone;
    [SerializeField] float headMaxTurnAngle;
    [SerializeField] float headTrackingSpeed;

    // Update is called once per frame
    void LateUpdate()
    {
        Quaternion currentLocalRotation = headBone.localRotation;
        headBone.localRotation = Quaternion.identity;

        Vector3 targetWorldLookDir = target.position - headBone.position;
        Vector3 targetLocalLookDir = headBone.InverseTransformDirection(targetWorldLookDir);

        targetLocalLookDir = Vector3.RotateTowards(Vector3.forward,
                                                   targetLocalLookDir,
                                                   Mathf.Deg2Rad * headMaxTurnAngle,
                                                   0);
        
        Quaternion targetLocalRotation = Quaternion.LookRotation(targetLocalLookDir, Vector3.up);

        // Apply smoothing
        headBone.localRotation = Quaternion.Slerp(currentLocalRotation,targetLocalRotation, 
                                                  1 - Mathf.Exp(-headTrackingSpeed * Time.deltaTime)
        );
    }
}
