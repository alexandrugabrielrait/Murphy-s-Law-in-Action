using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : ElectronicReceiver
{
    public DestroyableObject targetObject;

    override
    public void TurnOn()
    {
        if (targetObject != null)
            targetObject.Die();
    }

    override
    public void TurnOff()
    {

    }
}
