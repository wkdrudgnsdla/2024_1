using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public PlayerCam cam;
    public Camera PCam;
    public PlayerMove player;
    public GameObject Stage1Enemy;
    public GameObject Stage2Enemy;

    MultiTerrainChecker MTC;
    PaintDetailChecker PDC;
    public GameObject MainMenu;
    public GameObject UI;
    public GameObject StageClear;

    public GameObject Stage1Items;
    public GameObject _Stage1Items;
    public GameObject Stage2Items;
    public GameObject _Stage2Items;

    public float sec { get;set;}
    public float StartCountdown;

    public int SCount;
    public int MCount;

    [SerializeField]
    private int StageLevel;
    public int _StageLevel
    {
        get
        {
            return StageLevel;
        }
        set
        {
            if(StageLevel >= 1 && StageLevel <= 3)
            {
                StageLevel = value;
            }
        }

    }

    public bool startRace;
    public bool OutTrack;
    public bool Finish;
    public bool inStage;
    public bool isMainMenu;
    public bool isDTires;
    public bool isFTires;
    public bool isCTires;
    public bool Upgrading;
    public bool ReSpawnItem1;
    public bool ReSpawnItem2;

    public bool UIable;

    public bool STBost;

    public bool lose;

    public bool UpScore;

    public double cash;
    public double Money;

    public bool GameAllClear;//이거 3번째 엔드포인트에서 활성화

    public double StageScore;
    public double TimerScore;
    public double Resultscore;
    public double BeforRoundScore;

    public void Awake()
    {
        Stage1Enemy = GameObject.Find("Stage1Enemy");
        Stage2Enemy = GameObject.Find("Stage2Enemy");

        cam = GameObject.Find("PlayerCam").GetComponent<PlayerCam>();
        PCam = cam.GetComponent<Camera>();
        player = GameObject.Find("Player").GetComponent<PlayerMove>();
        PDC = player.GetComponent<PaintDetailChecker>();
        MTC = player.GetComponent<MultiTerrainChecker>();
        MainMenu = GameObject.Find("MainMenu");
        UI = GameObject.Find("UI");
        StageClear = GameObject.Find("StageClear");

        Stage1Items = Resources.Load("Stage1Items") as GameObject;
        Stage2Items = Resources.Load("Stage2Items") as GameObject;

        _Stage1Items = GameObject.Find("Stage1Items");
        _Stage2Items = GameObject.Find("Stage2Items");
    }

    public void Start()
    {
        GameAllClear = false;
        StartCountdown = 4;
        OutTrack = false;
        Finish = false;
        inStage = false;
        isMainMenu = true;
        Upgrading = false;  
        UIable = true;
        cash = 0;
        STBost = false;
        ReSpawnItem1 = false;
        ReSpawnItem2 = false;
        UpScore = false; 
        lose = false;

        TimerScore = 0;
        Resultscore = 0;
        StageScore = 0;
        BeforRoundScore = 0;
        Money = 0;
        StageLevel = 1;
    }

    public void Update()
    {
        if(_Stage1Items == null)
        {
            _Stage1Items = GameObject.Find("Stage1Items");
        }
        if(_Stage2Items == null)
        {
            _Stage2Items = GameObject.Find("Stage2Items");
        }

        if (MainMenu.active == true)
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

        if(_Stage1Items == null)
        {
            StartCoroutine(Stage1Item());
        }

        if(_Stage2Items == null)
        {
            StartCoroutine(Stage2Item());
        }


        if (StageLevel != 1)
        {
            Stage1Enemy.SetActive(false);
        }
        else if(StageLevel == 1)
        {
            Stage1Enemy.SetActive(true);
        }

        if(StageLevel != 2)
        {
            Stage2Enemy.SetActive(false);
        }
        else if(StageLevel == 2)
        {
            Stage2Enemy.SetActive(true);
        }
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        GameAllClear = false ;

        player.rb.velocity = Vector3.zero;
        player.rb.angularVelocity = Vector3.zero;
        player.rb.interpolation = RigidbodyInterpolation.None;

        StageScore = 0;
        TimerScore = 0;
        Resultscore = 0;
        BeforRoundScore = 0;

        sec = 0;
        MCount = 0;
        SCount = 0;
        cash = 0;

        UpScore = false;
        startRace = false;
        StartCountdown = 4;
        OutTrack = false;
        Finish = false;
        STBost = false;
        ReSpawnItem1 = false;
        ReSpawnItem2 = false;
        lose = false;

        player.moveSpeed = 6f;
        player.turnSpeed = 1f;
        player.brakeForce = 10f;
        player.turnDamping = 1f;
        player.extraGravity = 5f;
        player.currentSpeed = 0;
        player.moveable = false;
        if (StageLevel == 1)
        {
            //---------------플레이어-----------
            player.transform.position = new Vector3(164.3f, 2.5f, 5.5f);
            player.transform.rotation = Quaternion.Euler(0, 0, 0);


            cam.transform.position = new Vector3(843, 1029, 275);
            cam.transform.rotation = Quaternion.Euler(90, 0, 0);

            //--------------적--------------
            Stage1Enemy.transform.position = new Vector3(164.3f, 2.5f, -9.271066f);
            Stage1Enemy.transform.rotation = Quaternion.Euler(0,0,0);
        }

        if (StageLevel == 2)
        {
            //------------------플레이어-----------
            player.transform.position = new Vector3(1164.129f, -354.97f, -3000.2f);
            player.transform.rotation = Quaternion.Euler(0, 0, 0);

            cam.transform.position = new Vector3(1944.06f, 1374.4f, -2336.2f);
            cam.transform.rotation = Quaternion.Euler(90, 0, 0);
            cam.followSpeed = 1;
            cam.rotationSpeed = 2;

            //--------------적--------------
            Stage2Enemy.transform.position = new Vector3(1164.129f, -354.97f, -3031.31f);
            Stage2Enemy.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void NextGame()
    {
        player.rb.velocity = Vector3.zero;
        player.rb.angularVelocity = Vector3.zero;
        player.rb.interpolation = RigidbodyInterpolation.None;

        StageScore = 0;
        TimerScore = 0;

        Time.timeScale = 1;

        sec = 0;
        MCount = 0;
        SCount = 0;

        BeforRoundScore = Resultscore;
        startRace = false;
        StartCountdown = 4;
        OutTrack = false;
        Finish = false;
        STBost = false;
        UpScore = false;
        ReSpawnItem1 = false;
        ReSpawnItem2 = false;
        lose = false;

        player.extraGravity = 5f;
        player.currentSpeed = 0;
        player.moveable = false;

        if(StageLevel == 1)
        {
            player.transform.position = new Vector3(164.3f, 2.5f, 5.5f);
            player.transform.rotation = Quaternion.Euler(0, 0, 0);


            cam.transform.position = new Vector3(843, 1029, 275);
            cam.transform.rotation = Quaternion.Euler(90, 0, 0);
        }

        if (StageLevel == 2)
        {
            player.transform.position = new Vector3(1164.129f, -354.97f, -3000.2f);
            player.transform.rotation = Quaternion.Euler(0, 0, 0);

            cam.transform.position = new Vector3(1944.06f, 1374.4f, -2336.2f);
            cam.transform.rotation = Quaternion.Euler(90, 0 ,0);
            cam.followSpeed = 1;
            cam.rotationSpeed = 2;
        }
       

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
                    player.rb.AddForce(Vector3.right * 80, ForceMode.Impulse);
                    STBost = true;
                }
            }


            //UnityEngine.Debug.Log(MTC.layerIndex);

            if(PDC.isOnPaintDetail)
            {
                OutTrack = true;
            }
            else if (MTC.layerIndex == 1 || MTC.layerIndex == 3 || MTC.layerIndex == 5 || MTC.layerIndex == 0)
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
    }

    IEnumerator Stage1Item()
    {
        if(!ReSpawnItem1)
        {
            GameObject Item = MonoBehaviour.Instantiate(Stage1Items);
            Item.name = "Stage1Items";
            Vector3 pos = new Vector3(1481.204f, 228.1082f, 238.6916f);
            Item.transform.position = pos;
        }

        ReSpawnItem1 = true;
        yield return null;
    }
    
    IEnumerator Stage2Item()
    {
        if(!ReSpawnItem2)
        {
            GameObject Item = MonoBehaviour.Instantiate(Stage2Items);
            Item.name = "Stage2Items";
            Vector3 pos = new Vector3(1737.29f, -350.8989f, -3022.426f);
            Item.transform.position = pos;
        }

        ReSpawnItem2 = true;
        yield return null;
    }
}
