using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private List<KeyCode> abilityKeyCodes = new List<KeyCode> { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4 };
    [SerializeField] private KeyCode interactKeyCode = KeyCode.E;
    [SerializeField] private KeyCode weaponSwapKeyCode = KeyCode.Q;
    [SerializeField] private int nextKeyCode = 0;

    public KeyCode GetWeaponSwapKey() { return weaponSwapKeyCode; }
    public KeyCode GetInteractKey() { return interactKeyCode; }

    private static InputManager inputManager = null;
    public static InputManager Instance
    {
        get
        {
            if (inputManager == null)
            {
                inputManager = GameObject.FindObjectOfType<InputManager>();
                if (inputManager == null)
                {
                    inputManager = new GameObject("InputManager").AddComponent<InputManager>();
                }
            }
            return inputManager;
        }
    }

    public KeyCode GetKeyCodeByInt(int index)
    {
        return abilityKeyCodes[index];
    }
}
