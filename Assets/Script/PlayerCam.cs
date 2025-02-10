using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float distance = 10f;
    [SerializeField] private float height = 3f;
    [SerializeField] private float followSpeed = 20f;
    [SerializeField] private float rotationSpeed = 20f;

    private void Awake()
    {
        if (target == null)
            target = GameObject.Find("Player").transform;
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
