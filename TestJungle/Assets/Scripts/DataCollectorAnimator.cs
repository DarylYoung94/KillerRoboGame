using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCollectorAnimator : MonoBehaviour
{
    [SerializeField] private Animator animationController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Data Collector")
        {
            animationController.SetBool("InRange", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Data Collector")
        {
            animationController.SetBool("InRange", false);
        }
    }
}
