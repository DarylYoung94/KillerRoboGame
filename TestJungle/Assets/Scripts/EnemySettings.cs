using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Settings", menuName = "EnemySettings")]
public class EnemySettings : ScriptableObject
{
    public int level = 1;
    public float health = 10.0f;
    public int expReward = 100;
    public float dropRate = 0.3f;
}
