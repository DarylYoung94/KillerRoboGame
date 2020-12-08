using UnityEngine;
using UnityEngine.UI;

public abstract class AbstractAbilityCooldown : MonoBehaviour {

    [SerializeField] protected AbstractAbility ability;
    [SerializeField] protected GameObject abilityHolder;
    [SerializeField] protected KeyCode abilityKeyCode;
    public MonoBehaviour triggerable;

    protected int iconIndex;
    [SerializeField] protected float coolDownDuration;
    protected float nextReadyTime;
    [SerializeField] protected float coolDownTimeLeft;

    void Start()
    {
        if (ability != null && abilityHolder != null)
        {
            Initialise(ability, abilityHolder, -1);
        }
    }

    public void Initialise(AbstractAbility selectedAbility, GameObject abilityHolder, int iconIndex)
    {
        ability = selectedAbility;
        coolDownDuration = ability.aBaseCoolDown;
        this.iconIndex = iconIndex;
        ability.Initialise(abilityHolder);

        triggerable = ability.triggerable;
    }

    public void SetKeyCode (KeyCode keyCode) { abilityKeyCode = keyCode; }

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

    public void SetIconFill()
    {
        if(iconIndex>=0 || iconIndex<=3 )
        {
            IconManager iconManager = GameObject.Find("iconmanager").GetComponent<IconManager>();
            iconManager.SetIconFill(iconIndex, (coolDownDuration-coolDownTimeLeft)/coolDownDuration);
        }
    }

}