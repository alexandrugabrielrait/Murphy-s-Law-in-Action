using System.Collections;
using UnityEngine;

public class Magnet : Weapon
{
    public float range = 100f;
    public float ammo = 100;
    public float ammoMax = 100;
    public float ammoRegen = 1;
    public float ammoCost = 5;
    public float pushForce = 600;
    public CharacterController owner;
    public Rigidbody carried;

    private float time = 0, timeMax = 1;

    private Vector3 pos;

    private ParticleSystem muzzleflash;

    Camera cam;

    void Start()
    {
        cam = GetComponentInParent<Camera>();
        muzzleflash = GetComponentInChildren<ParticleSystem>();
    }

    void Update()
    {
        if (PauseMenu.gamePaused)
            return;
        time -= Time.deltaTime;
        if (ammo < ammoMax)
            ammo = Mathf.Min(ammo + ammoRegen * Time.deltaTime, ammoMax);
        if (carried && Input.GetButtonDown("Fire2"))
        {
            carried.AddForce(cam.transform.forward * pushForce);
            AudioManager.instance.Play("jump_pad_launch");
            Release();
            time = timeMax;
        }
        else
        {
            if (carried && (!isWeaponEnabled || !Input.GetButton("Fire1")))
            {
                Release();
            }
            else if (time <= 0 && isWeaponEnabled && ammo > 0 && !carried && Input.GetButton("Fire1"))
            {
                Shoot();
            }
            if (carried)
                Carry();
        }
    }

    void Shoot()
    {
        //Vector3 move = new Vector3(camera.transform.forward.x * pushBack, camera.transform.forward.y * Mathf.Sqrt(pushBack * 2f * 9.81f), camera.transform.forward.z * pushBack);

        //owner.Move(-move * Time.deltaTime);
        ammo = Mathf.Max(ammo - (ammoCost + ammoRegen) * Time.deltaTime, 0);
        muzzleflash.Play();
        if (ammo <= 0)
        {
            return;
        }

        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            Rigidbody body = hit.rigidbody;
            DestroyableObject o = hit.transform.GetComponent<DestroyableObject>();
            if (body && !body.isKinematic && o && o.metallic)
            {
                Vector3 movement = (Vector3.Lerp(body.transform.position, cam.transform.position, 0.5f * Time.deltaTime) - body.transform.position).normalized;
                float distance = Vector3.Distance(body.transform.position, cam.transform.position);

                body.velocity = 0.8f * distance * movement;

                if (Vector3.Distance(cam.transform.position + cam.transform.forward, body.transform.position) < 2)
                {
                    carried = body;
                    carried.useGravity = false;
                    carried.transform.SetParent(cam.transform);
                    Debug.Log("carried");
                }
            }
        }
    }

    private void Carry()
    {
        ammo = Mathf.Max(ammo - (ammoCost + ammoRegen) * Time.deltaTime, 0);
        muzzleflash.Play();
        if (ammo <= 0 || Vector3.Distance(cam.transform.position + cam.transform.forward, carried.transform.position) > 3)
        {
            Release();
            return;
        }

        carried.velocity = Vector3.zero;
        carried.angularVelocity = Vector3.zero;
        /*carried.useGravity = false;
        Vector3 movement = (Vector3.Lerp(carried.transform.position, cam.transform.position + cam.transform.forward * 4, 0.5f * Time.deltaTime) - carried.transform.position).normalized;
        float distance = Vector3.Distance(carried.transform.position, cam.transform.position + cam.transform.forward);

        carried.velocity = 0.8f * distance * movement;
        //body.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(hit.normal, cam.transform.position - body.transform.position, 0.8f * Time.deltaTime, 0));
        var previousUp = cam.transform.eulerAngles.y;
        var startYRotation = cam.transform.eulerAngles.y;

        var deltaRotation = previousUp - cam.transform.eulerAngles.y;
        var yRotation = startYRotation - deltaRotation;

        Quaternion target = Quaternion.Euler(0, yRotation, 0);
        carried.transform.rotation = Quaternion.Slerp(carried.transform.rotation, target, Time.deltaTime * 3);
        //hit.normal * (pushForce / body.mass);*/
    }

    private void Release()
    {
        //carried.useGravity = true;
        time = timeMax;
        pos = carried.transform.position;
        carried.transform.SetParent(null);
        carried.useGravity = true;
        carried.transform.position = pos;
        carried = null;
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
