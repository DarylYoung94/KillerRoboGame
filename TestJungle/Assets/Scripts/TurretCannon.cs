using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretCannon : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform firepoint;
    [SerializeField] private float range = 10.0f;
    [SerializeField] private float chargeTime = 3.0f;
    [SerializeField] private float cooldownTime = 10.0f;
    private float timer = 0.0f;
    [SerializeField] float widthFactor = 1.5f;

    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] GameObject laserHitPrefab;
    [SerializeField] GameObject laserProjPrefab;
    [SerializeField] float projectileSpeed = 10.0f;
    private GameObject vfx;
    private bool firing = false;

    void Awake()
    {
        timer = 0.0f;
        lineRenderer = firepoint.GetComponentInChildren<LineRenderer>();
        ResetLineRenderer();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        lineRenderer.SetPosition(0, firepoint.transform.position);

        if (target && !firing)
        {
            RaycastHit hit;
            if (Physics.Raycast(firepoint.position, firepoint.transform.TransformDirection(Vector3.forward), out hit))
            {
                if (hit.distance < range && timer < 0.0f)
                {
                    StartCoroutine(AimAndFire());
                }
            }
        }
    }

    public void SetTarget()
    {
        Transform tempTarget = transform.GetComponent<EnemyAI>().target;

        if (tempTarget)
            target = tempTarget;
    }

    private IEnumerator AimAndFire()
    {
        this.transform.GetComponent<QuadrupedController_Full>().StopRootMotion();
        firing = true;
        lineRenderer.enabled = true;

        float timeElapsed = 0;
        while (timeElapsed < chargeTime)
        {
            RaycastHit hit;
            if (Physics.Raycast(firepoint.transform.position, firepoint.transform.TransformDirection(Vector3.forward), out hit))
            {
                if (hit.collider)
                {
                    lineRenderer.SetPosition(1, hit.point);
                    if (vfx == null && laserHitPrefab != null)
                    {
                        vfx = Instantiate(laserHitPrefab, hit.point, Quaternion.identity, this.transform);
                    }
                    else if (vfx != null)
                    {
                        vfx.transform.position = hit.point;
                    }
                }
            }

            lineRenderer.widthMultiplier = 1.0f + (timeElapsed/chargeTime) * widthFactor;
            timeElapsed += Time.deltaTime;   
            yield return null;
        }

        if (laserProjPrefab)
        {
            GameObject laserProjectile = Instantiate(laserProjPrefab, firepoint.position, Quaternion.identity) as GameObject;
            laserProjectile.transform.LookAt(lineRenderer.GetPosition(1));
            laserProjectile.GetComponent<Rigidbody>().AddForce(laserProjectile.transform.forward * projectileSpeed, ForceMode.Impulse);
        }
        
        ResetLineRenderer();
        firing = false;
        timer = cooldownTime;
        this.transform.GetComponent<QuadrupedController_Full>().StartRootMotion();
    }

    void ResetLineRenderer()
    {
        lineRenderer.SetPosition(0, firepoint.transform.position);
        lineRenderer.SetPosition(1, firepoint.transform.position);
        lineRenderer.enabled = false;
        lineRenderer.widthMultiplier = 1.0f;
    }
}
