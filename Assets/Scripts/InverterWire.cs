using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverterWire : ElectronicReceiver
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

    private void Start()
    {
        TurnOff();
    }

    override
    public void TurnOn()
    {
        sender.SendOff();
        if(wireOff != null)
            mr.material = wireOff;
    }

    override
    public void TurnOff()
    {
        sender.SendOn();
        if (wireOn != null)
            mr.material = wireOn;
    }
}
