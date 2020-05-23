using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mark : MonoBehaviour
{
    private delegate void Callback();

    // Start is called before the first frame update
    void Start()
    {
        electrocuted = false;
        marked = false;
    }

    IEnumerator WaitAndResetBool(float timeToWait, Callback callback)
    {
        yield return new WaitForSeconds(timeToWait);

        if(callback != null)
        {
            callback();
        }
    }

    // Electrocute stuff
    [SerializeField] private bool electrocuted = false;
    public float electrocuteTimer = 0.2f;

    public void Electrocuted()
    {
        electrocuted = true;
        StartCoroutine(WaitAndResetBool(electrocuteTimer, () => electrocuted = false));
    }

    public bool IsElectrocuted() { return electrocuted; }

    // Mark stuff
    [SerializeField] private bool marked = false;
    public float markedTimer = 5.0f;


    public void MarkEnemy()
    {
        marked = true;
        StartCoroutine(WaitAndResetBool(markedTimer, () => marked = false));
    }

    public bool IsMarked() { return marked; }
    public void ResetMark() { marked = false; }

}


