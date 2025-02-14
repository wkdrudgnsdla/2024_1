using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    GameManager GM;

    public float moveSpeed = 10f;
    public float turnSpeed = 1f;
    public float brakeForce = 10f;
    public float turnDamping = 1f;
    public float extraGravity = 5f;
    public float currentSpeed;

    public bool moveable;
    public Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.drag = 0.1f;
        rb.angularDrag = 0.1f;
    }

    private void Start()
    {
        moveable = false;
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            turnSpeed = 0;
        }
        else if(Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftArrow))
        {
            turnSpeed = 0;
        }
        else if(Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.RightArrow))
        {
            turnSpeed = 0;
        }
        else if(Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftArrow))
        {
            turnSpeed = 0;
        }
        else
        {
            turnSpeed = 1;
        }
    }

    void FixedUpdate()
    {
        if (moveable)
        {
            float moveInput = Input.GetAxis("Vertical");
            float turnInput = Input.GetAxis("Horizontal");

            rb.AddForce(transform.right * moveInput * moveSpeed, ForceMode.Acceleration);

            if (turnInput != 0)
            {
                rb.angularVelocity = new Vector3(0, turnInput * turnSpeed, 0);
            }
            else
            {
                rb.angularVelocity = Vector3.Lerp(rb.angularVelocity, Vector3.zero, Time.fixedDeltaTime * turnDamping);
            }

            Vector3 horizontalForward = Vector3.ProjectOnPlane(transform.right, Vector3.up);
            if (horizontalForward.sqrMagnitude > 0.001f)
                horizontalForward.Normalize();
            float forwardSpeed = Vector3.Dot(rb.velocity, horizontalForward);
            Vector3 horizontalVelocity = horizontalForward * forwardSpeed;
            rb.velocity = horizontalVelocity + new Vector3(0, rb.velocity.y, 0);

            if (Input.GetKey(KeyCode.Space) && rb.velocity.magnitude > 0.1f)
                rb.AddForce(-rb.velocity.normalized * brakeForce, ForceMode.Acceleration);

            rb.AddForce(Vector3.down * extraGravity, ForceMode.Acceleration);

            currentSpeed = rb.velocity.magnitude;
        }
    }
}
