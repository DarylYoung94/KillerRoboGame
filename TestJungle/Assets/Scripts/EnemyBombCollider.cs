using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBombCollider : MonoBehaviour
{
    public GameObject explosionParticles;
    private GameObject player;
    public float radius = 20f;
    public float power = 10f;
    public float Damage = 10f;
    public float upForce = 2f;
    public Transform bombFaction;
    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.instance.player;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    {
            if(this.gameObject != null)
        {
            GameObject expParticles;
            expParticles = Instantiate (explosionParticles.gameObject, this.transform.position , Quaternion.identity );
            Vector3 explosionPosition = this.transform.position;

            Collider[] colliders = Physics.OverlapSphere(explosionPosition, radius);
                
            foreach (Collider hit in colliders)
            {
                PlayerManager playerHit = hit.transform.GetComponent<PlayerManager>();
                if (playerHit != null)
                {
                    playerHit.TakeDamage(Damage);
                    Destroy(this.gameObject);
                }
                Enemy enemyHit = hit.transform.GetComponent<Enemy>();
                if (enemyHit != null)
                {
                    enemyHit.TakeDamage(Damage, bombFaction.transform);
                }
                Rigidbody rb = hit.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(power, explosionPosition, radius, upForce, ForceMode.Impulse);
                    Destroy(this.gameObject);
                }
            }                
        }
    }
}
