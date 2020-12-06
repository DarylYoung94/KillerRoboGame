using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconManager : MonoBehaviour
{
  
    public GameObject [] AbilityIcons;
    public int abilityNumber = 0;
    public GameObject player;

    void Start()
    {
        player = GameManager.instance.player;

        AbilityIcons[0].SetActive(false);
        AbilityIcons[1].SetActive(false);
        AbilityIcons[2].SetActive(false);
        AbilityIcons[3].SetActive(false);
    }

    public int SetNextIcon(Sprite iconSprite)
    {
        if(abilityNumber<=3)
        {
            GameObject icon =  AbilityIcons[abilityNumber] ;
            GameObject iconChild = icon.transform.GetChild(1).gameObject;
            GameObject iconBG = icon.transform.GetChild(0).gameObject;
            icon.SetActive(true);
            iconChild.SetActive(true);
            iconBG.SetActive(true);
            iconChild.GetComponent<Image>().sprite = iconSprite;
        }

        abilityNumber++;
        return abilityNumber-1;
    }

    public void SetIconFill(int iconIndex,float fillAmount)
    {
        if (iconIndex>=0)
        {
            AbilityIcons[iconIndex].transform.GetChild(0).GetComponent<Image>().fillAmount = fillAmount; 
        }
    }
}
