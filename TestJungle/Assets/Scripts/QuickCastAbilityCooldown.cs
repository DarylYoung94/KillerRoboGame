using UnityEngine;
using System.Collections;


/*
    Cooldown type : Quick Cast
    Ability is immediately cast and put on cooldown when the 
    ability key is pressed.
*/
public class QuickCastAbilityCooldown : AbstractAbilityCooldown {


    // Update is called once per frame
    protected override void Update() 
    {
        if (Time.time > nextReadyTime) 
        {
            if (Input.GetKeyDown(abilityKeyCode)) 
            {
                ButtonDown();
                PutOnCooldown();
            }
        }
        else 
        {
            CoolDown();
        }
        SetIconFill();

    }
}