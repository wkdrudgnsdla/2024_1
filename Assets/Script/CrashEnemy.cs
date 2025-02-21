using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashEnemy : MonoBehaviour
{
    public float moveSpeed = 25f;

    public void Update()
    {
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
    }
}
