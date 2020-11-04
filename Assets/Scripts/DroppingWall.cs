using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppingWall : ElectronicReceiver
{
    bool dropping = false;

    public override void TurnOn()
    {
        dropping = true;
    }

    public override void TurnOff()
    {
    }

    private void Update()
    {
        if (dropping)
            transform.position += 0.1f * Vector3.down;
    }
}
