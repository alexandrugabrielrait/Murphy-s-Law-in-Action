using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupWeapon : MonoBehaviour
{
    public int id;
    public DestroyableObject parent;

    private void OnTriggerEnter(Collider other)
    {
        WeaponManager wc = other.GetComponent<WeaponManager>();
        if (wc != null)
        {
            if (!wc.weapons[id].unlocked)
            {
                wc.weapons[id].unlocked = true;
                ++wc.numberOfUnlockedWeapons;
                wc.SwitchWeapon(id);
            }
            else
            {
                //add ammo wc.weapons[id];
            }
            parent.Die();
        }
    }
}