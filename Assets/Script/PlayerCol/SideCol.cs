using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideCol : MonoBehaviour
{
    public GameManager GM;
    public float downSpeed = 3f;

    public void Awake()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        GM.player.rb.velocity = GM.player.rb.velocity.normalized * (GM.player.rb.velocity.magnitude - downSpeed);
    }
}
