using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAbilityTriggerable : MonoBehaviour
{
    public float speed;
    public GameObject player;
    public float invTime = 1f;
    public float invCurrentTime =1f;
    void start()
    {
        player = GameManager.instance.player;
    }
    void Update()
    {
        if(invCurrentTime < invTime)
        {
            
            GetComponent<PlayerManager>().canTakeDamage = false ;
            invCurrentTime += Time.deltaTime;
        }
        if(invCurrentTime> invTime)
        {
            GetComponent<PlayerManager>().canTakeDamage = true ;
            invCurrentTime = 1f; 
        }
    }
    public void Dash()
    {
        invCurrentTime = 0f;
       GetComponent<Rigidbody>().AddForce(this.transform.forward * speed , ForceMode.Impulse) ; 
            
        
    }
}
