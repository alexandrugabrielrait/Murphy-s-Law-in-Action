using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : ElectronicReceiver
{

    public GameObject leftSlider;
    public GameObject rightSlider;
    public float slideSpeed = 1f;
    private AudioManager am;

    bool opening;
    bool closing;
    [HideInInspector]
    public bool opened;
    [HideInInspector]
    public bool closed = true;

    private void Awake()
    {
        am = GetComponent<AudioManager>();
    }

    override
    public void TurnOn()
    {
        closing = false;
        if (!opening && !opened)
        {
            opening = true;
            if (closed)
                am.Play("door_sliding");
            closed = false;
        }
    }

    override
    public void TurnOff()
    {
        opening = false;
        if (!closing && !closed)
        {
            closing = true;
            if (opened)
                am.Play("door_sliding");
            opened = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.gamePaused)
        {
            am.sounds[0].source.volume = 0f;
            return;
        }
        //am.Play("door_sliding");
        if (opening)
        {
            leftSlider.transform.position -= transform.right * Time.deltaTime * slideSpeed;
            rightSlider.transform.position += transform.right * Time.deltaTime * slideSpeed;

            if (leftSlider.transform.localPosition.x < 0)
            {
                leftSlider.transform.localPosition = new Vector3(0, leftSlider.transform.localPosition.y, leftSlider.transform.localPosition.z);
                rightSlider.transform.localPosition = new Vector3(3, rightSlider.transform.localPosition.y, rightSlider.transform.localPosition.z);
                opening = false;
                opened = true;
                am.Stop("door_sliding");
                am.Play("door_sliding_end");
            }
        }
        else if (closing)
        {
            leftSlider.transform.position += transform.right * Time.deltaTime * slideSpeed;
            rightSlider.transform.position -= transform.right * Time.deltaTime * slideSpeed;

            if (leftSlider.transform.localPosition.x > 1)
            {
                leftSlider.transform.localPosition = new Vector3(1, leftSlider.transform.localPosition.y, leftSlider.transform.localPosition.z);
                rightSlider.transform.localPosition = new Vector3(2, rightSlider.transform.localPosition.y, rightSlider.transform.localPosition.z);
                closing = false;
                closed = true;
                am.Stop("door_sliding");
                //am.Play("door_sliding_end");
            }
        }
    }
}
