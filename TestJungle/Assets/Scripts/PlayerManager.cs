using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public int currentMoney;
    public Text currentMoneyUI;

    
    public int currentData;
    public Text currentDataUI;

    public GameObject player;
    public float health;
    public float startHealth = 90;
    public float missingHealth;
    public Image healthBar;
    public GameObject healthCanvas;
    public float maxHealth;
    public float leveledHealth;
    public Image ExpBar;
    public float EXP;
    public float EXP2;
    public Text healthText;
    public Text expText;
    public Text levelText;
    public float lvl;
    public bool canTakeDamage=true;

    // Start is called before the first frame update
    void Start()
    {
        //maxHealth = startHealth;
        maxHealth = startHealth + (player.GetComponent<PlayerXP>().level * 10);        
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        maxHealth = startHealth + (player.GetComponent<PlayerXP>().level * 10);
        EXP = player.GetComponent<PlayerXP>().exp;
        EXP2 = player.GetComponent<PlayerXP>().expToNextLevel;
        lvl = player.GetComponent<PlayerXP>().level;
        ExpBar.fillAmount = EXP / EXP2;
        healthText.text = health.ToString() + "/" + maxHealth.ToString();
        expText.text = EXP.ToString() + "/" + EXP2.ToString();
        levelText.text =   lvl.ToString();
        healthBar.fillAmount = health / maxHealth;
        if(health >= maxHealth)
        {
            health = maxHealth;
        }
        currentData = player.GetComponent<DataManager>().GetData();
        currentDataUI.text = currentData.ToString();
        currentMoneyUI.text = currentMoney.ToString();

        missingHealth = maxHealth - health;
    }

    public void TakeDamage(float amount)
    {
        if (canTakeDamage)
        {
            health -= amount;
            healthBar.fillAmount = health / maxHealth;
            if (health <= 0f)
            {
                Die();
            }
        }
    }

    public void HealPlayer()
    {
        float CM = (float)currentMoney ;
        if (CM >= missingHealth)
        {
            float healAmount = missingHealth;
            health = health + healAmount;
            currentMoney = currentMoney - (int)healAmount ;
        }
        else if (CM < missingHealth)
        {
            float healAmount = CM;
            health = health + healAmount;
            currentMoney = currentMoney - currentMoney;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Money")
        {
            int pickupValue;
            pickupValue = collision.transform.GetComponent<MoneyPickup>().value; 
            currentMoney = currentMoney += pickupValue;  
        }
    }
    
    public void Die()
    {
        Destroy(this.gameObject);
    }
}
