using CaveIn.Core;
using CaveIn.UI;
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
    bool isPaused = false;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        FindObjectOfType<LevelUI>().OnPause.AddListener(Pause);
        FindObjectOfType<LevelUI>().OnUnpause.AddListener(Unpause);
        sensitivity = FindObjectOfType<ProgressTracker>().sensitivity;
    }

    void Update()
    {
        if (isPaused) return;

        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

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

    private void Pause()
    {
        isPaused = true;
    }

    private void Unpause()
    {
        isPaused = false;
    }
}
