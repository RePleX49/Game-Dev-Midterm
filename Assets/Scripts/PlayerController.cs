using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    Vector3 NewPos;

    float InputHorizontal;
    float InputVertical;
    float MoveSpeedModifier = 1;

    public GameObject PlayerBottom;

    Vector3 MoveHorizontal;
    Vector3 MoveVertical;
    Vector3 FinalMoveVector;

    public float MoveSpeed = 15;
    public float JumpForce = 2;

    public bool IsGrounded = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        InputHorizontal = Input.GetAxis("Horizontal");
        MoveHorizontal = transform.right * (InputHorizontal);

        InputVertical = Input.GetAxis("Vertical");
        MoveVertical = transform.forward * (InputVertical);

        rb.position += (Vector3.ClampMagnitude(MoveHorizontal + MoveVertical, 1.0f) * (MoveSpeed * MoveSpeedModifier) * Time.deltaTime);
    }

    void FixedUpdate()
    {
        RaycastHit Hit;

        if (Physics.Raycast(PlayerBottom.transform.position, Vector3.down, out Hit))
        {
            if (Hit.distance < 0.08f)
            {
                IsGrounded = true;
            }
            else
            {
                IsGrounded = false;
            }
        }

        if (!IsGrounded)
        {
            MoveSpeedModifier = 0.65f;
        }
        else
        {
            MoveSpeedModifier = 1;
        }

        if (Input.GetKey(KeyCode.Space) && IsGrounded)
        {
            rb.AddForce((Vector3.up * JumpForce), ForceMode.Impulse);
        }
    }
}
