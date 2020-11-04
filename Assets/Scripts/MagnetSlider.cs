using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagnetSlider : MonoBehaviour
{
    public WeaponManager wc;
    private Slider slider;
    private Vector3 scale;

    private void Start()
    {
        slider = GetComponent<Slider>();
        scale = slider.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        Magnet m = wc.getCurrentWeapon() as Magnet;
        if (!PauseMenu.gamePaused && m && m.unlocked && m.gameObject.activeSelf)
        {
            slider.transform.localScale = scale;
            slider.maxValue = m.ammoMax;
            slider.value = m.ammo;
        }
        else
        {
            slider.transform.localScale = Vector3.zero;
        }
    }
}