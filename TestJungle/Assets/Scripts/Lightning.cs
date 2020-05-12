﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    private LineRenderer lineRenderer;

    private int noSegments = 12;
    private Color colour = Color.white;
    private float opacityTimeScale = 20f;

    private float posRange = 0.25f;
    private float radius = 1f;
    private Vector2 midPoint;
    [SerializeField] private Vector3[] linePoints;

    private float aimDist = 0.0f;
    private float chainRange = 4.0f;
    public bool applyChains = false;
    public GameObject chainLightningPrefab;
    private GameObject prevEnemy = null;
    float damage = 0.5f;


    // TODO
    // - Tick rate?
    // - Stop line at the first enemy hit

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = noSegments;
        midPoint = new Vector2(Random.Range(-radius,radius),
                               Random.Range(-radius,radius));

        for (int i=1; i<noSegments-1; ++i)
        {
            float z  = ((float)i) * aimDist / (float)(noSegments-1);
            float x  = (-2.0f*midPoint.x)/(aimDist*aimDist) * (z*z - aimDist*z);
            float y  = (-2.0f*midPoint.y)/(aimDist*aimDist) * (z*z - aimDist*z);

            lineRenderer.SetPosition(i, new Vector3(x + Random.Range(-posRange,posRange),
                                                    y + Random.Range(-posRange,posRange),
                                                    z));
        }

        lineRenderer.SetPosition(0, new Vector3(0,0,0));
        lineRenderer.SetPosition(noSegments-1, new Vector3 (0, 0, aimDist));


        GetLinePointsInWorldSpace();
    }

    // Update is called once per frame
    void Update()
    {
        ApplyCollisions();

        colour.a -= opacityTimeScale*Time.deltaTime;
        lineRenderer.startColor = colour;
        lineRenderer.endColor = colour;

        if(colour.a <= 0f)
            Destroy(this.gameObject);
        
    }

    private void ApplyCollisions()
    {
        for (int i=0; i<linePoints.Length-1; i++)
        {
            RaycastHit hit;
            int layerMask = 1 << 10;
            
            if(Physics.Linecast(linePoints[i],
                                linePoints[i+1],
                                out hit,
                                layerMask))
            {
                Enemy enemyHit = hit.transform.GetComponent<Enemy>();
                if (enemyHit != null)
                {
                    enemyHit.TakeDamage(damage);
                }

                if(applyChains)
                {
                    ChainLightning(hit.transform.gameObject);
                }

                SetLineRendererAfterCollision(i+1, hit.transform.position);
                break;
            }
        }
    }

    private void GetLinePointsInWorldSpace()
    {
        linePoints = new Vector3[noSegments];
        lineRenderer.GetPositions(linePoints);
        
        for(int i=0; i < noSegments; i++)
        {
            linePoints[i] = transform.TransformPoint(linePoints[i]);
        }
    }

    private void SetLineRendererAfterCollision(int collisionIndex, Vector3 point)
    {
        Vector3[] localPoints = linePoints;
        for (int i=0; i < linePoints.Length; i++)
        {
            if (i >= collisionIndex)
            {
                linePoints[i] = point;
            }
            
            localPoints[i] = this.transform.InverseTransformPoint(linePoints[i]);
        }

        lineRenderer.SetPositions(localPoints);
        GetLinePointsInWorldSpace();
    }

    private void ChainLightning(GameObject parent)
    {
        GameObject targetGO = FindClosestEnemy(parent);
        if (targetGO != null && targetGO != parent)
        {
            //Debug.Log ("Found closest enemy " + targetGO);
            //Debug.DrawLine(this.transform.position, targetGO.transform.position, Color.black, 1.0f);
            float aimDistance  = Vector3.Distance(this.transform.position, targetGO.transform.position);

            GameObject lightning = Instantiate(chainLightningPrefab, parent.transform.position, Quaternion.identity);
            lightning.transform.SetParent(parent.transform);
            lightning.transform.LookAt(targetGO.transform.position);
            lightning.GetComponent<Lightning>()
                     .Setup(aimDistance, applyChains, chainRange, chainLightningPrefab, damage);
            lightning.GetComponent<Lightning>()
                     .SetPrevEnemy(parent);
        }
    }


    private GameObject FindClosestEnemy(GameObject go)
    {
        Vector3 position = go.transform.position;
        GameObject target = null;

        Collider[] colliders = Physics.OverlapSphere(position, chainRange);

        float closestEnemy = Mathf.Infinity;

        foreach (Collider hit in colliders)
        {
            float distToEnemy = Vector3.Distance(position, hit.transform.position);
            Enemy enemyHit = hit.transform.GetComponent<Enemy>();

            if (enemyHit != null &&
                hit.transform.gameObject != go &&
                hit.transform.gameObject != prevEnemy &&
                distToEnemy < closestEnemy)
            {
                closestEnemy = distToEnemy;
                target = hit.transform.gameObject;
            }
        }

        return target;
    }

    public void Setup(float aimDist, bool applyChains, float chainRange, GameObject chainPrefab, float damage)
    {
        this.aimDist = aimDist ;
        this.chainRange = chainRange;
        this.chainLightningPrefab = chainPrefab;
        this.applyChains = applyChains;
        this.damage = damage;
    }

    public void SetPrevEnemy(GameObject enemy){ prevEnemy = enemy; }
}