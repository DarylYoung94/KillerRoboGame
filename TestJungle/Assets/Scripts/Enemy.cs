//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public GameObject screw;
    public GameObject nut;
    public bool canDropMoney = true;
    public bool killedByPlayer = true;
    public Transform _lastHit;
    
    // Necessary Components
    private EnemyUI enemyUI;
    private EnemyStats enemyStats;

    // Start is called before the first frame update
    void Start()
    {
        enemyStats = this.GetComponent<EnemyStats>();
        enemyUI = this.GetComponent<EnemyUI>();
    }

    public void TakeDamage(float amount, Transform lastHit)
    {
        _lastHit = lastHit;
        if (enemyUI)
            enemyUI.EnemyDamaged(amount);
        
        if(enemyStats)
        {
            enemyStats.TakeDamage(amount);

            if(enemyStats.IsAlive() && enemyStats.GetCurrentHealth() <= 0f)
            {
                enemyStats.Dead();

                if (Random.value < enemyStats.GetDropRate())
                {
                    LootManager.instance.SpawnAbilityPickup(transform.position);
                }

                if (canDropMoney)
                {
                    DropMoney(); 
                }

                if (killedByPlayer)
                {
                    TransferData(lastHit);
                }
                if(lastHit == GameManager.instance.player.transform )
                {
                   GameManager.instance.player.GetComponent<PlayerXP>().AddExp(enemyStats.GetExperience()); 
                }
                  
                Destroy(this.gameObject);
            }
        }
    }


    public void DropMoney()
    {
        int screwNum = Random.Range(0, 2);
        for (int i=0; i<screwNum; i++)
        {
            GameObject screwSpawn = Instantiate(screw,this.transform.position,Quaternion.identity);
            Rigidbody screwRB = screwSpawn.GetComponent<Rigidbody>();
            screwRB.AddForce(screwRB.transform.up * 8, ForceMode.Impulse);
        }

        int nutNum = Random.Range(0, 3);
        for (int i=0; i<nutNum; i++)
        {
            GameObject nutSpawn = Instantiate(nut,this.transform.position,Quaternion.identity);
            Rigidbody nutRB = nutSpawn.GetComponent<Rigidbody>();
            nutRB.AddForce(nutRB.transform.up * 8, ForceMode.Impulse);
        }
    }

    public void TransferData(Transform dataReceiver)
    {
        int tempData = this.gameObject.GetComponent<DataManager>().GetData();
        if(dataReceiver.GetComponent<DataManager>())
        {
            dataReceiver.GetComponent<DataManager>().AddToDataCollected(tempData);
        }
        //GameManager.instance.player.GetComponent<DataManager>().AddToDataCollected(tempData);
    }
    public void DetectLastHit()
    {
        
    }
}
