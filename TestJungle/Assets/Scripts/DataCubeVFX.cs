using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCubeVFX : MonoBehaviour
{
    [SerializeField] float speed = 10.0f;
    [SerializeField] float rotateSpeed = 10.0f;

    [SerializeField] Transform target = null;
    public void SetTarget(Transform target) { this.target = target; }

    private void Update()
    {
        if (Vector3.Distance(target.position, this.transform.position) < 0.1f)
        {
            Destroy(this.gameObject);
        }

        this.transform.position += (target.position - this.transform.position).normalized * speed * Time.deltaTime;
        this.transform.Rotate(Vector3.left, rotateSpeed * Time.deltaTime);
    }
}
