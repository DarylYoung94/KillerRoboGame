using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunAttack : MonoBehaviour
{
    public int pelletCount;
    public float verticalSpread;
    public float horizontalSpread;
    public float pelletFireVelocity;
    public GameObject pelletPrefab;
    public Transform BarrelExit;
    public List<GameObject> pelletsGO;
    Vector3 aim;

    [SerializeField] private float nextFireTime = 0;
    [SerializeField] private float cooldownTime = 10f;

    void Awake()
    {
        pelletsGO = new List<GameObject>(pelletCount);
        for (int i=0; i < pelletCount; i++)
        {
            pelletsGO.Add(pelletPrefab);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (nextFireTime > 0)
        {
            nextFireTime -= Time.deltaTime;
        }

        if (nextFireTime < 0)
        {
            nextFireTime = 0;
        }

        if (Input.GetMouseButton(0) && nextFireTime == 0)
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
    }

    void Fire()
    {
        for(int i=0; i < pelletsGO.Count; i++)
        {
            pelletsGO[i] = Instantiate(pelletPrefab, BarrelExit.position, BarrelExit.rotation);
            
            Rigidbody rb = pelletsGO[i].GetComponent<Rigidbody>();
            rb.transform.LookAt(aim);
            rb.AddForce(findSpreadDirection(rb.transform.forward) * pelletFireVelocity, ForceMode.Impulse);

            Destroy(pelletsGO[i],3);
        }
    }

    private Vector3 findSpreadDirection (Vector3 aim)
    {
        float spread1 = RandomGaussian.GetGaussian(0.0f, 2.0f, -horizontalSpread/2.0f, horizontalSpread/2.0f);
        float spread2 = RandomGaussian.GetGaussian(0.0f, 2.0f, -verticalSpread/2.0f, verticalSpread/2.0f);
        //float spread1 = getRandomGaussian(-horizontalSpread/2.0f, horizontalSpread/2.0f);
        //float spread2 = getRandomGaussian(-verticalSpread/2.0f, verticalSpread/2.0f);
        Vector3 retAim = Quaternion.Euler(spread1, spread2, spread1) * aim;
        retAim.Normalize();

        return retAim;
    }
}
