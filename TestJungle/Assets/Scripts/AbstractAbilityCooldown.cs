using UnityEngine;
using System.Collections;

public abstract class AbstractAbilityCooldown : MonoBehaviour {

    [SerializeField] protected AbstractAbility ability;
    [SerializeField] protected GameObject abilityHolder;
    [SerializeField] protected KeyCode abilityKeyCode;

    protected float coolDownDuration;
    protected float nextReadyTime;
    protected float coolDownTimeLeft;

    void Start()
    {
        if (ability != null && abilityHolder != null && abilityKeyCode != null)
        {
            Initialise(ability, abilityHolder, abilityKeyCode);
        }
    }

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

    public AbstractAbility GetAbility() { return ability; }

    public void SetCooldownMultiplier(float multiplier) { coolDownDuration = ability.aBaseCoolDown * multiplier; }
}