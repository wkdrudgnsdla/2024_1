using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float followSpeed { get; set; } = 20f;
    public float rotationSpeed { get; set; } = 20f;

    [SerializeField] private Transform target;
private float distance = 10f;
private float height = 3f;

    private void Awake()
    {
        if (target == null)
            target = GameObject.Find("Player").transform;
    }

    private void Start()
    {
        followSpeed = 0.2f;
        rotationSpeed = 1;
    }

    private void LateUpdate()
    {
        Vector3 desiredPosition = target.position - target.right * distance + Vector3.up * height;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.smoothDeltaTime);
        Vector3 lookAtPos = target.position + Vector3.up * height;
        Quaternion desiredRotation = Quaternion.LookRotation(lookAtPos - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSpeed * Time.smoothDeltaTime);
    }
}
