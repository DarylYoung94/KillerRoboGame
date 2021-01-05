using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FactionFunctions : MonoBehaviour
{
    public float searchRadius = 30f;
    FactionWaveManager factionWaveManager;
    public List<GameObject> enemies;
    public float closestEnemy;
    Transform bestTarget = null;
    public GameObject factionSpawn;
    public FactionType.Faction faction;
    
    public void Initialise(List<GameObject> _enemies, FactionType.Faction _faction)
    {
        enemies = _enemies;
        faction = _faction;
    }

    public void SearchForFactions()
    {
        factionWaveManager = factionSpawn.GetComponent<FactionWaveManager>();
        enemies = factionWaveManager.enemies;
        closestEnemy = Mathf.Infinity;
        
        foreach(GameObject enemy in enemies)
        {
            
           Collider[] colliders = Physics.OverlapSphere(enemy.transform.position, searchRadius); 
            
           foreach(Collider hit in colliders)  
           {
              
               if(hit != null)
               {
                   
                   FactionType tempFactionType = hit.transform.GetComponent<FactionType>();
                   if(tempFactionType)
                   {
                       faction = tempFactionType.faction;

                        if(faction != FactionType.Faction.None && faction != enemy.GetComponent<FactionType>().faction)
                        {
                            Enemy enemyHit = hit.transform.GetComponent<Enemy>();
                            float distToEnemy = Vector3.Distance(hit.transform.position, enemy.transform.position);

                            if (enemyHit != null && distToEnemy < closestEnemy)
                                {
                                    closestEnemy = distToEnemy;
                                    bestTarget = enemyHit.transform;
                                }   
                    
                            enemy.GetComponent<EnemyAI>().target = bestTarget;
                            Debug.Log("TargetFound");
                        }
                    }
               }
           }  
        }
    }
}





