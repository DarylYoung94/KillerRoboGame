using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeedAbilityTriggerable : MonoBehaviour
{
    [HideInInspector] public GameObject buffPrefab;
    [HideInInspector] public GameObject projectorPrefab;

    public float maxRange = 5f;
    public float despawnTimer = 8f;
    public float attackSpeedMultiplier = 1.0f;

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
        StartCoroutine(DestroyObject(buffInstance.gameObject));
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "AtkSpd")
        {
            // Change GetFirstWeapon to apply attack speed to all weapons?
            AbstractWeapon.GetWeaponCooldownHolder(this.gameObject).SetCooldownMultiplier(1.0f/attackSpeedMultiplier);
        }
    }

    public void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "AtkSpd")
        {
            AbstractWeapon.GetWeaponCooldownHolder(this.gameObject).SetCooldownMultiplier(1.0f);
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

    // Workaround for unity bug where OnTriggerExit is not called if the object tracking entry
    // or exit is destroyed.
    IEnumerator DestroyObject(GameObject gameObject)
    {
        yield return new WaitForSeconds(despawnTimer);
        Destroy(gameObject);

        AbstractWeapon.GetWeaponCooldownHolder(this.gameObject).SetCooldownMultiplier(1.0f);
    }
}
