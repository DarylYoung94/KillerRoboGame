using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineAbilityTriggerable : MonoBehaviour
{
     [HideInInspector] public GameObject minePrefab;
     [HideInInspector] public GameObject projectorPrefab;
     [HideInInspector] public GameObject timerParticles;
     [HideInInspector] public GameObject explosionParticles;

    private GameObject projectorTarget;
    Vector3 mineAim = -Vector3.one;
    
    public float despawnTimer = 3f;
    public GameObject spotlight;
    public float radius = 10f;
    public float power = 10f;
    public float upForce = 2f;
    public float Damage = 10f;
    public float mineTime = 1f;

    public void Hold()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        int layerMask = 1 << 8;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            mineAim = hit.point;
            UpdateProjector();
        }

    }
    public void Release()
    {
        projectorTarget.SetActive(false);

        GameObject mineInstance = Instantiate(minePrefab, mineAim, Quaternion.identity) as GameObject;
        
        StartCoroutine(spotlightTimer(mineTime,mineInstance));
        StartCoroutine(ExecuteAfterTime(despawnTimer,mineInstance));

    }

   
    private void UpdateProjector()
    {
        if (projectorTarget == null)
        {
            projectorTarget = Instantiate(projectorPrefab, mineAim, Quaternion.identity);
        }
        else
        {
            if (!projectorTarget.gameObject.activeSelf)
            {
                projectorTarget.gameObject.SetActive(true);
            }
            
            projectorTarget.transform.position = mineAim;
        }
    }

    IEnumerator ExecuteAfterTime(float mineTime, GameObject mineInstance)
    {
        yield return new WaitForSeconds(mineTime) ;
        Detonate(mineInstance);
        

    }
    IEnumerator spotlightTimer(float mineTime, GameObject mineInstance)
    {
        //add audio
        Debug.Log("here" + mineTime);
        //TODO
        GameObject Spotlight = mineInstance.transform.Find("Spotlight").gameObject;
       //GameObject.Find("Mine/Spotlight");
        Spotlight.SetActive(true);
       
        Debug.Log(GameObject.Find("Mine/Spotlight"));
        yield return new WaitForSeconds(mineTime);
        Spotlight.SetActive(false);

        yield return new WaitForSeconds(mineTime);
        Spotlight.SetActive(true);

       
        
        
    }


    public void Detonate(GameObject mineInstance)
    {
        if (mineInstance != null)
        {
            GameObject expParticles;
            expParticles = Instantiate (explosionParticles.gameObject, mineInstance.transform.position , Quaternion.identity );

            Vector3 explosionPosition = mineInstance.transform.position;
            //instantiate particle effects

            Collider[] colliders = Physics.OverlapSphere(explosionPosition, radius);
                
            foreach (Collider hit in colliders)
            {
                Enemy enemyHit = hit.transform.GetComponent<Enemy>();
                if (enemyHit != null)
                {
                    enemyHit.TakeDamage(Damage);
                    
                }
                Rigidbody rb = hit.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(power, explosionPosition, radius, upForce, ForceMode.Impulse);

                }

               
            }
            
            Destroy(mineInstance);
        }    
    }
}
