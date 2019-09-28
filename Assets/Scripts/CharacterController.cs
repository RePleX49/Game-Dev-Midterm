using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    Rigidbody rb;
    Vector3 NewPos;

    float MoveHorizontal;
    float MoveVertical;

    public float MoveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        MoveHorizontal = Input.GetAxis("Horizontal");
        MoveHorizontal *= MoveSpeed * Time.deltaTime;

        MoveVertical = Input.GetAxis("Vertical");
        MoveVertical *= MoveSpeed * Time.deltaTime;
    }

    void FixedUpdate()
    {
        rb.position += (transform.forward * MoveVertical);
        rb.position += (transform.right * MoveHorizontal);
    }
}
