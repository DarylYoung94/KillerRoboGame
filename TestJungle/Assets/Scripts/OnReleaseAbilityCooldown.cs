using UnityEngine;
using System.Collections;

/*
 *  Cooldown type : Release Cast
 *      Ability can be held then released.
 *      Cooldown starts as the key is released.
 */
public class OnReleaseAbilityCooldown : AbstractAbilityCooldown {
    
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
        }

        if(Input.GetKeyUp(abilityKeyCode) && abilityReady)
        {
            ButtonUp();
            PutOnCooldown();
            abilityReady = false;
        }
        SetIconFill();

    }
}