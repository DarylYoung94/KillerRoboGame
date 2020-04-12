using UnityEngine;
using System.Collections;

public abstract class AbstractAbilityCooldown : MonoBehaviour {

    [SerializeField] protected KeyCode abilityKeyCode;
    [SerializeField] protected AbstractAbility ability;
    [SerializeField] protected GameObject abilityHolder;

    protected float coolDownDuration;
    protected float nextReadyTime;
    protected float coolDownTimeLeft;

    public void Initialise(AbstractAbility selectedAbility, GameObject abilityHolder, KeyCode keyCode)
    {
        abilityKeyCode = keyCode;
        ability = selectedAbility;
        coolDownDuration = ability.aBaseCoolDown;
        ability.Initialise(abilityHolder);
    }

    protected abstract void Update();

    protected virtual void ButtonDown() { ability.ButtonDown(); }
    protected virtual void ButtonUp() { ability.ButtonUp(); }

    protected void CoolDown() { coolDownTimeLeft -= Time.deltaTime; }
    protected void PutOnCooldown() 
    {
        nextReadyTime = coolDownDuration + Time.time;
        coolDownTimeLeft = coolDownDuration;
    }

}