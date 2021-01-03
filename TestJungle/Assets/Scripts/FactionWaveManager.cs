using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FactionWaveManager : MonoBehaviour
{
    public List<FactionWaveSettings> factionWave;
    public List<GameObject> enemies = new List<GameObject>();
    public List<int> enemyIndex ;
    public List <GameObject> typeOfEnemy;
    public List <GameObject> groups;

    public Transform spawnPoint;
    public NavMeshAgent agent;
    public FactionFunctions fun;
    
    
    public int factionNum;
    
    [SerializeField] private int factionWaveIndex;
    private float speed = 5f;

    public int totalData;
    private int index;
    
    public float spawnTimer = 10f;
    private float timer =0;
    public int waveNumber;
    private bool spawn = false;

    public float globalSpawnTimer = 20f;
    public float globalTimer =0.0f;
    public bool globalSpawn = true;
    public float rangeFromSpawn = 5f;
    public FactionType.Faction faction;
    
    private void Start()
    {
        
        globalSpawn =true;
        index = 0;
        SetFactionWave();
        GetWaveInfo(factionWaveIndex);    
        SpawnWave();
        waveNumber++;
    }

    void Update()
    {
        CheckForDeadEnemies();
        
        if (enemies.Count == 0)
        {
            timer += Time.deltaTime;
            if (timer >= spawnTimer)
            {
                spawn = true;
                timer = 0;
            }

            if (spawn == true)
            {
                SetFactionWave();
                index = 0;
                SpawnWave();
                spawn = false;
                waveNumber++;
                CallWaveFunction();
            }
        }

       if(globalSpawn)
        {
            globalTimer += Time.deltaTime;
            if(globalTimer >=globalSpawnTimer)
            {   
                RecallWave();  
            } 
        }
        if(factionWaveIndex ==2)
        {
            Debug.Log("callfunction");
            CallWaveFunction();
        }
        RemoveGroups();
    }
   
    public void GetWaveInfo(int factionWaveIndex)
    {
        waveNumber = factionWave[factionWaveIndex].waveNumber;
        typeOfEnemy = factionWave[factionWaveIndex].typeOfEnemy;
        enemyIndex = factionWave[factionWaveIndex].enemyIndex;
        spawnTimer = factionWave[factionWaveIndex].spawnTimer;
    }

    void SpawnWave()
    {
        // call functions depending on what wave is spawned
        globalSpawn = true;
        GameObject group = new GameObject ("Group");
        group.AddComponent<FactionFunctions>();
        group.transform.parent = this.transform;
        groups.Add(group);

        for (int i = 0; i < enemyIndex.Count ; i++)
        {
            GameObject enemyInstance = Instantiate(typeOfEnemy[enemyIndex[index]], spawnPoint.transform.position, Quaternion.identity);
            enemyInstance.transform.parent = group.transform;
            enemyInstance.GetComponent<Rigidbody>().AddForce(-spawnPoint.forward * speed, ForceMode.Impulse);
            int cost = enemyInstance.GetComponent<DataManager>().unitCost;
            totalData = totalData - cost;
            enemyInstance.GetComponent<DataManager>().dataCollected =cost;
            //enemyInstance.tag = "Faction"+factionNum;
            enemyInstance.GetComponent<FactionType>().faction=faction;
            enemies.Add(enemyInstance);

            if (enemyInstance.GetComponent<RandomMovement>() != null)
            {
                enemyInstance.GetComponent<RandomMovement>().campLocation = enemies[0].gameObject;
            }
            if(factionNum==1 && factionWaveIndex ==2)
            {
                
            }

            index++;
        }
        group.GetComponent<FactionFunctions>().Initialise(enemies, faction);
    }
    void CheckForDeadEnemies()
    {
        for (int i=0; i<enemies.Count; i++)
        {
            if (!enemies[i])
            {
                enemies.RemoveAt(i);
            }
        }
    }

    void SetFactionWave()
    {
        //choose faction wave index based on total data
        if (totalData>=100 && totalData <175)
        {
            factionWaveIndex = 0;
        }
        else if( totalData >=175 && totalData <=200)
        {
            Debug.Log("175-200");
            factionWaveIndex =1;
            
        }
        else if (totalData >200 && totalData <=300)
        {
            Debug.Log("200-300");
            factionWaveIndex = 2;
        }
    
        GetWaveInfo(factionWaveIndex);
    }

    void GetFactionData(GameObject unit)
    {
        totalData = totalData + unit.GetComponent<DataManager>().GetData();
    }

    void RecallWave()
    {
        foreach(GameObject enemy in enemies)
        {
            if(enemy.GetComponent<RandomMovement>() != null)
            {
                if(enemy.GetComponent<RandomMovement>().enabled)
                {
                    enemy.GetComponent<RandomMovement>().enabled  = false;
                }
            
            }
            
            agent = enemy.GetComponent<NavMeshAgent>();
            agent.SetDestination(spawnPoint.transform.position);
            
            float distanceFromSpawn = Vector3.Distance(enemy.transform.position , spawnPoint.transform.position);
            if (distanceFromSpawn <= rangeFromSpawn)
            { 
                GetFactionData(enemy);                    
                Destroy(enemy);
                
            }   
        }

        if (enemies.Count ==0)
        {
            globalSpawn = false;
            globalTimer = 0;
        }
    }

    void CallWaveFunction()
    {
        fun.SearchForFactions();

    }

    void RemoveGroups()
    {
        foreach(GameObject group in groups )
        {
            if(group.transform.childCount == 0)
            {
                Destroy(group);
            }
        }
    }

}
    
