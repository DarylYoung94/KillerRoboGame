using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    [SerializeField] UnityEvent enter = new UnityEvent();
    [SerializeField] UnityEvent exit = new UnityEvent();

    private void OnTriggerEnter(Collider other)
    {
        enter.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        exit.Invoke();
    }
}
