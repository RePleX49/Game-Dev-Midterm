using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Target is the camera boom
    [SerializeField] Transform Target, Player;
    [SerializeField] float TurnSpeed = 1;
    [SerializeField] float CameraLagSpeed = 0.5f;
    [SerializeField] float SphereCastDistance = 50.0f;
    float MouseX;
    float MouseY;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        Vector3 TargetPosition = Player.position;
        Vector3 SmoothedPosition = Vector3.Lerp(Target.position, TargetPosition, CameraLagSpeed * Time.deltaTime);
        Target.position = SmoothedPosition;

        if (Input.GetKey(KeyCode.Mouse1))
        {
            RaycastHit Hit;
            Physics.SphereCast(transform.position, 20, transform.forward, out Hit, SphereCastDistance);

            if (Hit.transform.gameObject != null)
            {
                Debug.Log("Sphere Cast Hit");
                Debug.DrawLine(transform.position, Hit.transform.position, Color.green, 5.0f);
            }
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        MouseX += Input.GetAxis("Mouse X") * TurnSpeed;
        MouseY -= Input.GetAxis("Mouse Y") * TurnSpeed;
        MouseY = Mathf.Clamp(MouseY, -30, 45);

        transform.LookAt(Target);

        Target.rotation = Quaternion.Euler(MouseY, MouseX, 0);
        Player.rotation = Quaternion.Euler(0, MouseX, 0);
    }
}
