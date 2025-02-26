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

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Destroy());
        }
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
