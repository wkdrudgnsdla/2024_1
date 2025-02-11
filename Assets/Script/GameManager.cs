using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    PlayerCam cam;
    PlayerMove player;

    public float sec;

    public int SCount;
    public int MCount;
    public float StartCountdown;

    public bool startRace;

    public void Awake()
    {
        cam = GameObject.Find("PlayerCam").GetComponent<PlayerCam>();
        player = GameObject.Find("Player").GetComponent<PlayerMove>();
    }

    public void Start()
    {
        StartCountdown = 4;
    }

    public void Update()
    {
        StartCountdown -= Time.deltaTime;

        if(StartCountdown <= 0)
        {
            startRace = true;
        }

        if(startRace)
        {
            player.moveable = true;
            sec += Time.deltaTime;

            cam.followSpeed = 20;
            cam.rotationSpeed = 20;
        }
        else
        {
            player.moveable = false;

            cam.followSpeed = 3;
            cam.rotationSpeed = 4;
        }

        SCount = (int)sec;

        if (SCount >= 60)
        {
            sec = 0;
            SCount = 0;
            MCount += 1;
        }
    }
}
