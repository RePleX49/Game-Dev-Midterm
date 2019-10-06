using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    CharacterController cc;

    float InputHorizontal;
    float InputVertical;

    public GameObject PlayerBottom;
    public float MoveSpeed = 15;
    public float gravity = 16;
    public float JumpSpeed = 20;

    Vector3 MoveHorizontal;
    Vector3 MoveVertical;
    Vector3 FinalMoveVector;

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

        InputVertical = Input.GetAxis("Vertical");
        MoveVertical = transform.forward * (InputVertical);

        
    }

    void FixedUpdate()
    {
        Vector3 NewPos;
       
        NewPos = Vector3.ClampMagnitude(MoveHorizontal + MoveVertical, 1.0f) * MoveSpeed * Time.deltaTime;
       
        //TODO FIX DA JUMP
        if (cc.isGrounded)
        {
            NewPos.y = -gravity * Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                NewPos.y = JumpSpeed;
            }
        }
        else
        {
            NewPos.y -= gravity * Time.deltaTime;
        }

        cc.Move(NewPos);
    }
}
