using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyFaction : MonoBehaviour
{
    public Factions factions;

    public int disposition;

    public int disFaction1;
    public int disFaction2;
    public int disFaction3;
    public int disFaction4;
        void Start()
    {
        disposition = factions.disposition;
    
    }
     void Update()
    {
       disFaction1 = disposition - factions.faction1.disposition; 
       disFaction2 = disposition - factions.faction2.disposition; 
       disFaction3 = disposition - factions.faction3.disposition; 
       disFaction4 = disposition - factions.faction4.disposition; 


    }
}
