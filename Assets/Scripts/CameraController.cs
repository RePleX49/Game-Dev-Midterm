using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Target is the camera boom
    [SerializeField] Transform Target, Player;
    [SerializeField] float TurnSpeed = 1;
    float MouseX;
    float MouseY;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        MouseX += Input.GetAxis("Mouse X") * TurnSpeed;
        MouseY -= Input.GetAxis("Mouse Y") * TurnSpeed;
        MouseY = Mathf.Clamp(MouseY, -30, 60);

        transform.LookAt(Target);

        Target.rotation = Quaternion.Euler(MouseY, MouseX, 0);
        Player.rotation = Quaternion.Euler(0, MouseX, 0);
    }
