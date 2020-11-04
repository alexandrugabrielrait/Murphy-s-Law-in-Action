using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlateDestroyer : Breakable
{
    public JumpPlate plate;
    public Material plateOn;
    public Material plateOff;
    public MeshRenderer top;

    private void Start()
    {
        health = plate.plateHealth;
        if (health != -1)
            top.material = plateOn;
    }

    public override void Break()
    {
        plate.on = false;
        top.material = plateOff;
    }
}
