using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretLevelEnabler : TriggerHints
{

    private void Start()
    {
        hints = new Hint[2];
        hints[0] = new Hint
        {
            text = "Secret Level Found! Complete the level in order to unlock.",
            key = KeyCode.H
        };
        hints[1] = new Hint
        {
            text = "Use 'H' to remove this hint.",
            key = KeyCode.H
        };
    }

    public override void TurnOn()
    {
        base.TurnOn();
        SaveSystem.unlockSecret = true;
    }

}
