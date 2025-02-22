using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideCol : MonoBehaviour
{
    public GameManager GM;
    public CrashUIManager CUIM;
    public float downSpeed = 24f;

    public void Awake()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        CUIM = GameObject.Find("GameManager").GetComponent<CrashUIManager>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Forest") || collision.gameObject.CompareTag("Desert") || collision.gameObject.CompareTag("City") || collision.gameObject.CompareTag("Item"))
        {
            return;
        }
        else
        {
            GM.player.rb.velocity = GM.player.rb.velocity.normalized * (GM.player.rb.velocity.magnitude - downSpeed);
            CUIM.isside = true;
        }
    }
}
