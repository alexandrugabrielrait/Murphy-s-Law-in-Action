using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public List<Weapon> weapons = new List<Weapon>();
    public int currentWeapon;
    public int numberOfUnlockedWeapons;


    void Start()
    {
        foreach (Weapon w in GetComponentsInChildren<Weapon>())
            w.EnableWeapon(false);
        if (weapons[currentWeapon].unlocked)
            weapons[currentWeapon].EnableWeapon(true);
    }

    public Weapon getCurrentWeapon()
    {
        return weapons[currentWeapon];
    }

    public void SwitchWeapon(int id)
    {
        weapons[currentWeapon].EnableWeapon(false);
        currentWeapon = id;
        weapons[currentWeapon].EnableWeapon(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.gamePaused)
            return;
        if (numberOfUnlockedWeapons > 1)
        {
            if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                weapons[currentWeapon].EnableWeapon(false);
                if (Input.GetAxis("Mouse ScrollWheel") > 0)
                {
                    currentWeapon++;
                    if (currentWeapon >= weapons.Count)
                        currentWeapon = 0;
                }
                if (Input.GetAxis("Mouse ScrollWheel") < 0)
                {
                    currentWeapon--;
                    if (currentWeapon < 0)
                        currentWeapon = weapons.Count - 1;
                }
                while (!weapons[currentWeapon].unlocked)
                {
                    currentWeapon++;
                    if (currentWeapon >= weapons.Count)
                        currentWeapon = 0;
                }
                weapons[currentWeapon].EnableWeapon(true);
            }
            for (int i = 0; i < weapons.Count; ++i)
                if (weapons[i] && Input.GetKeyDown(KeyCode.Alpha1 + i))
                    SwitchWeapon(i);
        }
        else if (numberOfUnlockedWeapons == 0)
            weapons[currentWeapon].EnableWeapon(false);
    }
}
