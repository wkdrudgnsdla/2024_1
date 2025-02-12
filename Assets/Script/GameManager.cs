using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    PlayerCam cam;
    Camera PCam;
    public PlayerMove player;
    MultiTerrainChecker MTC;

    public float sec { get;set;}
    public float StartCountdown;

    public int SCount;
    public int MCount;

    public bool startRace;
    public bool OutTrack;
    public bool Finish;

    public void Awake()
    {
        cam = GameObject.Find("PlayerCam").GetComponent<PlayerCam>();
        PCam = cam.GetComponent<Camera>();
        player = GameObject.Find("Player").GetComponent<PlayerMove>();
        MTC = player.GetComponent<MultiTerrainChecker>();
    }

    public void Start()
    {
        StartCountdown = 4;
        OutTrack = false;
        Finish = false;
    }

    public void Update()
    {
        StartCountdown -= Time.deltaTime;

        if(StartCountdown <= 0 && !Finish)
        {
            startRace = true;
        }

        if(OutTrack)
        {
            player.moveSpeed = 5;
            PCam.fieldOfView = Mathf.Lerp(PCam.fieldOfView, 40f, Time.deltaTime);
        }
        else
        {
            player.moveSpeed = 10;
            PCam.fieldOfView = Mathf.Lerp(PCam.fieldOfView, 60f, Time.deltaTime);
        }

        if(MTC.layerIndex == 1 || MTC.layerIndex == 3)
        {
            OutTrack = false;
        }
        else
        {
            OutTrack = true;
        }

        if(startRace)
        {
            player.rb.interpolation = RigidbodyInterpolation.Interpolate;
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

    public void StartGame()
    {
        player.rb.velocity = Vector3.zero;
        player.rb.angularVelocity = Vector3.zero;
        player.rb.interpolation = RigidbodyInterpolation.None;

        sec = 0;
        MCount = 0;
        SCount = 0;

        startRace = false;
        StartCountdown = 4;
        OutTrack = false;
        Finish = false;

        player.moveSpeed = 10f;
        player.turnSpeed = 1f;
        player.brakeForce = 10f;
        player.turnDamping = 1f;
        player.extraGravity = 5f;
        player.currentSpeed = 0;
        player.moveable = false;
        player.transform.position = new Vector3(74.4f, 2.5f, 5.5f);
        player.transform.rotation = Quaternion.Euler(0, 0, 0);


        cam.transform.position = new Vector3(843, 1029, 275);
        cam.followSpeed = 1;
        cam.rotationSpeed = 2;
    }
}
