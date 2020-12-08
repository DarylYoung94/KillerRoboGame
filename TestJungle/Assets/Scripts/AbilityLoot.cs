using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AbilityLoot : MonoBehaviour
{
    public GameObject player;
    public IconManager iconManager;
    public AbstractAbility ability;
    
    private float timer = 0.0f;
    public float timeToDespawn = 10.0f;
    [SerializeField] private bool active = true;

    void Start()
    {
        player = GameManager.instance.player;
        iconManager = GameObject.Find("iconmanager").GetComponent<IconManager>();

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
            AbstractAbilityCooldown abilityCooldown = player.AddComponent(ability.GetCooldownType()) as AbstractAbilityCooldown;
            int iconIndex = iconManager.SetNextIcon(ability.abilityIcon);
            LootManager.instance.AddObtainedAbility(ability, abilityCooldown);

            abilityCooldown.Initialise(ability,
                                       player,
                                       iconIndex);

            // If a slot is free then assign it otherwise don't set key code and disable the ability holder
            if (player.GetComponent<InputManager>().IsKeyCodeAvailable())
            {
                abilityCooldown.SetKeyCode(player.GetComponent<InputManager>().GetNextKeyCode());
            }
            else
            {
                abilityCooldown.enabled = false;
            }

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
