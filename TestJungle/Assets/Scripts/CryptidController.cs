using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;

public class CryptidController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float speed;
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;
    [SerializeField] Transform target;
    [SerializeField] float targetRange = 100.0f;
    [SerializeField] float attackRange = 10.0f;
    NavMeshAgent agent;


    [Header("Animation")]
    [SerializeField] private Animator animationController;

    [Header("VFX")]
    [SerializeField] private GameObject vfxPrefab;
    [SerializeField] private GameObject vfx;
    [SerializeField] private Transform vfxSpawnPoint;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        ResetAllAnimations();
    }

    void Update()
    {
        FindTargetWithinRange();
        agent.SetDestination(target.position);
        transform.LookAt(target);

        if (animationController.GetBool("Attacking") == true)
        {
            vfx.GetComponent<VisualEffect>().SetVector3("AttractiveTargetPosition", target.position);
        }
        else
        {
            Destroy(vfx);
        }
    }

    public void SetTarget()
    {
        target = transform.GetComponent<EnemyAI>().target;
    }

    public void FindTargetWithinRange()
    {
        if (Vector3.Distance(this.transform.position, target.transform.position) < targetRange)
        {
            if (Vector3.Distance(this.transform.position, target.transform.position) < attackRange)
            {
                if (!animationController.GetBool("Attacking"))
                {
                    SetAttacking();
                }
            }
            else
            {
                SetRunning();
            }
        }
        else
        {
            target = null;
            SetWalking();
        }
    }

    // Animation Methods
    public void SetWalking()
    {
        // Reset
        animationController.SetBool("Running", false);
        animationController.SetBool("Attacking", false);  
        // Set Walking
        animationController.SetBool("Walk", true);

        agent.speed = walkSpeed;
    }

    public void SetRunning()
    {
        // Reset
        animationController.SetBool("Walking", false);
        animationController.SetBool("Attacking", false);

        // Setting Running
        animationController.SetBool("Running", true);

        agent.speed = runSpeed;
    }

    public void SetAttacking()
    {
        // Reset
        animationController.SetBool("Walk", false);
        animationController.SetBool("Running", false);

        // Setting Attacking
        animationController.SetBool("Attacking", true);

        agent.speed = 0.0f;

        if (vfx == null)
        {
            vfx = Instantiate(vfxPrefab, vfxSpawnPoint.position, Quaternion.identity);
        }
    }

    public void ResetAllAnimations()
    {
        animationController.SetBool("Walk", false);
        animationController.SetBool("Running", false);
        animationController.SetBool("Attacking", false);  
    }
}
