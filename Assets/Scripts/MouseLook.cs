using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Transform playerBody;
    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.gamePaused)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            return;
        }
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        float mouseX = Input.GetAxis("Mouse X") * SaveSystem.data.sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * SaveSystem.data.sensitivity * Time.deltaTime;

        LookUp(mouseY);

        playerBody.Rotate(Vector3.up * mouseX);

    }

    public void LookUp(float ammount)
    {
        xRotation -= ammount;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}