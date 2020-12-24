using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New  Faction Wave Settings", menuName = "FactionWaveSettings")]
public class FactionWaveSettings : ScriptableObject
{
 public int waveNumber;
 public int totalData;
 public List <GameObject> typeOfEnemy;
 public List<int> enemyIndex;
 public float spawnTimer;

}
