using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ElectronicTimedWire : ElectronicReceiver
{
    ElectronicSender sender;
    MeshRenderer mr;
    public Material wireOn;
    public Material wireOff;
    public float time = 10;
    private float currentTime = 0;
    public TMP_Text textMesh;

    void Awake()
    {
        sender = GetComponent<ElectronicSender>();
        mr = GetComponentInChildren<MeshRenderer>();
        textMesh.text = Mathf.RoundToInt(time) + "";
    }

    override
    public void TurnOn()
    {
        sender.SendOn();
        if (wireOn != null)
            mr.material = wireOn;
        currentTime = time;
        textMesh.text = "";
    }

    private void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            textMesh.text = Mathf.RoundToInt(currentTime) + "";
        }
        else if (currentTime < 0)
        {
            currentTime = 0;
            sender.SendOff();
            textMesh.text = Mathf.RoundToInt(time) + "";
            if (wireOff != null)
                mr.material = wireOff;
        }
    }

    override
    public void TurnOff()
    {
    }
}