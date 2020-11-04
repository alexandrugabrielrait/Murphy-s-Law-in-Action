using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlate : MonoBehaviour
{
    public bool on = true;
    public Transform modelTransform;
    public Transform modelCenter;
    public Transform target;
    public int plateHealth = -1;

    private void Start()
    {
        modelCenter.LookAt(target);
        Quaternion rot = modelCenter.rotation;
        rot.x = 0;
        rot.z = 0;
        modelCenter.rotation = rot;
        Vector3 tpos = target.position;
        tpos.y = Mathf.RoundToInt(tpos.y) + 0.001f;
        target.position = tpos;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!on)
            return;
        PlayerMovement pm = other.GetComponent<PlayerMovement>();
        Vector3 movement = (target.position - other.transform.position).normalized;
        float distance = Vector3.Distance(target.position, other.transform.position);
        if (pm)
        {
            AudioManager.instance.Play("jump_pad_launch");
            pm.controller.Move(Vector3.up);
            pm.isControllable = false;
            pm.velocity = Vector3.zero;
            pm.bonusVelocity = 6 * Vector3.up + 0.8f * distance * movement;
        }
        else
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb)
            {
                AudioManager.instance.Play("jump_pad_launch");
                rb.velocity = 7 * Vector3.up + 0.8f * distance * movement;
            }
        }
    }
}
