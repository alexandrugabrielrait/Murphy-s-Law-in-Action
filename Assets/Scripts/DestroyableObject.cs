using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableObject : MonoBehaviour
{
    public bool laserProof = true;
    public bool reflective = false;
    public bool metallic = false;

    public void Die()
    {
        transform.position -= new Vector3(0, 100, 0);
        Destroy(gameObject, 0.5f);
    }
}
