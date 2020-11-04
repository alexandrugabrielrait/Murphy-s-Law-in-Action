using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerHints : ElectronicReceiver
{
    protected bool showed = false;

    public Hint[] hints;

    public override void TurnOff()
    {
    }

    public override void TurnOn()
    {
        if (!showed)
        {
            showed = true;
            HintManager hm = FindObjectOfType<HintManager>();
            hm.Clear();
            for (int i = 0; i < hints.Length && i < HintManager.MAX_HINTS; ++i)
                hm.SetHint(i, hints[i]);
        }
    }
}
