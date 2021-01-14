using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FacUI : MonoBehaviour
{
    public GameObject player;
    public GameObject mainUI;
    public GameObject donateText;
    public GameObject donateOptions;
    public GameObject donated;
    public GameObject noData;
    public List<GameObject> factions;
    public GameObject chosenFaction;

    public List<GameObject> factionInteractables;

    public float interactRange = 5f;
    public int factionIndex;
    public GameObject bellHit;

    
    void Start() 
    {
        player = GameManager.instance.player;   
        mainUI.SetActive(false);
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
                
                if(distance <=interactRange && hit.transform.gameObject == factionInteractables[0])
                {
                UpdateFaction(0);
                }
                if(distance <=interactRange && hit.transform.gameObject == factionInteractables[1])
                {
                UpdateFaction(1);
                }
                if(distance <=interactRange && hit.transform.gameObject == factionInteractables[2])
                {
                UpdateFaction(2);
                }
                if(distance <=interactRange && hit.transform.gameObject == factionInteractables[3])
                {
                UpdateFaction(3);
                }

            }   

        }
    }

    public void CloseWindow()
    {
        
        mainUI.SetActive(false);
    }

    public void ChooseDonate()
    {
        donateText.SetActive(false);
        donateOptions.SetActive(true);

    }

    public void UpdateFaction(int facInt)
    {
        mainUI.SetActive(true);
        donateText.SetActive(true);
        donateOptions.SetActive(false);
        chosenFaction = factions[facInt]; 
    }

    

    public void Donate50Data()
    {
        int currentData = player.GetComponent<DataManager>().GetData();
        if(currentData<=0)
        {
            donateOptions.SetActive(false);
            noData.SetActive(true);
        }
        if(currentData >=50)
        {
        player.GetComponent<DataManager>().RemoveData(50);
        chosenFaction.GetComponent<FactionWaveManager>().GetDonation(50);  
        }
        if (currentData<50)
        {
            
            chosenFaction.GetComponent<FactionWaveManager>().GetDonation(currentData) ; 
            player.GetComponent<DataManager>().ResetData();
        }

        donateOptions.SetActive(false);
        donated.SetActive(true);
        
    }
    public void Donate100Data()
    {
         int currentData = player.GetComponent<DataManager>().GetData();
         if(currentData<=0)
        {
            donateOptions.SetActive(false);
            noData.SetActive(true);
        }
        if(currentData >=100)
        {
        player.GetComponent<DataManager>().RemoveData(100);
        chosenFaction.GetComponent<FactionWaveManager>().GetDonation(50);  
        }
        if (currentData<100)
        {
            
            chosenFaction.GetComponent<FactionWaveManager>().GetDonation(currentData) ; 
            player.GetComponent<DataManager>().ResetData();
        }
        donateOptions.SetActive(false);
        donated.SetActive(true);
    }
    public void Donate200Data()
    {
        int currentData = player.GetComponent<DataManager>().GetData();
        if(currentData<=0)
        {
            donateOptions.SetActive(false);
            noData.SetActive(true);
        }
        if(currentData >=200)
        {
        player.GetComponent<DataManager>().RemoveData(200);
        chosenFaction.GetComponent<FactionWaveManager>().GetDonation(50);  
        }
        if (currentData<200)
        {
            
            chosenFaction.GetComponent<FactionWaveManager>().GetDonation(currentData) ; 
            player.GetComponent<DataManager>().ResetData();
        }
        donateOptions.SetActive(false);
        donated.SetActive(true);
    }
}
