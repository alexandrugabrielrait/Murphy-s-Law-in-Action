using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectronicWire : ElectronicReceiver
{
    ElectronicSender sender;
    MeshRenderer mr;
    public Material wireOn;
    public Material wireOff;

    void Awake()
    {
        sender = GetComponent<ElectronicSender>();
        mr = GetComponentInChildren<MeshRenderer>();
    }

    override
    public void TurnOn()
    {
        sender.SendOn();
        if (wireOn != null)
            mr.material = wireOn;
    }

    override
    public void TurnOff()
    {
        sender.SendOff();
        if (wireOff != null)
            mr.material = wireOff;
    }
}