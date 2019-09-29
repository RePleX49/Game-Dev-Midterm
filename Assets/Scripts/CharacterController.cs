using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    Rigidbody rb;
    Vector3 NewPos;

    float InputHorizontal;
    float InputVertical;

    Vector3 MoveHorizontal;
    Vector3 MoveVertical;
    Vector3 FinalMoveVector;

    public float MoveSpeed;

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
        rb.position += (Vector3.ClampMagnitude(MoveHorizontal + MoveVertical, 1.0f) * MoveSpeed * Time.deltaTime);
    }
}
