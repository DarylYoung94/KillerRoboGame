using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayFacData : MonoBehaviour
{
    public TextMeshPro dataText;
    public GameObject faction;
    
    void Update()
    {
        dataText.SetText( faction.GetComponent<FactionWaveManager>().totalData.ToString() + " Data");
    }
}
