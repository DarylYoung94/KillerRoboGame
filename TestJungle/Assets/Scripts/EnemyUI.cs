using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    [Header("Health Bar")]
    public GameObject healthCanvas;
    public Image healthBar;

    [Header ("Damage Inidicator")]
    public GameObject damagePopPrefab;
    public Vector3 dmgPopLoc;

    [Header ("Total Damage Indicator")]
    public GameObject totalDamagePopPrefab;
    private GameObject totalDamageGO;
    public Vector3 totalDmgPopLoc;
    private float totalDamageTimer = 2.0f;
    private float totalDamageTaken = 0.0f;

    void Start()
    {
        if (!healthCanvas)
            healthCanvas = transform.Find("Canvas").gameObject;

        if (!healthBar)
        {
            healthBar = transform.Find("Healthbar").GetComponent<Image>();
        }
    }

    void Update()
    {
        healthCanvas.transform.LookAt(Camera.main.transform.position);
        healthBar.fillAmount = this.GetComponent<EnemyStats>().GetHealthPercentage();
    
        TotalDamageTimer();
    }
    
    public void EnemyDamaged(float amount)
    {
        if (amount >= 1.0f)
        {
            ShowDamagePop(amount);
        }
        else
        {
            ShowTotalDamagePop();
        }
    }

    public void ShowDamagePop(float damageTaken)
    {
        GameObject go = Instantiate(damagePopPrefab,
                                    transform.position + dmgPopLoc + RandomVector3(1.0f),
                                    Quaternion.identity);

        go.GetComponent<TextMesh>().text = damageTaken.ToString();
    }

    public void ShowTotalDamagePop()
    {   
        // 3 states this could be in
        // - First time damage is taken so we instantiate
        // - The damage popup timed out and is no longer active so we make it active and reset the timer.
        // - The damage popup is active and we need to reset the timer.
        if (totalDamageGO == null)
        {
            totalDamageGO = Instantiate(totalDamagePopPrefab,
                                        transform.position + totalDmgPopLoc,
                                        Quaternion.identity);
        }
        else if (totalDamageGO.activeSelf == false)
        {
            totalDamageGO.SetActive(true);
        }

        if(totalDamageGO != null && totalDamageGO.activeSelf)
        {
            totalDamageGO.GetComponent<TextMesh>().text = totalDamageTaken.ToString("n2");
        }

        // Reset Timer
        totalDamageTimer = 2.0f;
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

    private Vector3 RandomVector3(float range)
    {
        return new Vector3(Random.Range(-range,range), Random.Range(-range,range), Random.Range(-range,range));
    }
}
