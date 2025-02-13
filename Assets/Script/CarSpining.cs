using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpining : MonoBehaviour
{
    public float SpinSpeed = 10;

    private void Update()
    {
        transform.Rotate(0, Time.deltaTime * SpinSpeed, 0, Space.Self);
    }
}
