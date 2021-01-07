using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityLoot : MonoBehaviour
{
    public GameObject player;
    public AbstractAbility ability;
    
    private float timer = 0.0f;
    public float timeToDespawn = 10.0f;
    [SerializeField] private bool active = true;

    void Start()
    {
        player = GameManager.instance.player;
        timer = 0.0f;
    }
   
   void Update()
   {
        if (!active)
            timer += Time.deltaTime;

        if (timer > timeToDespawn)
        {
            DespawnPickup();
        }
   }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag ("Player"))
        {
            AssignAbility();
        }
    }

    void AssignAbility()
    {        
        if (ability != null)
        {
            LootManager.instance.AddObtainedAbility(ability);
            Destroy(this.gameObject);
        }
    }

    void DespawnPickup()
    {
        active = false;
        LootManager.instance.AddUnobtainedAbility(ability);
        Destroy(this.gameObject);
    }
}
