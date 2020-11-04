using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public bool isWeaponEnabled;
    public bool unlocked;

    public abstract void EnableWeapon(bool b);
}
