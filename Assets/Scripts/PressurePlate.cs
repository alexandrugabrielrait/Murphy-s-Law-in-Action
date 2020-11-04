using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : ElectronicSender
{
    Vector3 pos;
    Transform modelTransform;

    private void Start()
    {
        modelTransform = GetComponentInChildren<Transform>();
        pos = modelTransform.position;
    }

    private void OnTriggerExit(Collider other)
    {
        modelTransform.position = pos;
        SendOff();
    }

    private void OnTriggerStay(Collider other)
    {
        if (PauseMenu.gamePaused)
            return;
        modelTransform.position = pos - new Vector3(0, 0.1f, 0);
        SendOn();
    }
}
