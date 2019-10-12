using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Target is the camera boom
    [SerializeField] Transform Target, Player;
    [SerializeField] float TurnSpeed = 1.0f;
    [SerializeField] float CameraLagSpeed = 12.0f;
    [SerializeField] float SphereCastDistance = 100.0f;
    [SerializeField] float CameraVertMin = -30.0f;
    [SerializeField] float CameraVertMax = 48.0f;
    [SerializeField] float ZoomedFOV = 45.0f;
    [SerializeField] float ZoomDuration = 12.75f;
    float DefaultFOV;

    float MouseX;
    float MouseY;

    float currentHitDistance;

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
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, ZoomedFOV, ZoomDuration * Time.deltaTime);

            if(Physics.Raycast(Player.position, transform.forward, out Hit, SphereCastDistance))
            {
                currentHitDistance = Hit.distance;
                DScript = Hit.transform.GetComponent<DissolveScript>();
            }
            else
            {
                currentHitDistance = SphereCastDistance;
                if(DScript)
                {
                    DScript = null;
                }            
            }
        }
        else
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, DefaultFOV, ZoomDuration * Time.deltaTime);
            if (DScript)
            {
                DScript = null;
            }
        }

        if(Input.GetKey(KeyCode.Mouse0))
        {
            if (DScript)
            {
                DScript.FlipFlopEffect();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine(Player.position, Player.position + (transform.forward * currentHitDistance));
        Gizmos.DrawWireSphere(Player.position + (transform.forward * currentHitDistance), 0.5f);
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
