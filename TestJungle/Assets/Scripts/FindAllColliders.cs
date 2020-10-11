using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindAllColliders : MonoBehaviour
{  
    public List<CapsuleCollider> capsules = new List<CapsuleCollider>();
    public float radius = 5.0f;

    void Update()
    {
        capsules.Clear();

        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, radius);
        foreach (var hitCollider in hitColliders)
        {
            CapsuleCollider cc = hitCollider as CapsuleCollider;

            if (cc != null)
            {
                capsules.Add(cc);
            }
        }

        this.transform.GetComponent<Cloth>().capsuleColliders = capsules.ToArray();
    }
}
