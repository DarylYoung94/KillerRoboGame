using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyPickup : MonoBehaviour
{
    public int value;
    public int despawnTimer = 15;

    private void Start()
    {
         Destroy(this.gameObject , despawnTimer);
    }

    public void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Player")
            {
               
                Destroy(this.gameObject);
                
            }
        }

        

}
