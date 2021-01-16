using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataConsoleSpawn : MonoBehaviour
{
    public GameObject dataConsole;
    public List<Transform> potentialSpawnPoints;
    public List<Transform> selectedSpawnPoints;

    private List<int> uniqueNumbers;
    private List<int> finishedList;

    void Start()
    {
        foreach(Transform child in this.transform) 
        {
            potentialSpawnPoints.Add(child);
        }
        
        uniqueNumbers = new List<int>();
        finishedList = new List<int>();

        SelectSpawnPoints();
        SpawnDataConsoles();
    }

    public void SelectSpawnPoints()
    {
        for (int i = 0; i < potentialSpawnPoints.Count; i++)
        {
            uniqueNumbers.Add(i);
        } 
        for(int i = 0; i< potentialSpawnPoints.Count; i ++)
        {
            int ranNum = uniqueNumbers[Random.Range(0,uniqueNumbers.Count)];
            finishedList.Add(ranNum);
            uniqueNumbers.Remove(ranNum);
        }
        for(int i = 0; i< potentialSpawnPoints.Count/2; i ++)
        {
            selectedSpawnPoints.Add(potentialSpawnPoints[finishedList[i]]);
        } 
    }

    public void SpawnDataConsoles()
    {
        GameObject group = new GameObject ("DCGroup");
        group.transform.parent = this.transform;

        for(int i = 0; i< selectedSpawnPoints.Count; i++)
        {
            GameObject DCInstance = Instantiate(dataConsole, selectedSpawnPoints[i].position, selectedSpawnPoints[i].transform.rotation);
            DCInstance.transform.parent = group.transform;
        }
    }
    
}
