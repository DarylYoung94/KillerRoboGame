using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ChangeDefaultMaterial : ScriptableWizard
{

    static void CreateWindow()
    {
        // Creates the wizard for display
        ScriptableWizard.DisplayWizard("Update default materials.",
            typeof(ChangeDefaultMaterial),
            "Default!");
    }

    [MenuItem("My Tools/Update Default Materials")]
    static void UpdateMaterials()
    {
        Material materialChanged = (Material)Resources.Load("URP", typeof(Material));
        Renderer[] rend = FindObjectsOfType(typeof(Renderer)) as Renderer[];
        Debug.Log(rend.Length);

        for (int i=0; i<rend.Length; i++)
        {
            //Debug.Log(rend[i].material.name);
            if(rend[i].material.name.StartsWith("Default"))
            {
                rend[i].sharedMaterial = materialChanged;
            }
        }
    }

    private void OnWizardCreate()
    {
        UpdateMaterials();
    }
}
