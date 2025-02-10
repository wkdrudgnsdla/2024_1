using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float turnSpeed = 2f;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float moveInput = Input.GetAxis("Vertical");
        float turnInput = Input.GetAxis("Horizontal");

        rb.AddForce(transform.right * moveInput * moveSpeed, ForceMode.Acceleration);
        rb.AddTorque(Vector3.up * turnInput * turnSpeed, ForceMode.Acceleration);
    }
}
