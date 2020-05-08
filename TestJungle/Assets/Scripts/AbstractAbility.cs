using System.Collections;
using static System.Type;
using UnityEngine;

public abstract class AbstractAbility : ScriptableObject {

    // Public Members
    public string aName = "New Ability";
    public string cdString = "QuickCast";
    public float aBaseCoolDown = 1f;
    
    // Private Members
    [SerializeField] private bool holding = false;

    // Public methods
    public abstract void Initialise(GameObject obj);

    public abstract void ButtonDown();
    public abstract void ButtonUp();

    public bool GetHolding() { return holding; }
    public void SetHolding(bool holding){ this.holding = holding; }

    public System.Type GetCooldownType() { return CooldownHelper.GetCooldownType(cdString); }
}