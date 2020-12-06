using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Faction", menuName = "Faction")]
public class Factions : ScriptableObject
{
    public Factions faction1;
    public Factions faction2;
    public Factions faction3;
    public Factions faction4;

   
    public Material factionColour; 
    public new string name;
    public string description;
    public int disposition;

  
}
