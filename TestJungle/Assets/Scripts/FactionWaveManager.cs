using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FactionWaveManager : MonoBehaviour
{

    public FactionType.Faction faction;
    public List<FactionWaveSettings> factionWave;
    public WaveBehaviour waveBehaviour;
    public int totalData;
    [SerializeField] private int factionWaveIndex;
    public List<GameObject> enemies = new List<GameObject>();
    private List<int> enemyIndex ;
    public List <GameObject> typeOfEnemy;
    public List <GameObject> groups;
    public List<WaveBehaviour> availableBehaviours;
    public Transform spawnPoint;

    private NavMeshAgent agent;
    private FactionFunctions fun;
      

    private float speed = 5f;
    private int index;
    public float spawnTimer = 10f;
    private float timer =0;
    public int waveNumber;
    private bool spawn = false;


    public float globalSpawnTimer = 20f;
    public float globalTimer =0.0f;
    private bool globalSpawn = true;
    public float rangeFromSpawn = 5f;

    [Header("Patrol/Scout")]
    public List<Transform> waypoints;
    public Transform waypointGO;
    public List<Transform> points = new List<Transform>();
    private Transform newPoint;
    public float distanceFromWaypoint;
    public bool patrolling = false;
    public bool scouting = false;
    public int targetPointIndex =0 ;


    public enum WaveBehaviour
    {
        None,
        DataCollector,
        Scout,
        Patrol,
        Attack,
        Defend,
    }
    
    private void Start()
    {
        GetWaypoints(waypointGO);
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

        if (globalSpawn)
        {
            globalTimer += Time.deltaTime;
            if(globalTimer >=globalSpawnTimer)
            {   
                RecallWave();  
                RemoveGroups(); 
            } 
            CallWaveFunction();
        }
    }
   
    public void GetWaveInfo(int factionWaveIndex)
    {
        waveNumber = factionWave[factionWaveIndex].waveNumber;
        typeOfEnemy = factionWave[factionWaveIndex].typeOfEnemy;
        enemyIndex = factionWave[factionWaveIndex].enemyIndex;
        spawnTimer = factionWave[factionWaveIndex].spawnTimer;
        availableBehaviours = factionWave[factionWaveIndex].behaviours;

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
            
            enemyInstance.GetComponent<FactionType>().faction=faction;
            enemies.Add(enemyInstance);

            if (enemyInstance.GetComponent<RandomMovement>() != null)
            {
                enemyInstance.GetComponent<RandomMovement>().campLocation = enemies[0].gameObject;
            }
            index++;
        }
        ChooseBehaviour();
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
        else if(totalData <100)
        {
            //cannot spawn another wave unless player funds it
        }
    
        GetWaveInfo(factionWaveIndex);
    }

    void GetFactionData(GameObject unit)
    {
        totalData = totalData + unit.GetComponent<DataManager>().GetData();
    }

    void RecallWave()
    {
        waveBehaviour = WaveBehaviour.None;

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
            points.Clear();
        }
    }

    void CallWaveFunction()
    {
        switch (waveBehaviour)
        {
            case WaveBehaviour.None:
                patrolling = false;
                scouting = false;
                break;

            case WaveBehaviour.Scout:
                Debug.Log("Scouting");
                scouting = true;
                if(scouting)
                {
                    Scout();
                }
                break;

            case WaveBehaviour.Patrol:
                Debug.Log("Patrolling");
                patrolling = true;  
                if(patrolling)
                {
                    Patrol();   
                }
                break;

            case WaveBehaviour.Attack:
                Debug.Log("Attacking");
                break;

            case WaveBehaviour.Defend:
                Debug.Log("Defending");
                break;

        }
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

    void ChooseBehaviour()
    {
        if(availableBehaviours.Count == 1 )
        {
            Debug.Log("1 available behaviour");
            waveBehaviour = availableBehaviours[0];
        }

        else if(availableBehaviours.Count == 2 )
        {
            Debug.Log("2 available behaviours");
            //choose between available behaviours
            float randomNum;
            randomNum = Random.Range(0f,1f) ;
            if(randomNum >= 0.3)
            {
                waveBehaviour = availableBehaviours[0];
            }
            else 
            {
                waveBehaviour = availableBehaviours[1];
            }
        }
    }

    void GetWaypoints(Transform waypointGO)
    {
        foreach(Transform child in waypointGO)
        {
            waypoints.Add(child);
        }
    }

    void Patrol()
    {        
        //choose two waypoints and path between them

        if(points.Count <2)
        {
            int random = Random.Range(0, waypoints.Count);
            newPoint = waypoints[random];
            points.Add(newPoint);
            targetPointIndex = 0;
        }

        foreach(GameObject enemy in enemies)
        {
            agent = enemy.GetComponent<NavMeshAgent>();
            agent.SetDestination(points[targetPointIndex].position);
                
            float distanceFromWaypoint = Vector3.Distance(enemy.transform.position , points[targetPointIndex].transform.position);

            if(distanceFromWaypoint <= rangeFromSpawn && targetPointIndex == 0)
            {
                targetPointIndex = 1;
                distanceFromWaypoint = Vector3.Distance(enemy.transform.position , points[targetPointIndex].transform.position);
            }
            
            if (distanceFromWaypoint <= rangeFromSpawn && targetPointIndex == 1)
            {
                targetPointIndex = 0;
                distanceFromWaypoint = Vector3.Distance(enemy.transform.position , points[targetPointIndex].transform.position);
            }
        }
    }

    void Scout()
    {
        //choose random point in waypoints and path enemy to it 
        if(points.Count< enemies.Count)
        {
            int random = Random.Range(0, waypoints.Count);
            newPoint = waypoints[random];
            points.Add(newPoint);
        }
        for(int i = 0; i < enemies.Count; i++)
        {
            bool destinationReached = false;
            agent = enemies[i].GetComponent<NavMeshAgent>();
               
            float distanceFromWaypoint = Vector3.Distance(enemies[i].transform.position , points[i].transform.position);
            if(distanceFromWaypoint > 3f)
            {
              agent.SetDestination(points[i].position); 
            }
        } 
    }
}
    
