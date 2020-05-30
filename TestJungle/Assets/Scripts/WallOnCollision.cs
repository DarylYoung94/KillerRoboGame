using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallOnCollision : MonoBehaviour
{
    public GameObject shatteredPrefab;

    public void ShatterWall()
    {
        GameObject shatteredWall = Instantiate(shatteredPrefab, this.transform.position, Quaternion.identity);
        Destroy(shatteredWall, 5.0f);
        Destroy(this.gameObject);
    }
}
