﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [SerializeField] int dataCollected = 0;

    public void CollectData(int amount)
    {
        dataCollected += amount;
    }

    public void ResetData()
    {
        dataCollected = 0;
    }
}
