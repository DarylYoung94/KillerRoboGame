using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VendingMachine : MonoBehaviour
{
    public GameObject player;
    public List<GameObject> factions;
    public List<GameObject> buyInteractable;
    public GameObject sellInteractable;
    public GameObject vendUI;
    public GameObject exit;
    private float interactRange =5f;
    public GameObject factionSpawn;
    public FactionType.Faction faction;
    public List<GameObject> vendList;
    public int facNum = 0;
    public List<Transform> spawnPoints;
    public List<TextMeshProUGUI> costText;

    private List<int> uniqueNumbers;
    private List<int> finishedList;


    public List<GameObject> abilityPickups;
    public List<Sprite> abilitySprites;
    public List<Sprite> currentSprites;
    public List<GameObject> facAbilitySprite; 

    public List<GameObject> selectedAbilityPickups;

    void Start() 
    {
        player = GameManager.instance.player;   
        uniqueNumbers = new List<int>();
        finishedList = new List<int>();
        vendUI.SetActive(false);
        SelectAbilities();
    }

    public void SelectAbilities()
    {
        for (int i = 0; i < abilityPickups.Count; i++)
        {
            uniqueNumbers.Add(i);
        } 
        for(int i = 0; i< abilityPickups.Count; i ++)
        {
            int ranNum = uniqueNumbers[Random.Range(0,uniqueNumbers.Count)];
            finishedList.Add(ranNum);
            uniqueNumbers.Remove(ranNum);
        }
        for(int i = 0; i< 3; i ++)
        {
            selectedAbilityPickups.Add(abilityPickups[finishedList[i]]);
            currentSprites.Add(abilitySprites[finishedList[i]]);
        } 
    }

    void Update()
    {
        if (Input.GetKeyDown(InputManager.Instance.GetInteractKey()))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                float distance = Vector3.Distance(hit.transform.position, player.transform.position);
                
                if(distance <=interactRange && hit.transform.gameObject == buyInteractable[0])
                {
                    player.GetComponent<Basicmovement>().enabled = false;
                    player.GetComponent<WeaponManager>().DisableWeapon();
                    facNum = 0;
                    vendUI.SetActive(true);
                    ShowSprites(facNum);
                }
                if(distance <=interactRange && hit.transform.gameObject == buyInteractable[1])
                {
                    player.GetComponent<Basicmovement>().enabled = false;
                    player.GetComponent<WeaponManager>().DisableWeapon();
                    facNum = 1; 
                    vendUI.SetActive(true);
                    ShowSprites(facNum);    
                }
                if(distance <=interactRange && hit.transform.gameObject == buyInteractable[2])
                {
                    player.GetComponent<Basicmovement>().enabled = false;
                    player.GetComponent<WeaponManager>().DisableWeapon();
                    facNum =2; 
                    vendUI.SetActive(true);
                    ShowSprites(facNum);    
                }
                if(distance <=interactRange && hit.transform.gameObject == buyInteractable[3])
                {
                    player.GetComponent<Basicmovement>().enabled = false;
                    player.GetComponent<WeaponManager>().DisableWeapon();
                    facNum = 3; 
                    vendUI.SetActive(true);
                    ShowSprites(facNum);    
                }
                else
                {
                    
                }
                

            }   

        }
    }

    public void CloseWindow()
    {
        player.GetComponent<Basicmovement>().enabled = true;
        player.GetComponent<WeaponManager>().EnableWeapon();
        vendUI.SetActive(false);
    }

    void ShowSprites(int factionNum)
    {
        for (int i = 0; i < facAbilitySprite.Count; i++ )
        {
            facAbilitySprite[i].GetComponent<Image>().sprite = vendList[factionNum].GetComponent<VendingMachine>().currentSprites[i];
            costText[i].SetText(vendList[facNum].GetComponent<VendingMachine>().selectedAbilityPickups[i].GetComponent<AbilityLoot>().cost.ToString());
        }
    }

    public void PurchaseAbility1()
    {
        if(player.GetComponent<PlayerManager>().currentMoney >= vendList[facNum].GetComponent<VendingMachine>().selectedAbilityPickups[0].GetComponent<AbilityLoot>().cost )
        {
            player.GetComponent<PlayerManager>().currentMoney = player.GetComponent<PlayerManager>().currentMoney - vendList[facNum].GetComponent<VendingMachine>().selectedAbilityPickups[0].GetComponent<AbilityLoot>().cost;
            GameObject abilityPickupInstance = Instantiate(vendList[facNum].GetComponent<VendingMachine>().selectedAbilityPickups[0], spawnPoints[facNum].position, Quaternion.identity);    
        }
        
    }
    public void PurchaseAbility2()
    {
        if(player.GetComponent<PlayerManager>().currentMoney >= vendList[facNum].GetComponent<VendingMachine>().selectedAbilityPickups[1].GetComponent<AbilityLoot>().cost )
        {
            player.GetComponent<PlayerManager>().currentMoney = player.GetComponent<PlayerManager>().currentMoney - vendList[facNum].GetComponent<VendingMachine>().selectedAbilityPickups[0].GetComponent<AbilityLoot>().cost;
            GameObject abilityPickupInstance = Instantiate(vendList[facNum].GetComponent<VendingMachine>().selectedAbilityPickups[1], spawnPoints[facNum].position, Quaternion.identity);    
        }    
    }
    public void PurchaseAbility3()
    {
        if(player.GetComponent<PlayerManager>().currentMoney >= vendList[facNum].GetComponent<VendingMachine>().selectedAbilityPickups[2].GetComponent<AbilityLoot>().cost )
        {
            player.GetComponent<PlayerManager>().currentMoney = player.GetComponent<PlayerManager>().currentMoney - vendList[facNum].GetComponent<VendingMachine>().selectedAbilityPickups[0].GetComponent<AbilityLoot>().cost;
            GameObject abilityPickupInstance = Instantiate(vendList[facNum].GetComponent<VendingMachine>().selectedAbilityPickups[2], spawnPoints[facNum].position, Quaternion.identity);    
        } 
    }

}
