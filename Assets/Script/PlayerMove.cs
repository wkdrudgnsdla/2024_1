using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float turnSpeed = 2f;
    public float brakeForce = 10f;
    public float turnDamping = 5f;
    public float currentSpeed;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.drag = 0.1f;
        rb.angularDrag = 0.1f;
    }

    void FixedUpdate()
    {
        float moveInput = Input.GetAxis("Vertical");
        float turnInput = Input.GetAxis("Horizontal");

        rb.AddForce(transform.right * moveInput * moveSpeed, ForceMode.Acceleration);

        if (moveInput != 0)
        {
            if (turnInput != 0)
            {
                rb.AddTorque(Vector3.up * turnInput * turnSpeed, ForceMode.Acceleration);
            }
            else
            {
                rb.angularVelocity = Vector3.Lerp(rb.angularVelocity, Vector3.zero, Time.fixedDeltaTime * turnDamping);
            }
        }

        float forwardSpeed = Vector3.Dot(rb.velocity, transform.right);
        rb.velocity = transform.right * forwardSpeed;

        if (Input.GetKey(KeyCode.Space) && rb.velocity.magnitude > 0.1f)
            rb.AddForce(-rb.velocity.normalized * brakeForce, ForceMode.Acceleration);

        currentSpeed = rb.velocity.magnitude;
    }
}
