using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    Rigidbody rb;
    Vector3 NewPos;

    float InputHorizontal;
    float InputVertical;
    float MoveSpeedModifier;

    public GameObject PlayerBottom;

    Vector3 MoveHorizontal;
    Vector3 MoveVertical;
    Vector3 FinalMoveVector;

    public float MoveSpeed = 15;
    public float JumpForce = 2;

    bool IsGrounded = true;

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
    }

    void FixedUpdate()
    {
        if(!IsGrounded)
        {
            MoveSpeedModifier = 0.725f;
        }
        else
        {
            MoveSpeedModifier = 1;
        }

        rb.position += (Vector3.ClampMagnitude(MoveHorizontal + MoveVertical, 1.0f) * (MoveSpeed * MoveSpeedModifier) * Time.deltaTime);

        RaycastHit Hit;
        Physics.Raycast(PlayerBottom.transform.position, Vector3.down, out Hit);

        if(Hit.transform.gameObject)
        {
            if(Hit.distance < 0.1f)
            {
                IsGrounded = true;
            }
            else
            {
                IsGrounded = false;
            }
        }

        if(Input.GetKey(KeyCode.Space) && IsGrounded)
        {
            rb.AddForce((Vector3.up * JumpForce), ForceMode.Impulse);
        }
    }
}
