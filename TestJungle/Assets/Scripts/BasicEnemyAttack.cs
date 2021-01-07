using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyAttack : MonoBehaviour
{
    Transform target;
    public float attackTime = 1f;
    public float nextAttackTime = 1.0f;
    public float attackSpeed = 1f;
    public float attackDamage = 10f;
    public float attackRange = 5f;
    public GameObject attackParticles;
    public bool allowAttack = true;
    


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AttackSpeed(attackSpeed));

    }

    // Update is called once per frame
    void Update()
    {
        SetTarget();
        float distance = Mathf.Infinity;
        if (target)
            distance = Vector3.Distance(target.position, transform.position);

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

    public void SetTarget()
    {
        target = transform.GetComponent<EnemyAI>().target;
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
                if(hit.transform == target)
                {
                    Enemy enemyHit = hit.transform.GetComponent<Enemy>(); 
                    if(enemyHit !=null)
                    {
                        enemyHit.TakeDamage(attackDamage, this.transform);
                    }
                }
                    
                PlayerManager playerHit = hit.transform.GetComponent<PlayerManager>();
                if (playerHit!= null)
                {
                    playerHit.TakeDamage(attackDamage);
                }
                
            }
        }
    }

    IEnumerator AttackSpeed(float attackSpeed)
    {
        yield return new WaitForSeconds(attackSpeed);
        Attack();
    }
}
