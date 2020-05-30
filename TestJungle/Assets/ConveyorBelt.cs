using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public float speed;

    private void OnCollisionStay(Collision other)
    {
        other.transform.position += Vector3.left * speed * Time.deltaTime;
    }
}
