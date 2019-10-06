using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Target is the camera boom
    [SerializeField] Transform Target, Player;
    [SerializeField] float TurnSpeed = 1.0f;
    [SerializeField] float CameraLagSpeed = 12.0f;
    [SerializeField] float SphereCastDistance = 75.0f;
    [SerializeField] float CameraVertMin = -30.0f;
    [SerializeField] float CameraVertMax = 48.0f;
    [SerializeField] float ZoomedFOV = 45.0f;
    float DefaultFOV;

    float MouseX;
    float MouseY;

    RaycastHit Hit;
    DissolveScript DScript;

    [SerializeField] Vector3 Offset;

    void Start()
    {
        DefaultFOV = Camera.main.fieldOfView;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            Camera.main.fieldOfView = ZoomedFOV;

            Physics.SphereCast(Player.position, 1, transform.forward, out Hit, SphereCastDistance);

            if (Hit.transform.gameObject != null)
            {           
                Debug.DrawLine(Player.position, transform.forward * SphereCastDistance, Color.green, 5.0f);
                DScript = Hit.transform.GetComponent<DissolveScript>();
            }
        }
        else
        {
            Camera.main.fieldOfView = DefaultFOV;
        }

        if(Input.GetKey(KeyCode.Mouse0))
        {
            if (DScript != null)
            {
                DScript.FlipFlopEffect();
            }
        }
    }

    void FixedUpdate()
    {
        Vector3 TargetPosition = Player.position + Offset;
        Vector3 SmoothedPosition = Vector3.Lerp(Target.position, TargetPosition, CameraLagSpeed * Time.deltaTime);
        Target.position = SmoothedPosition;       
    }

    // Update is called once per frame
    void LateUpdate()
    {
        MouseX += Input.GetAxis("Mouse X") * TurnSpeed;
        MouseY -= Input.GetAxis("Mouse Y") * TurnSpeed;
        MouseY = Mathf.Clamp(MouseY, CameraVertMin, CameraVertMax);

        transform.LookAt(Target);

        Target.rotation = Quaternion.Euler(MouseY, MouseX, 0);
        Player.rotation = Quaternion.Euler(0, MouseX, 0);
    }
}
