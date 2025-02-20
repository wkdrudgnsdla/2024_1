using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class GameManager : MonoBehaviour
{
    public PlayerCam cam;
    public Camera PCam;
    public PlayerMove player;
    MultiTerrainChecker MTC;
    public GameObject MainMenu;
    public GameObject UI;
    public GameObject StageClear;

    public float sec { get;set;}
    public float StartCountdown;

    public int SCount;
    public int MCount;

    public bool startRace;
    public bool OutTrack;
    public bool Finish;
    public bool inStage;
    public bool isMainMenu;
    public bool isDTires;
    public bool isFTires;
    public bool isCTires;
    public bool Upgrading;

    public bool UIable;

    public bool STBost;

    public double cash;
    public float Money;

    public double StageScore;
    public double TimerScore;
    public double Resultscore;

    public void Awake()
    {
        cam = GameObject.Find("PlayerCam").GetComponent<PlayerCam>();
        PCam = cam.GetComponent<Camera>();
        player = GameObject.Find("Player").GetComponent<PlayerMove>();
        MTC = player.GetComponent<MultiTerrainChecker>();
        MainMenu = GameObject.Find("MainMenu");
        UI = GameObject.Find("UI");
        StageClear = GameObject.Find("StageClear");
    }

    public void Start()
    {
        StartCountdown = 4;
        OutTrack = false;
        Finish = false;
        inStage = false;
        isMainMenu = true;
        Upgrading = false;  
        UIable = true;
        cash = 0;
        STBost = false;

        Money = 0;
        TimerScore = 0;
        Resultscore = 0;
        StageScore = 0; 
    }

    public void Update()
    {
        if(MainMenu.active == true)
        {
            isMainMenu = true;
            UI.SetActive(false);
        }
        else if(MainMenu.active == false && UIable == true)
        {
            isMainMenu = false;
            UI.SetActive(true);
        }

        outTrack();

        GameCheck();
    }

    public void StartGame()
    {
        player.rb.velocity = Vector3.zero;
        player.rb.angularVelocity = Vector3.zero;
        player.rb.interpolation = RigidbodyInterpolation.None;

        StageScore = 0;
        TimerScore = 0;
        Resultscore = 0;

        sec = 0;
        MCount = 0;
        SCount = 0;
        cash = 0;

        startRace = false;
        StartCountdown = 4;
        OutTrack = false;
        Finish = false;
        STBost = false;

        player.moveSpeed = 6f;
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

    public void outTrack()
    {
        if (OutTrack)
        {
            if (MTC.terrainUnderneath.gameObject.CompareTag("Desert"))
            {
                if (!isDTires)
                {
                    player.moveSpeed = 4;
                    PCam.fieldOfView = Mathf.Lerp(PCam.fieldOfView, 40f, Time.deltaTime);
                }
            }
            else if (MTC.terrainUnderneath.gameObject.CompareTag("Forest"))
            {
                if (!isFTires)
                {
                    player.moveSpeed = 4;
                    PCam.fieldOfView = Mathf.Lerp(PCam.fieldOfView, 40f, Time.deltaTime);
                }
            }
            else if (MTC.terrainUnderneath.gameObject.CompareTag("City"))
            {
                if (!isFTires)
                {
                    player.moveSpeed = 4;
                    PCam.fieldOfView = Mathf.Lerp(PCam.fieldOfView, 40f, Time.deltaTime);
                }
            }
        }
        else
        {
            player.moveSpeed = player.SetSpeed;
            PCam.fieldOfView = Mathf.Lerp(PCam.fieldOfView, 60f, Time.deltaTime);
        }
    }

    public void GameCheck()
    {
        if (!isMainMenu)
        {
            inStage = true;
            StartCountdown -= Time.deltaTime;

            if (StartCountdown <= 1 && !Finish)
            {
                startRace = true;
                if (!STBost)
                {
                    player.rb.AddForce(Vector3.right * 100, ForceMode.Impulse);
                    STBost = true;
                }
            }


            UnityEngine.Debug.Log(MTC.layerIndex);

            if (MTC.layerIndex == 1 || MTC.layerIndex == 3)
            {
                OutTrack = false;
            }
            else
            {
                OutTrack = true;
            }

            if (startRace && !Upgrading)
            {
                player.rb.interpolation = RigidbodyInterpolation.Interpolate;
                player.moveable = true;
                sec += Time.deltaTime;

                cam.followSpeed = 20;
                cam.rotationSpeed = 20;
            }
            else if (!Upgrading)
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
        else if (isMainMenu)
        {
            inStage = false;
            StartCountdown = 4;
            OutTrack = false;
            Finish = false;
        }

        if (Finish)
        {
            cam.followSpeed = 1;
            cam.rotationSpeed = 1;
        }
    }
}
