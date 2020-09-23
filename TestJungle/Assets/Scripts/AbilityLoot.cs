using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AbilityLoot : MonoBehaviour
{
    public GameObject player;
    private int randomIndex;
    public IconManager iconManager;
    
    void Start()
    {
        player = GameManager.instance.player;
        iconManager = GameObject.Find("iconmanager").GetComponent<IconManager>();
    }
   

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag ("Player"))
        {
            randomIndex = Random.Range(0, player.GetComponent<PlayerManager>().unobtainedAbilities.Count);
            Debug.Log(randomIndex);
            AssignAbility();
        }
    }

    void AssignAbility()
    {
        AbstractAbility assignedAbility = player.GetComponent<PlayerManager>().unobtainedAbilities[randomIndex];
        
        if (assignedAbility != null)
        {
            AbstractAbilityCooldown abilityCooldown = player.AddComponent(assignedAbility.GetCooldownType()) as AbstractAbilityCooldown;
            int iconIndex = iconManager.SetNextIcon(assignedAbility.abilityIcon);
            
            abilityCooldown.Initialise(assignedAbility,
                                       player,
                                       player.GetComponent<InputManager>().GetNextKeyCode(),
                                       iconIndex);
            
            removeAbilityFromList(assignedAbility);

            Destroy();
        }
    }

    public int findAbilityIndex(AbstractAbility abilityNumber)
    {
        int index = -1;
        for (int i = 0; i < player.GetComponent<PlayerManager>().unobtainedAbilities.Count; i++)
        {
            if (player.GetComponent<PlayerManager>().unobtainedAbilities[i] == abilityNumber)
            {
                index = i;
                break;
            }
        }

        return index;
    }

   public void removeAbilityFromList(AbstractAbility abilityNumber)
    {
        if (findAbilityIndex(abilityNumber) != -1)
        {
            player.GetComponent<PlayerManager>().unobtainedAbilities.RemoveAt(findAbilityIndex(abilityNumber));
        }
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
