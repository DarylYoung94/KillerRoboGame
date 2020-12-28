using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [Header ("Animation")]
    // Constant states
    const string PLAYER_SPRINT = "Sprinting";

    // Animator variables
    [SerializeField] Animator animator;
    float treadBaseSpeed = 1.5f;
    float treadMultiplier = 1.0f;
    WeaponManager weaponManager;
    string currentAnimaton = "";
    bool sprinting = false;

    void Awake()
    {
        weaponManager = GetComponent<WeaponManager>();

        if (weaponManager)
        {
            ChangeAnimationState(weaponManager.GetCurrentWeapon().GetAnimationString(), true);
        }
    }

    void Update()
    {
        animator.SetFloat("TreadSpeed", treadBaseSpeed * treadMultiplier);

        if (sprinting)
        {
            ChangeAnimationState(PLAYER_SPRINT, true);
        }
        else
        {
            ChangeAnimationState(weaponManager.GetCurrentWeapon().GetAnimationString(), true);
        }
    }

    void ChangeAnimationState(string newAnimation, bool crossFade)
    {
        if (currentAnimaton == newAnimation) return;

        if (!crossFade)
        {
            animator.Play(newAnimation);
        }
        else
        {
            animator.CrossFade(newAnimation, 0.2f);
        }
        currentAnimaton = newAnimation;
    }

    public void SetTreadSpeed (float speed) { treadMultiplier = speed; }
    public void SetSprinting () { sprinting = true; }
    public void SetWalking () { sprinting = false; }
}
