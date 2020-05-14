//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float enemyLevel = 1;

    public int experience;

    public Image healthBar;
    public GameObject healthCanvas;
    public float healthness;
    public float startHealth;
    public float health = 0;

    public GameObject enemy;
    public GameObject player;

    public float damageTaken;
    public GameObject damagePop;
    public Vector3 dmgPopLoc;

    public float totalDamageTaken = 0;
    public GameObject totalDamagePrefab;
    private GameObject totalDamageGO;
    public Vector3 totalDmgPopLoc;
    float totalDamageTimer;

    
    public float dropChance;
    public float dropRate = 0.3f; //can change this in inspector for different enemies?
    public GameObject lootPrefab;
    
    bool alive = true;

    // Start is called before the first frame update
    void Start()
    {
        healthness = startHealth + 10 * enemyLevel;
        health = healthness;
        enemy = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        player = GameManager.instance.player;
        dropChance = Random.value;

        healthCanvas.transform.LookAt(Camera.main.transform.position);
        
        TotalDamageTimer();
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        damageTaken = amount;
        totalDamageTaken += amount;

        if (damagePop)
            ShowDamagePop();

        if (totalDamagePrefab)
            ShowTotalDamagePop();

        healthBar.fillAmount = health / healthness;
        if(alive && health <= 0f )
        {
            alive = false;

            if (dropChance < dropRate)
            {
                Loot();
            }

            player.GetComponent<PlayerXP>().AddExp(experience);  
            Die();
        }
    }
    

    public void ShowDamagePop()
    {
        GameObject DMG = Instantiate(damagePop,
                                     transform.position + dmgPopLoc + RandomVector3(1.0f),
                                     Quaternion.identity);
        if(DMG != null)
        {
            DMG.GetComponent<TextMesh>().text = damageTaken.ToString();
        }

    }

    public void ShowTotalDamagePop()
    {   
        // 3 states this could be in
        // - First time damage is taken so we instantiate
        // - The damage popup timed out and is no longer active so we make it active and reset the timer.
        // - The damage popup is active and we need to reset the timer.
        if (totalDamageGO == null)
        {
            totalDamageGO = Instantiate(totalDamagePrefab,
                                        transform.position + totalDmgPopLoc,
                                        Quaternion.identity);
        }
        else if (totalDamageGO.activeSelf == false)
        {
            totalDamageGO.SetActive(true);
        }

        if(totalDamageGO != null && totalDamageGO.activeSelf)
        {
            totalDamageGO.GetComponent<TextMesh>().text = totalDamageTaken.ToString("0.00");
        }

        ResetTimer();
    }

    void Die()
    {
        Destroy(this.gameObject);
    }

    public void Loot()
    {
        GameObject loot = Instantiate(lootPrefab, enemy.transform.position, Quaternion.identity);
    }

    private Vector3 RandomVector3(float range)
    {
        return new Vector3(Random.Range(-range,range), Random.Range(-range,range), Random.Range(-range,range));
    }

    private void TotalDamageTimer()
    {
        totalDamageTimer -= Time.deltaTime;

        if (totalDamageTimer < 0)
        {
            totalDamageTaken = 0;
            if (totalDamageGO)
            {
                totalDamageGO.GetComponent<TextMesh>().text = totalDamageTaken.ToString("0.00");
                totalDamageGO.SetActive(false);
            }
        }
    }

    private void ResetTimer()
    {
        totalDamageTimer = 2.0f;
    }
}
