using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : ElectronicReceiver
{
    public MeshRenderer top;
    public LineRenderer lr;
    public Material topOn;
    public Material topOff;
    public bool on = false;
    private Bullseye sensor;
    private Laser reflection;
    public Laser reflectionPrefab;
    private RaycastHit prevHit;

    override
    public void TurnOn()
    {
        on = true;
        if (topOn != null)
            top.material = topOn;
    }

    override
    public void TurnOff()
    {
        on = false;
        if (topOff != null)
            top.material = topOff;
    }

    void Update()
    {
        lr.enabled = on;
        if (on)
        {
            lr.SetPosition(0, lr.transform.position);
            RaycastHit hit;
            if (Physics.Raycast(lr.transform.position, lr.transform.forward, out hit))
            {
                if (hit.collider)
                {
                    if (reflection && DifferentHits(hit))
                        reflection.KillReflection();
                    lr.SetPosition(1, hit.point);
                    PlayerMovement p = hit.collider.GetComponent<PlayerMovement>();

                    if (p)
                        p.Die(3);
                    else
                    {
                        DestroyableObject d = hit.collider.GetComponent<DestroyableObject>();

                        if (d)
                        {
                            if (!d.laserProof)
                                d.Die();
                            else if (d.reflective && DifferentHits(hit))
                            {
                                reflection = Instantiate(reflectionPrefab, hit.point, Quaternion.FromToRotation(Vector3.up, Vector3.Reflect(lr.transform.forward, hit.normal)));
                                //Quaternion.FromToRotation(Vector3.up, hit.normal - lr.transform.forward));
                            }
                        }

                        Bullseye be = hit.collider.GetComponent<Bullseye>();

                        if (be && be.laserSensor)
                        {
                            sensor = be;
                            be.SendOn();
                        }
                        else if (sensor)
                        {
                            sensor.SendOff();
                            sensor = null;
                        }

                    }
                }
                prevHit = hit;
            }
            else
            {
                lr.SetPosition(1, lr.transform.position + lr.transform.forward * 5000);
            }
        }
        else
        {
            if (sensor)
            {
                sensor.SendOff();
                sensor = null;
            }
            if (reflection)
                reflection.KillReflection();
        }
    }

    public void KillReflection()
    {
        if (reflection)
            reflection.KillReflection();
        if (sensor)
            sensor.SendOff();
        Destroy(gameObject);
    }

    public bool DifferentHits(RaycastHit hit)
    {
        return prevHit.transform != hit.transform || prevHit.point != hit.point || prevHit.distance != hit.distance;
    }
}