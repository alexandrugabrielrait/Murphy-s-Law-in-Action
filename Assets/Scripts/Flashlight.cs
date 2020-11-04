using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : Weapon
{
    Light l;

    private void Awake()
    {
        l = GetComponent<Light>();
    }

    private void Update()
    {
        if (!enabled)
            return;
        if (Input.GetButtonDown("Fire2"))
                l.enabled = !l.enabled;
    }

    override
    public void EnableWeapon(bool b)
    {
        enabled = b;
        if (!b)
            l.enabled = false;
    }
}
