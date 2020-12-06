using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningTriggerable : MonoBehaviour
{
    [HideInInspector] public GameObject lightningPrefab;
    [HideInInspector] public GameObject chainLightningPrefab;

    public float despawnTimer;
    public float range;
    public float chainRange;
    public float damage;
    public bool applyChains;

    public Transform barrelExit;

    public Transform arms;
    // Speed in rotations per second
    private float maxArmSpeed = 3.0f;
    private float currentArmSpeed = 0.0f;
    private float desiredSpeed = 0.0f;
    private float lerpSpeed = 0.5f;

    public void Hold()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 lightningAim = hit.point;
            float aimDistance  = Vector3.Distance(this.gameObject.transform.position, lightningAim);

            if (aimDistance <= range)
            {
                GameObject lightningGO = Instantiate(lightningPrefab, barrelExit.position, Quaternion.identity);
                lightningGO.transform.SetParent(this.gameObject.transform);
                lightningGO.transform.LookAt(lightningAim);

                Lightning lightning = lightningGO.GetComponent<Lightning>();
                lightning.Setup(null, aimDistance + 0.25f, applyChains, chainRange, chainLightningPrefab, damage);

                desiredSpeed = maxArmSpeed;
            }
        }
    }

    void Update()
    {
        if (this.enabled && arms == null)
        {
            arms = this.transform.GetComponent<WeaponManager>().GetWeaponGameObject().transform.Find("ElectricGun/ElectricGun/Arms");
        }

        currentArmSpeed = Mathf.Lerp(currentArmSpeed, desiredSpeed, lerpSpeed * Time.deltaTime);

    }

    void FixedUpdate()
    {
        if (this.enabled && arms != null)
        {
            arms.localEulerAngles += new Vector3(0,0,currentArmSpeed * 360f * Time.deltaTime);
        }
    }

    public void Release()
    {
        desiredSpeed = 0.0f;
    }
}
