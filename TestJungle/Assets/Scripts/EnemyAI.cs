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

    public FactionType.Faction faction;
    public float closestEnemy;
    Transform bestTarget = null;
    public FactionType.Faction thisFaction;

    [SerializeField] UnityEvent foundEnemyEvent = new UnityEvent();

    void Start()
    {
        foundEnemyEvent.AddListener(FoundEnemy);

        target = GameManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        thisFaction = GetComponent<FactionType>().faction;
    }

    // Update is called once per frame
    void Update()
    {
        
        thisFaction = GetComponent<FactionType>().faction;
        Collider[] colliders = Physics.OverlapSphere(transform.position, lookRadius);
        closestEnemy = Mathf.Infinity;

        foreach(Collider hit in colliders)
        {
            if(hit != null && hit.GetComponent<Enemy>() || hit.GetComponent<PlayerManager>())
            {
                FactionType tempFactionType = hit.transform.GetComponent<FactionType>();

                if(tempFactionType )
                {
                    //faction = tempFactionType.faction;
                    if(thisFaction != hit.GetComponent<FactionType>().faction)
                    {
                        float distToEnemy = Vector3.Distance(transform.position, hit.transform.position);
                        if(distToEnemy < closestEnemy)
                        {
                            closestEnemy = distToEnemy;
                            bestTarget = hit.transform;
                        }

                        target = bestTarget;
                        //Debug.Log("bestTarget");
                        
                    }
                }
            }
        }

        
        float distance = target ? Vector3.Distance(target.position, transform.position) : Mathf.Infinity;
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
