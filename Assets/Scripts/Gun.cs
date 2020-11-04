using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public int ammo = 30;
    public int ammoMax = 30;
    public int backpackAmmo = 0;
    public int backpackAmmoMax = 90;
    public float pushForce = 5f;
    public float pushBack = 5f;
    public float minRecoil = 0f;
    public float maxRecoil = 0.5f;
    public float recoilTime = 2f;
    public float reloadTime = 2f;
    public bool automatic;
    public CharacterController owner;
    public GameObject impactEffect;
    public GameObject bulletHole;
    public Transform magazine;

    private ParticleSystem muzzleflash;
    private bool recoil;
    private float recoilTimer;
    private float recoilIncrement;
    private bool isReloading;

    Camera cam;

    private float nextTimeToFire = 0f;

    void Start()
    {
        cam = GetComponentInParent<Camera>();
        muzzleflash = GetComponentInChildren<ParticleSystem>();
    }

    void Update()
    {
        if (PauseMenu.gamePaused)
            return;
        if (!isWeaponEnabled)
            return;
        if (recoil)
            recoilTimer -= Time.deltaTime;
        if (recoilTimer < 0)
        {
            recoil = false;
            recoilIncrement = 0;
        }
        if (ammo < ammoMax && backpackAmmo != 0 && Input.GetButtonDown("Reload"))
            Reload();
        if (ammo > 0 && ((automatic && Input.GetButton("Fire1")) || (!automatic && Input.GetButtonDown("Fire1"))) && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        if (isReloading)
            return;
        ammo--;
        AudioManager.instance.Play("ak47_shot");
        muzzleflash.Play();

        //Vector3 move = new Vector3(camera.transform.forward.x * pushBack, camera.transform.forward.y * Mathf.Sqrt(pushBack * 2f * 9.81f), camera.transform.forward.z * pushBack);

        //owner.Move(-move * Time.deltaTime);


        if (!recoil)
        {
            recoil = true;
            recoilTimer = recoilTime;
        }
        else
        {
            cam.GetComponent<MouseLook>().LookUp(Random.Range(minRecoil, maxRecoil));
            recoilTimer = recoilTime;
            recoilIncrement += 0.01f;
            if (recoilIncrement > 0.1f)
                recoilIncrement = 0.1f;
        }

        if (ammo == 0 && backpackAmmo != 0)
            Reload();

        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward + new Vector3 (0, Random.Range(0, recoilIncrement),
            Random.Range(-recoilIncrement, recoilIncrement)), out hit, range))
        {
            if (!hit.transform.GetComponent<PlayerMovement>())
            {
                GameObject impactInstance = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactInstance, 2f);
                GameObject holeInstance = Instantiate(bulletHole, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                Destroy(holeInstance, 30f);
                holeInstance.transform.parent = hit.transform;
            }

            Bullseye be = hit.collider.GetComponent<Bullseye>();

            if (be && be.target)
                be.SendOn();

            Breakable bk = hit.collider.GetComponent<Breakable>();

            if (bk)
                bk.Hit();

            Rigidbody body = hit.rigidbody;


            if (body == null || body.isKinematic)
            {
                return;
            }

            body.velocity -= hit.normal * (pushForce / body.mass);
        }
    }

    void Reload()
    {
        if (isReloading)
            return;
        isReloading = true;
        magazine.localPosition -= new Vector3(0, 2, 0);
        StartCoroutine(ReloadAfterDuration(reloadTime));
    }

    private IEnumerator ReloadAfterDuration(float t)
    {
        yield return new WaitForSeconds(t);
        if (backpackAmmo > 0)
        {
            int minimum = Mathf.Min(ammoMax - ammo, backpackAmmo);
            ammo += minimum;
            backpackAmmo -= minimum;
        }
        else if (backpackAmmo < 0)
        {
            ammo = ammoMax;
        }
        magazine.localPosition += new Vector3(0, 2, 0);
        isReloading = false;
    }

    override
    public void EnableWeapon(bool b)
    {
        isWeaponEnabled = b;
        if (b)
            transform.localScale = new Vector3(1, 1, 1);
        else
            transform.localScale = new Vector3(0, 0, 0);
    }
}
