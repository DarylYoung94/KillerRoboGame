using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityPickupAnimation : MonoBehaviour
{
    [SerializeField] private float rotSpeed = 1.0f;
    [SerializeField] private float bobSpeed = 1.0f;
    [SerializeField] private float bobHeight = 1.0f;
    [SerializeField] private float waitTime = 0.3f;

    [SerializeField] private Vector3 botPosition, topPosition;

    void Start()
    {
        botPosition = this.transform.position;
        topPosition = botPosition + new Vector3(0, bobHeight, 0);
        StartCoroutine(Move(topPosition));
    }

    void Update()
    {
        topPosition = botPosition + new Vector3(0, bobHeight, 0);
        this.transform.localEulerAngles += new Vector3(0,rotSpeed*Time.deltaTime,0);
    }

    IEnumerator Move (Vector3 target)
    {
        while (Mathf.Abs((target - transform.position).y) > 0.01f)
        {
            Vector3 direction = target.y == topPosition.y ? Vector3.up : Vector3.down;
            transform.position += direction * bobSpeed * Time.deltaTime;

            yield return null;
        }

        yield return new WaitForSeconds(waitTime);

        Vector3 newTarget = target.y == topPosition.y ? botPosition : topPosition;

        StartCoroutine(Move(newTarget));   
    }

}
