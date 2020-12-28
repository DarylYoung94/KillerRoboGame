using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public int dataCollected = 0;
    public int unitCost;

    public void CollectData(int amount)
    {
        dataCollected += amount;
    }

    public void ResetData()
    {
        dataCollected = 0;
    }

    public void AddToDataCollected(int amount)
    {
        dataCollected += amount;
    }

    public int GetData() { return dataCollected; }
}
