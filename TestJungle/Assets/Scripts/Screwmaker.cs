using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Screwmaker : MonoBehaviour
{
    public GameObject button;
    public GameObject player;
    public float interactRange;
    public GameObject screw;
    public GameObject nut;
    public GameObject spawnPoint;
    public float cooldownTime = 20;
    public float currentTime;
    public TextMeshPro countdown;

    private void Start() {
        player = GameManager.instance.player;
        countdown.SetText("Screws ready!");

    }
    
     void Update()
    {

        if(currentTime > 0 )
        {
            currentTime-=  Time.deltaTime;
            countdown.SetText(Mathf.Round(currentTime).ToString());
        }
        if(currentTime < 0 )
        {
            currentTime = 0;
            countdown.SetText("Screws ready!");
        }
        




        float distance = Vector3.Distance(button.transform.position, player.transform.position);

        if (Input.GetKeyDown(InputManager.Instance.GetInteractKey()) && currentTime ==0)
        {
             Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                
                    if(hit.collider.transform.gameObject == button && distance <= interactRange)
                    {
                        MakeScrew();
                       currentTime = cooldownTime;
                    }
            }
        }
        else if(distance >= interactRange)
        {
           // thankYou.SetText("Repair Station");
        }

        void MakeScrew()
        {
            int numScrews = Random.Range(2,5); 
            for(int i = 0; i < numScrews; i++)
            {
                GameObject screwInstance = Instantiate(screw, spawnPoint.transform.position, Quaternion.identity);
                
            }
        }
    }
}
