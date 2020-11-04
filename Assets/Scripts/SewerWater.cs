using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SewerWater : MonoBehaviour
{
    private IEnumerator OnTriggerEnter(Collider other)
    {
        PlayerMovement pm = other.GetComponent<PlayerMovement>();
        if (pm != null)
        {
            pm.inWater = true;
            WeaponManager wm = pm.GetComponent<WeaponManager>();
            if (wm != null)
                wm.getCurrentWeapon().gameObject.SetActive(false);
            AudioManager.instance.Play("water_splash");
            yield return new WaitForSeconds(1);
            pm.Die(1);
        }
        else if (other.GetComponent<Bullseye>())
        {
            AudioManager.instance.Play("water_splash");
            Debug.LogError("water");
            other.transform.position -= new Vector3(0, 100, 0);
            Destroy(other.GetComponent<Bullseye>().target);
        }
        else
        {
            AudioManager.instance.Play("water_splash");
            yield return new WaitForSeconds(1);
            other.transform.position -= new Vector3(0, 100, 0);
            Destroy(other.gameObject, 0.1f);
        }
    }
}
