using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectronicSender : MonoBehaviour
{
    public ElectronicReceiver target;

    public virtual void SendOn()
    {
        if (target)
            target.TurnOn();
    }
    public virtual void SendOff()
    {
        if (target)
            target.TurnOff();
    }

}
