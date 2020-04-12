using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAbilityTriggerable : MonoBehaviour
{
    [HideInInspector] public GameObject bombPrefab;
    public GameObject projectorPrefab;

    public float despawnTimer = 5f;
    public float projectileSpeed = 5f;

    private GameObject projectorTarget;

    Vector3 bombAim = -Vector3.one;

    // Update is called once per frame
    public void Hold()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            bombAim = hit.point;
            UpdateProjector();
        }
    }

    public void Release()
    {
        projectorTarget.SetActive(false);

        Transform firepoint = this.gameObject.transform.Find("firePoint");
        GameObject bombInstance = Instantiate(bombPrefab, firepoint.position, Quaternion.identity) as GameObject;
        Rigidbody bombRB = bombInstance.GetComponent<Rigidbody>();
        bombRB.transform.LookAt(bombAim);
        bombRB.AddForce(bombRB.transform.forward * projectileSpeed, ForceMode.Impulse);
    }

    private void UpdateProjector()
    {
        if (projectorTarget == null)
        {
            projectorTarget = Instantiate(projectorPrefab, bombAim, Quaternion.identity);
        }
        else
        {
            if (!projectorTarget.gameObject.activeSelf)
            {
                projectorTarget.gameObject.SetActive(true);
            }
            
            projectorTarget.transform.position = bombAim;
        }
    }
}
