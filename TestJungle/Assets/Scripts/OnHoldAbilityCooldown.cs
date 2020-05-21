using UnityEngine;
using System.Collections;

/*
 *  Cooldown type : Held Cast
 *      Ability can be held then released.
 *      Cooldown starts as the key is pressed.
 */
public class OnHoldAbilityCooldown : AbstractAbilityCooldown {
    
    bool abilityReady = true;
    protected override void Update () 
    {
        if (Time.time > nextReadyTime) 
        {
            abilityReady = true;
        }
        else 
        {
            CoolDown();
        }

        if(Input.GetKey(abilityKeyCode) && abilityReady) 
        {
            ButtonDown();
            PutOnCooldown();
        }

        if(Input.GetKeyUp(abilityKeyCode) && abilityReady)
        {
            ButtonUp();
            abilityReady = false;
        }
        SetIconFill();
    }

}