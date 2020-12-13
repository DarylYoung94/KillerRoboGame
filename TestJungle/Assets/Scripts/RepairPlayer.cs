using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class RepairPlayer : MonoBehaviour
{
    public GameObject button;
    public GameObject player;
    public float interactRange;
    public TextMeshPro thankYou;
    
     void Update()
    {
        float distance = Vector3.Distance(button.transform.position, player.transform.position);

        if (Input.GetKeyDown(InputManager.Instance.GetInteractKey()))
        {
             Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                
                    if(hit.collider.transform.gameObject == button && distance <= interactRange)
                    {
                        
                        player.transform.GetComponent<PlayerManager>().HealPlayer();
                        //Debug.Log("healPlayer");
                        thankYou.SetText("Thank You");
                    }
            }
        }
        else if(distance >= interactRange)
        {
            thankYou.SetText("Repair Station");
        }
    
        
      
    }

    


}