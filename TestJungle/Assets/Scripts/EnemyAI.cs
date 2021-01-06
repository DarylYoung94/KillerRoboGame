using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyAI : MonoBehaviour
{
    public float lookRadius = 10f;
    public Transform target;
    NavMeshAgent agent;
    public bool moving = false;

    [SerializeField] UnityEvent foundEnemyEvent = new UnityEvent();

    void Start()
    {
        foundEnemyEvent.AddListener(FoundEnemy);

        target = GameManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            foundEnemyEvent.Invoke(); 
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    public void FoundEnemy()
    {
        agent.SetDestination(target.position);
    }
}
