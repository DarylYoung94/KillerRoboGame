using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    public IconManager iconManager;
    GameObject currentWeaponIcon;

    public List<AbstractWeapon> weapons;
    public List<AbstractAbilityCooldown> weaponHolders;
    public List<int> activeWeaponList = new List<int> { 0, 1, 4 };
    
    // Should be between 0 and the number of active weapons.
    private int activeWeaponIndex = 0;

    void Start()
    {
        CreateWeaponHolders();
        SetCurrentWeapon(activeWeaponIndex);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(InputManager.Instance.GetWeaponSwapKey()))
        {
            int temp = (activeWeaponIndex + 1) % activeWeaponList.Count;
            SetCurrentWeapon(temp);
        }
    }

    public AbstractWeapon GetCurrentWeapon()
    {
        return weapons[activeWeaponList[activeWeaponIndex]];
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

            weaponHolders[i].Initialise(weapons[i], this.transform.gameObject);
            weaponHolders[i].SetKeyCode(KeyCode.Mouse0);
            weaponHolders[i].enabled = false;
        }
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

            weaponHolders[weaponHolders.Count-1].enabled = false;
        }
    }

    private void SetCurrentWeapon(int index)
    {
        weaponHolders[activeWeaponList[activeWeaponIndex]].enabled = false;
        weaponHolders[activeWeaponList[index]].enabled = true;

        activeWeaponIndex = index;
        iconManager.SetWeaponIcon(GetCurrentWeapon().abilityIcon);
    }

    public GameObject GetWeaponGameObject()
    {
        return this.transform.Find("Weapon").gameObject;
    }
}
