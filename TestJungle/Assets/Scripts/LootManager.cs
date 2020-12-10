using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour
{
    [SerializeField] private List<AbstractAbility> unobtainedAbilities;
    [SerializeField] private List<AbstractAbility> obtainedAbilities;
    [SerializeField] private List<AbstractAbilityCooldown> abilityCooldowns;
    [SerializeField] private int[] activeAbilities = new int[4] { -1, -1, -1, -1 };
    [SerializeField] private int[] inactiveAbilities = new int[8] { -1, -1, -1, -1, -1 , -1, -1, -1};

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

    void Update()
    {
        //UpdateIconFills();
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

    public void AddObtainedAbility(AbstractAbility ability)
    {
        AbstractAbilityCooldown aac = GameManager.instance.player.AddComponent(ability.GetCooldownType()) as AbstractAbilityCooldown;
        obtainedAbilities.Add(ability);
        abilityCooldowns.Add(aac);

        // Get next ability index
        int index = obtainedAbilities.Count - 1;
        IconManager.instance.SetAbilityIconByIndex(index, ability.abilityIcon);

        if (index < activeAbilities.Length)
        {
            activeAbilities[index] = index;
            aac.SetKeyCode(GameManager.instance.player.GetComponent<InputManager>().GetKeyCodeByInt(index));
        }
        else
        {
            inactiveAbilities[index - activeAbilities.Length] = index;
            aac.enabled = false;
        }

        aac.Initialise(ability, GameManager.instance.player);
    }

    public void SwapAbilities(int abilityA, int abilityB)
    {
        if (abilityA != abilityB)
        {
            AbilityGroupIndex a = new AbilityGroupIndex();
            AbilityGroupIndex b = new AbilityGroupIndex();

            for(int i=0; i<activeAbilities.Length; i++)
            {
                if (activeAbilities[i] == abilityA)
                {
                    a.Set(AbilityGroup.ACTIVE, i);
                }
                else if (activeAbilities[i] == abilityB)
                {
                    b.Set(AbilityGroup.ACTIVE, i);   
                }
            }

            for(int i=0; i<inactiveAbilities.Length; i++)
            {
                if (inactiveAbilities[i] == abilityA)
                {
                    a.Set(AbilityGroup.INACTIVE, i);
                }
                else if (inactiveAbilities[i] == abilityB)
                {
                    b.Set(AbilityGroup.INACTIVE, i);
                }
            }

            if (a.group == b.group)
            {
                if (a.group == AbilityGroup.ACTIVE)
                {
                    SwapKeyCodes(activeAbilities[a.index], activeAbilities[b.index]);
                    int temp = activeAbilities[a.index];
                    activeAbilities[a.index] = activeAbilities[b.index];
                    activeAbilities[b.index] = temp;
                }
                else if (a.group == AbilityGroup.INACTIVE)
                {
                    int temp = inactiveAbilities[a.index];
                    inactiveAbilities[a.index] = inactiveAbilities[b.index];
                    inactiveAbilities[b.index] = temp;
                }
            }
            else if (a.group == AbilityGroup.ACTIVE && b.group == AbilityGroup.INACTIVE)
            {
                int temp = inactiveAbilities[b.index];
                inactiveAbilities[b.index] = activeAbilities[a.index];
                abilityCooldowns[inactiveAbilities[b.index]].enabled = false;
                abilityCooldowns[inactiveAbilities[b.index]].SetKeyCode(KeyCode.None);

                activeAbilities[a.index] = temp;
                abilityCooldowns[activeAbilities[a.index]].enabled = true;
                abilityCooldowns[activeAbilities[a.index]].SetKeyCode(InputManager.Instance.GetKeyCodeByInt(a.index));
            }
            else if (a.group == AbilityGroup.INACTIVE && b.group == AbilityGroup.ACTIVE)
            {
                int temp = inactiveAbilities[a.index];
                inactiveAbilities[a.index] = activeAbilities[b.index];
                abilityCooldowns[inactiveAbilities[a.index]].enabled = false;
                abilityCooldowns[inactiveAbilities[a.index]].SetKeyCode(KeyCode.None);

                activeAbilities[b.index] = temp;
                abilityCooldowns[activeAbilities[b.index]].enabled = true;
                abilityCooldowns[activeAbilities[b.index]].SetKeyCode(InputManager.Instance.GetKeyCodeByInt(b.index));
            }
        }
    }

    // Takes a number between 0 and 3 corresponding to the 4 active abilities and the
    // index of the obtained ability to use in this slot
    public void SetAbilitySlot(int abilitySlot, int abilityIndex)
    {
        // Disable and cleanup current ability in the slot if it exists
        abilityCooldowns[activeAbilities[abilitySlot]].enabled = false;
        abilityCooldowns[activeAbilities[abilitySlot]].SetKeyCode(KeyCode.None);

        // Enable new ability and setup
        activeAbilities[abilitySlot] = abilityIndex;
        abilityCooldowns[activeAbilities[abilitySlot]].enabled = true;
        abilityCooldowns[activeAbilities[abilitySlot]].SetKeyCode(InputManager.Instance.GetKeyCodeByInt(abilitySlot));
    }

    public void SwapKeyCodes(int a, int b)
    {
        KeyCode temp = abilityCooldowns[a].GetKeyCode();
        abilityCooldowns[a].SetKeyCode(abilityCooldowns[b].GetKeyCode());
        abilityCooldowns[b].SetKeyCode(temp);
    }

    public void UpdateIconFills()
    {
        for (int i=0; i<activeAbilities.Length; i++)
        {
            if (activeAbilities[i] >= 0)
            {
                IconManager.instance.SetIconFill(i, abilityCooldowns[activeAbilities[i]].CoolDownProgress());
            }
        }
    }

    public enum AbilityGroup {
        ACTIVE,
        INACTIVE,
        NONE
    }

    public class AbilityGroupIndex
    {
        public int index;
        public AbilityGroup group;
        
        public AbilityGroupIndex()
        {
            group = AbilityGroup.NONE;
            index = -1;
        }
        public AbilityGroupIndex(AbilityGroup ag, int indexInGroup)
        {
            group = ag;
            index = indexInGroup;
        }

        public static void Swap(ref AbilityGroupIndex a, ref AbilityGroupIndex b)
        {
            AbilityGroupIndex temp = a;
            a = b;
            b = temp;
        }

        public void Set(AbilityGroup ag, int indexInGroup)
        {
            group = ag;
            index = indexInGroup;
        }
    };
}
