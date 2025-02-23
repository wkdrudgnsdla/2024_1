using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    GameManager GM;
    public GameObject BoomEffect;
    public ParticleSystem Speed;

    public float SetSpeed;
    public float SetTurnSpeed;

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
        BoomEffect = Resources.Load("BoomEffect") as GameObject;
        Speed = GameObject.Find("Speed").GetComponent<ParticleSystem>();
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.drag = 0.1f;
        rb.angularDrag = 0.1f;
        SetSpeed = 6f;
        SetTurnSpeed = 1f;
    }

    private void Start()
    {
        moveable = false;
        moveSpeed = 6;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            turnSpeed = 0;
        }
        else if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftArrow))
        {
            turnSpeed = 0;
        }
        else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.RightArrow))
        {
            turnSpeed = 0;
        }
        else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftArrow))
        {
            turnSpeed = 0;
        }
        else
        {
            turnSpeed = SetTurnSpeed;
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

            // 현재 속도에 따라 radiusThickness를 0에서 0.5까지 선형 보간 적용
            var shape = Speed.shape;
            shape.radiusThickness = Mathf.Lerp(0f, 0.5f, Mathf.Clamp01(currentSpeed / 100f));
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("CrashEnemy"))
        {
            if (collision.gameObject.TryGetComponent<CrashEnemy>(out CrashEnemy enemy))
            {
                Vector3 pos = enemy.transform.position;
                StartCoroutine(BoomEF(pos));
            }
        }
    }

    IEnumerator BoomEF(Vector3 pos)
    {
        Debug.Log("CloneBoom");
        GameObject BEF = MonoBehaviour.Instantiate(BoomEffect);
        BEF.name = "BoomEffect";
        BEF.transform.position = pos;
        yield return new WaitForSeconds(0.2f);
        Destroy(BEF);
    }
}
