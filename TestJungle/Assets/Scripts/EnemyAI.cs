using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyAI : MonoBehaviour
{
    public float attackTime = 1f;
    public float nextAttackTime = 1.0f;
    public float attackSpeed = 1f;
    public float attackDamage = 10f;
    public float lookRadius = 10f;
    public float attackRange = 5f;
    public Transform target;
    NavMeshAgent agent;
    public GameObject attackParticles;
    public bool allowAttack = true;
    public bool moving =false;
    // Use this for initialization

    private float currentSpeed = 0.0f;
    private float maxSpeed = 5.0f;

    [SerializeField] UnityEvent foundEnemyEvent = new UnityEvent();

    void Start()
    {
        foundEnemyEvent.AddListener(FoundEnemy);

        StartCoroutine(AttackSpeed(attackSpeed));
        target = GameManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    IEnumerator AttackSpeed(float attackSpeed)
    {
        yield return new WaitForSeconds(attackSpeed);
        Attack();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            foundEnemyEvent.Invoke(); 
           
        }
        if (nextAttackTime > 0)
        {
            nextAttackTime -= Time.deltaTime;
        }

        if (nextAttackTime < 0)
        {
            nextAttackTime = 0;
        }

        if (distance <= attackRange && nextAttackTime == 0 && allowAttack == true) 
        {
            
            Attack();
            nextAttackTime = attackTime;
            

            if (attackParticles)
            {
                Instantiate(attackParticles, transform.position, Quaternion.identity);
            }

        }
        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
    
    public void Attack()
    {
        if (target != null)
        {
            if (attackParticles)
            {
                Instantiate(attackParticles, transform.position, Quaternion.identity);
            }

            Vector3 attackPosition = transform.position;
            Collider[] collider = Physics.OverlapSphere(attackPosition, attackRange);

            foreach (Collider hit in collider)
            {
                
                PlayerManager playerHit = hit.transform.GetComponent<PlayerManager>();
                if (playerHit!= null)
                {
                    playerHit.TakeDamage(attackDamage);
                }
            }
        }
    }

    public void FoundEnemy()
    {
        agent.SetDestination(target.position);
        
    }
}
