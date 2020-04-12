using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public KeyCode[] abilityKeys;
   public  List<KeyCode> keyCodeList = new List<KeyCode>(4);

    void Start()
    {
        keyCodeList.Add(KeyCode.Q);
        keyCodeList.Add(KeyCode.E);
        keyCodeList.Add(KeyCode.R);
        keyCodeList.Add(KeyCode.F);

        abilityKeys = new KeyCode[] { KeyCode.Q, KeyCode.E, KeyCode.R, KeyCode.F };
       
    }
    void Update()
    {
    }

    public KeyCode GetNextKeyCode ()
    {
        if (keyCodeList.Count == 0)
            Debug.Log("No key bindings available");

        KeyCode keyCode = keyCodeList[0];
        keyCodeList.RemoveAt(0);
        return keyCode;
    }
}
