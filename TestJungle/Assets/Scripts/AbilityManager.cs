using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    [SerializeField] private List<AbstractAbility> unobtainedAbilities;
    [SerializeField] private List<AbstractAbility> obtainedAbilities;
    public static AbilityManager instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }    
}
 