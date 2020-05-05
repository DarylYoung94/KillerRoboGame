using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunTriggerable : MonoBehaviour
{
    public float despawnTimer = 5f;

    public int pelletCount;
    public float verticalSpread;
    public float horizontalSpread;
    public float force;
    public GameObject pelletPrefab;
    public Transform barrelExit;

    private List<GameObject> pelletsGO;
    Vector3 aim;

    [SerializeField] private float nextFireTime = 0;
    [SerializeField] private float cooldownTime = 10f;

    public void Initialise()
    {
        pelletsGO = new List<GameObject>(pelletCount);
        for (int i=0; i < pelletCount; i++)
        {
            pelletsGO.Add(pelletPrefab);
        }
    }

    public void Shoot() 
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {         
            aim = hit.point;    
            nextFireTime = cooldownTime;
            Fire();
        }
    }

    void Fire()
    {
        for(int i=0; i < pelletsGO.Count; i++)
        {
            pelletsGO[i] = Instantiate(pelletPrefab, barrelExit.position, barrelExit.rotation);
            
            Rigidbody rb = pelletsGO[i].GetComponent<Rigidbody>();
            rb.transform.LookAt(aim);
            rb.AddForce(findSpreadDirection(rb.transform.forward) * force, ForceMode.Impulse);

            Destroy(pelletsGO[i],3);
        }
    }

    private Vector3 findSpreadDirection (Vector3 aim)
    {
        float spread1 = RandomGaussian.GetGaussian(0.0f, 2.0f, -horizontalSpread/2.0f, horizontalSpread/2.0f);
        float spread2 = RandomGaussian.GetGaussian(0.0f, 2.0f, -verticalSpread/2.0f, verticalSpread/2.0f);
        Vector3 retAim = Quaternion.Euler(spread1, spread2, spread1) * aim;
        retAim.Normalize();

        return retAim;
    }
}
