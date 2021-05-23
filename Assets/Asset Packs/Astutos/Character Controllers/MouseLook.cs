using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] float sensitivity = 100f;
    [SerializeField] Transform playerBody;
    [SerializeField] bool smooth = false;
    [SerializeField] float smoothTime = 2f;


    float xRotation;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        if (smooth)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(xRotation, 0, 0), smoothTime);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        }
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
