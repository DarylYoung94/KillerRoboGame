using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] EnemySettings activeEnemySettings = null;

    [Header ("State")]
    [SerializeField] bool alive = true;
    [SerializeField] int level = 1;

    [Header ("Health")]
    [SerializeField] float currentHealth = 10.0f;
    [SerializeField] float maxHealth = 0.0f; 

    [Header ("Experience")]
    [SerializeField] int experience = 100;

    [Header ("Drops")]
    [SerializeField] float dropRate = 0.3f;

     


    // Getters and Setters
    public float GetCurrentHealth (){ return currentHealth; }
    public float GetHealthPercentage (){ return currentHealth/maxHealth; }
    public void TakeDamage(float damage) { currentHealth -= damage; }
    public bool IsAlive() { return alive; }
    public void Dead() { alive = false; }
    public int GetExperience() { return experience; }
    public float GetDropRate() { return dropRate; }
    public int GetLevel() { return level; }

    void Start()
    {
        UpdateInternalSettings();
       
    }

    public void UpdateEnemySettings(EnemySettings enemySettings)
    {
        activeEnemySettings = enemySettings;
        UpdateInternalSettings();
    }

    public void UpdateInternalSettings()
    {
        if (activeEnemySettings)
        {
            currentHealth = activeEnemySettings.health;
            maxHealth = activeEnemySettings.health;
            experience = activeEnemySettings.expReward;
            dropRate = activeEnemySettings.dropRate;
            level = activeEnemySettings.level;
        }
    }
}
