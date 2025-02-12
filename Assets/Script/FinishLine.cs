using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public GameManager GM;

    public void Awake()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        GM.Finish = true;
        GM.startRace = false;
        GM.player.rb.velocity = Vector3.Lerp(GM.player.rb.velocity, Vector3.zero, Time.deltaTime * 30);
        GM.player.rb.angularVelocity = Vector3.Lerp(GM.player.rb.angularVelocity, Vector3.zero, Time.deltaTime * 30);
    }
}
