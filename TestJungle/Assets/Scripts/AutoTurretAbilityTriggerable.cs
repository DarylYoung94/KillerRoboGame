using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTurretAbilityTriggerable : MonoBehaviour
{
    public float atkSpeed;
    public float bulletSpeed;
    public float bulletDamage;
    public float playerSpeed;
    public float amountToHeal;
    public int abilityCost;
    private int currentMoney;
    public float payRate;

    public GameObject bulletPrefab;
    public GameObject player;
    public GameObject firePoint;
    
    private Coroutine shootCo;
    private Coroutine healAndPay;
    
    public bool firstPress = true;
    public bool canUse;
    public bool running = true;
    
    private void Start() 
    {
        player = GameManager.instance.player;
        playerSpeed = player.GetComponent<Basicmovement>().baseSpeed;
        firePoint = GameObject.Find("firePoint");
    }

    private void Update() 
    {
         currentMoney =  player.GetComponent<PlayerManager>().currentMoney;
        if(currentMoney>0)
        {
            canUse = true;
        }
        else
        {
            canUse=false;
        }
        if(currentMoney <=0)
        {
            currentMoney = 0;
            StopCoroutine(shootCo);
            StopCoroutine(healAndPay);
            player.GetComponent<Basicmovement>().baseSpeed = playerSpeed;
            firstPress = true;
        }
    }
    
    public void Toggle()
    {
        if(canUse)
        {
            if(firstPress)
            {
                healAndPay = StartCoroutine(HealAndPay());
                shootCo = StartCoroutine(ExecuteAfterTime(atkSpeed));  
                player.GetComponent<Basicmovement>().baseSpeed = playerSpeed *0.4f;
                firstPress=false;
            }
            else
            {
                StopCoroutine(shootCo);
                StopCoroutine(healAndPay);
                player.GetComponent<Basicmovement>().baseSpeed = playerSpeed;
                firstPress = true;
            }
        }
    }

    public void Shoot()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 bulletAim = hit.point;
            GameObject bulletInstance = Instantiate(bulletPrefab, firePoint.transform.position, Quaternion.identity);
            bulletInstance.GetComponent<BulletCollider>().Damage = bulletDamage;
            Rigidbody bulletRB = bulletInstance.GetComponent<Rigidbody>();
            bulletRB.transform.LookAt(bulletAim);
            bulletRB.AddForce(bulletRB.transform.forward *bulletSpeed, ForceMode.Impulse); 
        }    
    }
    
    public void heal()
    {
        float currentHealth;
        currentHealth = player.GetComponent<PlayerManager>().health;
        player.GetComponent<PlayerManager>().health = currentHealth + amountToHeal;
    }

    public void Pay()
    {
        player.GetComponent<PlayerManager>().currentMoney = currentMoney - abilityCost; 
    }

    IEnumerator ExecuteAfterTime(float autoSpeed)
    {
        while(running)
        {
            Shoot();  
            yield return new WaitForSeconds(autoSpeed);   
        } 
    }

    IEnumerator HealAndPay()
    {
        while(running)
        {
            heal();
            Pay();
            yield return new WaitForSeconds(payRate);
        }
    }
    
}
