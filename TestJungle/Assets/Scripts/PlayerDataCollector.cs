using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class PlayerDataCollector : MonoBehaviour
{
    [SerializeField] DataManager data;
    public float collectRange;
    public GameObject player;
    public GameObject dataConsole;
    [SerializeField] float collectTime = 0.0f;
    [SerializeField] float collectCooldown = 0.5f;
    [SerializeField] Transform dataCollectPoint;

    void Update()
    {
        
            PlayerCollectData();
        collectTime += Time.deltaTime;
    }
    
    public void PlayerCollectData()
    {
        if (Input.GetKey(InputManager.Instance.GetInteractKey()) && collectTime > collectCooldown)
        {
            
            

             Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                DataConsole dataConsole = hit.collider.transform.gameObject.GetComponent<DataConsole>();
                float distance = Vector3.Distance(hit.transform.position, player.transform.position);
                
                    if( distance <= collectRange)
                    {
                        
                        if(dataConsole && dataConsole.CollectData(dataCollectPoint))
                        {
                            collectTime = 0.0f;
                            data.CollectData(1);
                            Debug.Log("CollectData");
                        }

                   
                    }
            }
        }
    }
}
  