using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    CharacterController cc;

    float InputHorizontal;
    float InputForward;
    float VerticalVelocity;
    [SerializeField] float MoveSpeed = 12.75f;
    
    [SerializeField] GameObject PlayerBottom;
    
    [SerializeField] float gravity = 16f;
    [SerializeField] float JumpForce = 7.25f;
    [SerializeField] float SlopeForce;
    [SerializeField] float SlopeRayLength;

    bool isJumping = false;
    
    Vector3 MoveHorizontal;
    Vector3 MoveForward;
    Vector3 NewPos;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        InputHorizontal = Input.GetAxis("Horizontal");
        MoveHorizontal = transform.right * (InputHorizontal);

        InputForward = Input.GetAxis("Vertical");
        MoveForward = transform.forward * (InputForward);    

        // Check if the Character is grounded
        if (cc.isGrounded)
        {
            if(isJumping)
            {
                isJumping = false;
            }

            // if grounded simply set the vertical move to be equal to gravity
            VerticalVelocity = -gravity * Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isJumping = true;
                VerticalVelocity = JumpForce;
            }
        }
        else
        {          
            // if not grounded decrease the vertical move exponentially
            VerticalVelocity -= gravity * Time.deltaTime;
        }

        // Clamp Magnitude to prevent "doubled" move speed diagonally
        NewPos = Vector3.ClampMagnitude(MoveHorizontal + MoveForward, 1.0f) * MoveSpeed;

        NewPos.y = VerticalVelocity;

        cc.Move(NewPos * Time.deltaTime);

        if((InputForward != 0 || InputHorizontal != 0) && OnSlope())
        {
            cc.Move(Vector3.down * cc.height / 2 * SlopeForce * Time.deltaTime);
        }
    }

    bool OnSlope()
    {
        if(isJumping)
        {
            return false;
        }

        RaycastHit Hit;

        if(Physics.Raycast(transform.position, Vector3.down, out Hit, cc.height / 2 * SlopeRayLength))
        {
            if(Hit.normal != Vector3.up)
            {
                return true;
            }
        }

        return false;
    }
}
