using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<EnemySettings> enemySettings;
    public List<GameObject> enemies = new List<GameObject>();

    private int waveLevel = 1;
    public Transform spawnPoint;
    public GameObject enemyPrefab;
    public int numberOfEnemies = 3;
    public float Speed;
    public float spawnTimer = 10;
    private float timer =0;
    private bool spawn = false;

 
    void Start()
    {
        SpawnWave();
    }

    // Update is called once per frame
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
                SpawnWave();
                spawn = false;
                waveLevel++;
            }
        }

        if (waveLevel <= enemySettings.Count)
        {
            enemyPrefab.GetComponent<EnemyStats>().UpdateEnemySettings(enemySettings[waveLevel-1]);
        }
    }

    void SpawnWave()  
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            GameObject enemyInstance = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            enemyInstance.GetComponent<Rigidbody>().AddForce(-spawnPoint.forward * Speed, ForceMode.Impulse);
            enemies.Add(enemyInstance);
        }
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
}
