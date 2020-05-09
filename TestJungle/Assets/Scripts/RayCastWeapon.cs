using System.Collections;
using static System.Type;
using UnityEngine;

// Raycast type weapons e.g. damage applied off of a raycast
public abstract class RayCastWeapon : AbstractWeapon {
    public float range = 10.0f;
}