using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour
{
    [SerializeField] private List<AbstractAbility> unobtainedAbilities;
    [SerializeField] private List<AbstractAbility> obtainedAbilities;
    [SerializeField] private List<AbstractAbilityCooldown> abilityCooldowns;
    public static LootManager instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public int NumberAbilitiesLeft() { return unobtainedAbilities.Count; }

    public void SpawnAbilityPickup(Vector3 position)
    {
        AbstractAbility ability = LootManager.instance.GetNewAbility();
        if (ability)
        {
            GameObject pickup = Instantiate(ability.pickupPrefab,
                                            position,
                                            Quaternion.identity);
            pickup.GetComponent<AbilityLoot>().ability = ability;
        }
    }

    public AbstractAbility GetNewAbility()
    {
        AbstractAbility returnedAbility = null;

        if (unobtainedAbilities.Count > 0)
        {
            int randomIndex = Random.Range(0, unobtainedAbilities.Count);
            returnedAbility = unobtainedAbilities[randomIndex];
            unobtainedAbilities.Remove(returnedAbility);
        }

        return returnedAbility;
    }

    public void AddUnobtainedAbility(AbstractAbility ability)
    {
        unobtainedAbilities.Add(ability);
    }

    public void AddObtainedAbility(AbstractAbility ability, AbstractAbilityCooldown aac)
    {
        obtainedAbilities.Add(ability);
        abilityCooldowns.Add(aac);
    }
}
