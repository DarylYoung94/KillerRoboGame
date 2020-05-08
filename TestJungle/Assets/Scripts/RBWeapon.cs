using System.Collections;
using static System.Type;
using UnityEngine;

// Rigibody type weapons e.g. projectiles with applied force
public abstract class RBWeapon : AbstractWeapon {
    public float force = 5.0f;
}
