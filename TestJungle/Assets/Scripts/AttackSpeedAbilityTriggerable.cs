using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeedAbilityTriggerable : MonoBehaviour
{
    [HideInInspector] public GameObject buffPrefab;
    [HideInInspector] public GameObject projectorPrefab;

    public float maxRange = 5f;
    public float despawnTimer = 8f;
    public float projectileSpeed = 5f;

    private GameObject projectorTarget;
    Vector3 buffAim = -Vector3.one;

    // Update is called once per frame
    public void Hold()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        int layerMask = 1 << 8;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            buffAim = hit.point;
            UpdateProjector();
        }
    }

    public void Release()
    {
        projectorTarget.SetActive(false);

        GameObject buffInstance = Instantiate(buffPrefab, buffAim, Quaternion.identity) as GameObject;
        Destroy(buffInstance.gameObject, despawnTimer);
        // OnTriggerExit doesnt trigger if the object is destroyed while you're in it.
        // Fixed in Unity 5.6 apparently
        this.gameObject.GetComponent<BasicAttack>().SetASMultipler(1.0f);
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "AtkSpd")
        {
            this.gameObject.GetComponent<BasicAttack>().SetASMultipler(2.0f);
        }
    }

    public void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "AtkSpd")
        {
            this.gameObject.GetComponent<BasicAttack>().SetASMultipler(1.0f);
        }
    }

    private void UpdateProjector()
    {
        if (projectorTarget == null)
        {
            projectorTarget = Instantiate(projectorPrefab, buffAim, Quaternion.identity);
        }
        else
        {
            if (!projectorTarget.gameObject.activeSelf)
            {
                projectorTarget.gameObject.SetActive(true);
            }
            
            projectorTarget.transform.position = buffAim;
        }
    }
}
