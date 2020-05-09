using System.Collections;
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

    private Vector3 targetVector = new Vector3 (0,0,8);

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = noSegments;
        midPoint = new Vector2(Random.Range(-radius,radius),
                               Random.Range(-radius,radius));

        for (int i=1; i<noSegments-1; ++i)
        {
            float z  = ((float)i) * targetVector.z / (float)(noSegments -1);
            float x  = (-2.0f*midPoint.x)/(targetVector.z*targetVector.z) * (z*z - targetVector.z*z);
            float y  = (-2.0f*midPoint.y)/(targetVector.z*targetVector.z) * (z*z - targetVector.z*z);

            lineRenderer.SetPosition(i, new Vector3(x + Random.Range(-posRange,posRange),
                                                    y + Random.Range(-posRange,posRange),
                                                    z));
        }

        lineRenderer.SetPosition(0, new Vector3(0f,0f,0f));
        lineRenderer.SetPosition(noSegments-1, targetVector);
        
    }

    // Update is called once per frame
    void Update()
    {
        colour.a -= opacityTimeScale*Time.deltaTime;
        lineRenderer.startColor = colour;
        lineRenderer.endColor = colour;

        if(colour.a <= 0f)
            Destroy(this.gameObject);
        
    }

    public void SetTarget(Vector3 target) 
    {
        targetVector = target;
    }

    /*private Vector3 FindClosestEnemy()
    {
        Vector3 totemPosition = totemPrefab.transform.position;
        Collider[] colliders = Physics.OverlapSphere(totemPosition, totemRange);

        Transform closestEnemyTransform;
        float closestDistEnemy = Mathf.Infinity;

        foreach (Collider hit in colliders)
        {

            float distToEnemy = Vector3.Distance(hit.transform.position, totemPosition);
            Enemy enemyHit = hit.transform.GetComponent<Enemy>();


            if (enemyHit != null && distToEnemy < closestDistEnemy)
            {
                closestDistEnemy = distToEnemy;
                closestEnemyTransform = enemyHit.transform;
            }
        }

        return closestEnemyTransform;
    }*/
}
