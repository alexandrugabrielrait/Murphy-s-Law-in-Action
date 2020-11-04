using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiSender : ElectronicSender
{
    public ElectronicReceiver[] others;

    public override void SendOn()
    {
        if (target)
            target.TurnOn();
        foreach (ElectronicReceiver rec in others)
            rec.TurnOn();
    }
    public override void SendOff()
    {
        if (target)
            target.TurnOff();
        foreach (ElectronicReceiver rec in others)
            rec.TurnOff();
    }
}
