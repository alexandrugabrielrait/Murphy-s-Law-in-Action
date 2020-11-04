using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBulb : Breakable
{
    public GameObject lightSource;
    public Material replacementMaterial;

    private void Start()
    {
        health = 10;
    }

    public override void Break()
    {
        GetComponent<MeshRenderer>().material = replacementMaterial;
        Destroy(lightSource);
    }

}
