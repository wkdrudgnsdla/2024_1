using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float rotationSpeed = 5.0f;

    private void Awake()
    {
        target = GameObject.Find("Player").GetComponent<Transform>();
    }

    private void LateUpdate()
    {
        Vector3 direction = target.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
