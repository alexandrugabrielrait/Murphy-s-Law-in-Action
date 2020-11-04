using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmmoText : MonoBehaviour
{
    public WeaponManager wc;
    public GameObject crosshair;
    private TMP_Text t;

    private void Start()
    {
        t = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Gun g = wc.getCurrentWeapon() as Gun;
        if (!PauseMenu.gamePaused && g && g.unlocked && g.gameObject.activeSelf)
        {
            if (g.backpackAmmo >= 0)
                t.text = g.ammo + "/" + g.backpackAmmo;
            else
                t.text = g.ammo + "/\u221E";
        }
        else
        {
            t.text = "";
        }
        if(PauseMenu.gamePaused)
            crosshair.SetActive(false);
        else
            crosshair.SetActive(true);
    }
}