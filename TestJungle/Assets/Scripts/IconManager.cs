using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconManager : MonoBehaviour
{
    public GameObject WeaponIcon;
    public GameObject [] AbilityIcons;

    public static IconManager instance = null;

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

    void Start()
    {
        for (int i=0; i < AbilityIcons.Length; i++)
        {
            AbilityIcons[i].SetActive(false);
        }
    }

    public void SetWeaponIcon(Sprite iconSprite)
    {
        WeaponIcon.GetComponent<Image>().sprite = iconSprite;
    }

    public void SetAbilityIconByIndex(int index, Sprite iconSprite)
    {
        GameObject icon =  AbilityIcons[index] ;
        icon.SetActive(true);
        icon.GetComponent<Image>().sprite = iconSprite;
    }

    public int GetIconIndexByGameObject(GameObject icon)
    {
        return System.Array.FindIndex(AbilityIcons, o => o == icon);
    }

    public void SetIconFill(int iconIndex, float fillAmount)
    {
        if (iconIndex >= 0)
        {
            AbilityIcons[iconIndex].transform.GetChild(0).GetComponent<Image>().fillAmount = fillAmount; 
        }
    }
}
