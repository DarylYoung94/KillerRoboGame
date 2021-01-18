using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    public IconManager iconManager;
    GameObject currentWeaponIcon;
    [SerializeField] Transform weaponPrefabHolder;
    [SerializeField] Transform secondHand;

    public List<GameObject> weaponPrefabs;
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

    public void DisableWeapon()
    {
        weaponHolders[activeWeaponList[activeWeaponIndex]].enabled = false;
    }

    public void EnableWeapon()
    {
        weaponHolders[activeWeaponList[activeWeaponIndex]].enabled = true;
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

            CreateWeaponGameObject(weapons[i]);

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

            CreateWeaponGameObject(weapon);

            weaponHolders[weaponHolders.Count-1].Initialise(weapon, this.transform.gameObject);
            weaponHolders[weaponHolders.Count-1].SetKeyCode(KeyCode.Mouse0);
            weaponHolders[weaponHolders.Count-1].enabled = false;
        }
    }

    private void SetCurrentWeapon(int index)
    {
        weaponHolders[activeWeaponList[activeWeaponIndex]].enabled = false;
        weaponHolders[activeWeaponList[index]].enabled = true;


        activeWeaponIndex = index;
        iconManager.SetWeaponIcon(GetCurrentWeapon().abilityIcon);
        SetWeaponGameObject(activeWeaponIndex);
    }

    private void SetWeaponGameObject(int index)
    {
        if (weaponPrefabHolder)
        {
            for (int i = 0; i < weaponPrefabHolder.transform.childCount; i++)
            {
                weaponPrefabHolder.GetChild(i).gameObject.SetActive(false);
            }
        }

        weaponPrefabs[activeWeaponList[index]].SetActive(true);
    }

    private void CreateWeaponGameObject(AbstractWeapon weapon)
    {
        if (weapon.weaponPrefab && weaponPrefabHolder)
        {
            GameObject newWeaponPrefab = Instantiate(weapon.weaponPrefab,
                                                     weaponPrefabHolder.transform.position,
                                                     Quaternion.identity,
                                                     weaponPrefabHolder.transform);
            
            newWeaponPrefab.transform.rotation = weaponPrefabHolder.transform.rotation;
            weaponPrefabs.Add(newWeaponPrefab);

            foreach (Transform t in newWeaponPrefab.GetComponentsInChildren<Transform>())
            {
                if (t.tag == "Firepoint")
                {
                    weapon.barrelExit = t;
                    break;
                }
            }
        }
    }

    public GameObject GetWeaponGameObject()
    {
        return weaponPrefabHolder.gameObject;
    }
}
