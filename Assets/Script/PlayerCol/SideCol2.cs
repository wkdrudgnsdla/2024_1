using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SideCol2 : MonoBehaviour
{
    public GameManager GM;
    public float downSpeed = 3f;

    public void Awake()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Forest") || collision.gameObject.CompareTag("Desert") || collision.gameObject.CompareTag("City") || collision.gameObject.CompareTag("Item"))
        {
            return;
        }
        else
        {
            GM.player.rb.velocity = GM.player.rb.velocity.normalized * (GM.player.rb.velocity.magnitude - downSpeed);
        }
    }
}
