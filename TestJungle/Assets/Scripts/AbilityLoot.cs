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
            randomIndex = Random.Range(0, player.GetComponent<PlayerManager>().unobtainableAbilities.Count);
            Debug.Log(randomIndex);
            AssignAbility();
        }
    }

    void AssignAbility()
    {
        AbstractAbility assignedAbility = player.GetComponent<PlayerManager>().unobtainableAbilities[randomIndex];
        
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
        else
        {
            Debug.Log("no more abilities to unlock");
        }
    }

    public int findAbilityIndex(AbstractAbility abilityNumber)
    {
        int index = -1;
        for (int i = 0; i < player.GetComponent<PlayerManager>().unobtainableAbilities.Count; i++)
        {
            if (player.GetComponent<PlayerManager>().unobtainableAbilities[i] == abilityNumber)
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
            player.GetComponent<PlayerManager>().unobtainableAbilities.RemoveAt(findAbilityIndex(abilityNumber));
        }
        else
        {
            Debug.Log("can't find ability in list");
        }
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
