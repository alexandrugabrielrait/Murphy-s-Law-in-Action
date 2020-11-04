using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropperPipe : ElectronicReceiver
{

    public GameObject spawnSpot;
    public DestroyableObject spawnedObjectType;
    public DestroyableObject spawnedObject;
    public bool on;

    override
    public void TurnOn()
    {
        if (!on)
            Spawn();
        on = true;
    }

    override
    public void TurnOff()
    {
        on = false;
    }

    public void Update()
    {
        if (on && spawnedObject == null)
            Spawn();
    }

    public void Spawn()
    {
        if (spawnedObject != null)
        {
            spawnedObject.Die();
        }
        spawnedObject = Instantiate(spawnedObjectType, spawnSpot.transform.position, spawnedObjectType.transform.rotation);
    }
}
