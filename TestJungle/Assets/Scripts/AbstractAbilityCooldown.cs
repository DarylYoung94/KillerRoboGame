using UnityEngine;
using UnityEngine.UI;

public abstract class AbstractAbilityCooldown : MonoBehaviour {

    [SerializeField] protected AbstractAbility ability;
    [SerializeField] protected GameObject abilityHolder;
    [SerializeField] protected KeyCode abilityKeyCode;
    public MonoBehaviour triggerable;

    [SerializeField] protected float coolDownDuration;
    protected float nextReadyTime;
    [SerializeField] protected float coolDownTimeLeft;

    void Start()
    {
        if (ability != null && abilityHolder != null)
        {
            Initialise(ability, abilityHolder);
        }
    }

    public void Initialise(AbstractAbility selectedAbility, GameObject abilityHolder)
    {
        ability = selectedAbility;
        coolDownDuration = ability.aBaseCoolDown;
        ability.Initialise(abilityHolder);

        triggerable = ability.triggerable;
    }

    public void SetKeyCode (KeyCode keyCode) { abilityKeyCode = keyCode; }
    public KeyCode GetKeyCode () { return abilityKeyCode;}

    protected virtual void Update() { triggerable.enabled = this.enabled; }
    
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

    public float CoolDownProgress()
    {
        float temp = (coolDownDuration-coolDownTimeLeft)/coolDownDuration;
        return temp < 1.0f ? temp : 1.0f;
    }
}