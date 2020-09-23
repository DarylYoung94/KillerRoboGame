using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public List<AbstractWeapon> weapons;
    public List<AbstractAbilityCooldown> weaponHolders;
    public int currentWeaponIndex = 0;

    List<KeyCode> weaponKeys = new List<KeyCode>()
                                    { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3,
                                      KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6 };

    void Start()
    {
        CreateWeaponHolders();
        SetCurrentWeapon(currentWeaponIndex);
    }
    // Update is called once per frame
    void Update()
    {
        for(int i=0; i<weaponKeys.Count; i++ )
        {
            if (Input.GetKeyDown(weaponKeys[i]) && i<weaponHolders.Count)
            {
                SetCurrentWeapon(i);
            }
        }

    }

    void CreateWeaponHolders()
    {
        for (int i=0; i<weapons.Count; i++)
        {
            if (weapons[i].cdString == "OnHold")
            {
                weaponHolders.Add(this.transform.gameObject.AddComponent<OnHoldAbilityCooldown>());
            }
            if (weapons[i].cdString == "OnRelease")
            {
                weaponHolders.Add(this.transform.gameObject.AddComponent<OnReleaseAbilityCooldown>());
            }
            if (weapons[i].cdString == "QuickCast")
            {
                weaponHolders.Add(this.transform.gameObject.AddComponent<QuickCastAbilityCooldown>());
            }

            weaponHolders[i].Initialise(weapons[i], this.transform.gameObject, KeyCode.Mouse0, -1);
            weaponHolders[i].enabled = false;
        }

        weaponHolders[0].enabled = true;
    }

    public void AddWeapon(AbstractWeapon weapon)
    {
        bool alreadyExists = false;
        foreach (AbstractAbilityCooldown aac in weaponHolders)
        {
            if (aac.GetAbility() == weapon as AbstractAbility)
            {
                alreadyExists = true;
            }
        }

        if (!alreadyExists)
        {
            if (weapon.cdString == "OnHold")
            {
                weaponHolders.Add(this.transform.gameObject.AddComponent<OnHoldAbilityCooldown>());
            }
            if (weapon.cdString == "OnRelease")
            {
                weaponHolders.Add(this.transform.gameObject.AddComponent<OnReleaseAbilityCooldown>());
            }
            if (weapon.cdString == "QuickCast")
            {
                weaponHolders.Add(this.transform.gameObject.AddComponent<QuickCastAbilityCooldown>());
            }
        }
    }

    private void SetCurrentWeapon(int index)
    {
        weaponHolders[currentWeaponIndex].enabled = false;
        weaponHolders[index].enabled = true;

        currentWeaponIndex = index;
    }
}
