using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketCollider : MonoBehaviour
{
    public GameObject explosionParticles;
    public Rigidbody rocketPrefab;
    private GameObject player;
    
    public float time = 3f;
    public float radius = 10f;
    public float power = 10f;
    public float upForce = 2f;
    public float Damage = 10f;
    
    private void Start() 
    {
         player = GameManager.instance.player;
    }

    void OnCollisionEnter(Collision collision) 
    {
        RocketAbilityTriggerable rocket = player.GetComponent<RocketAbilityTriggerable>();
        rocket.Detonate(this.gameObject);
        Destroy(this.gameObject);
    }    
}

