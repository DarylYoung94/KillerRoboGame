using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LlamaAnimation : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    Animator animator;
    public float moveSpeed = 3.5f;
    public float curveTolerance = 0.01f;
    public float longAutoRange = 10f;
    public float shortAutoRange = 5f;
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        target = GetComponent<EnemyAI>().target;


        if (animator != null)
        {
            float animCurve = animator.GetFloat("JumpCurve");
            if (animCurve > curveTolerance)
            {
                navMeshAgent.speed = moveSpeed;
            }
            else 
            {
                navMeshAgent.speed = 0f;
            }
        }
        //GameManager.instance.player
        float distanceFromPlayer = Vector3.Distance(target.transform.position,
                                                    this.transform.position);

        if (target == null)
        {
            animator.SetInteger("AutoRange", (int)AutoRange.OUT_OF_RANGE);
        }
        else if (distanceFromPlayer <= shortAutoRange)
        {
            animator.SetInteger("AutoRange", (int)AutoRange.SHORT);
        }
        else if (distanceFromPlayer <= longAutoRange)
        {
            animator.SetInteger("AutoRange", (int)AutoRange.LONG);
        }
        else
        {
            animator.SetInteger("AutoRange", (int)AutoRange.OUT_OF_RANGE);
        }
    }

    enum AutoRange {
        SHORT = 0,
        LONG = 1,
        OUT_OF_RANGE = 2
    }
}
