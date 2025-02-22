using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public RankingManager RM;
    public GameManager GM;
    public UpgradeManager UM;
    public ItemUIManager IUM;
    public InputField PlayerNameInput;
    public GameObject InputField;

    public GameObject Upgrade;

    public Text StageScore;
    public Text TimeScore;
    public Text ResultScore;
    public Text StageCash;
    public Text Result;
    public Text NextStage;
    public GameObject UpgradeButton;

    public bool not1st;

    public void Awake()
    {
        UM = GameObject.Find("GameManager").GetComponent<UpgradeManager>();
        RM = GameObject.Find("GameManager").GetComponent<RankingManager>();
        PlayerNameInput = GameObject.Find("PlayerNameInput").GetComponent<InputField>();
        InputField = GameObject.Find("PlayerNameInput");
        UpgradeButton = GameObject.Find("UpgradeButton");
        Upgrade = GameObject.Find("Upgrade");
        GM = gameObject.GetComponent<GameManager>();
        IUM = gameObject.GetComponent<ItemUIManager>();
        StageScore = GameObject.Find("StageScoreText").GetComponent<Text>();
        TimeScore = GameObject.Find("TimeScoreText").GetComponent <Text>();
        ResultScore = GameObject.Find("ResultScoreText").GetComponent<Text>();
        StageCash = GameObject.Find("StageCash").GetComponent<Text>();
        Result = GameObject.Find("Result").GetComponent<Text>();
        NextStage = GameObject.Find("NextStage").GetComponent<Text>();
    }

    public void Start()
    {
        Upgrade.SetActive(false);
        not1st = false;
        InputField.SetActive(false);
    }

    public void Update()
    {
        if(GM.GameAllClear)
        {
            InputField.SetActive(true);
        }


        GM.Resultscore = GM.StageScore + GM.TimerScore + GM.BeforRoundScore;



        ResultScore.text = "총 점수 : " + GM.Resultscore.ToString() + "점";

        StageScore.text = "스테이지 점수    " + GM.StageScore.ToString() + "점";

        TimeScore.text = "타이머 점수    " + GM.TimerScore.ToString() + "점";

        double cash = GM.Money / 10000;
        StageCash.text = "획득 상금    " + cash.ToString() + "만원";

        if (GM.GameAllClear)
        {
            UpgradeButton.SetActive(false);
            Result.text = "희망의 도시 티켓 획득";
            Result.fontSize = 180;
            Result.color = new Color(255, 214, 0);
            NextStage.text = "메뉴화면으로";
        }
        else if(!GM.GameAllClear && GM.lose)
        {
            Result.text = "패배";
            Result.fontSize = 200;
            Result.color = new Color(171, 171, 171);
            NextStage.text = "다시 플레이";
        }
        else if (!GM.GameAllClear && !GM.lose)
        {
            Result.text = "1등";
            Result.fontSize = 200;
            GM.SetCash = GM.cash;
            Result.color = new Color(255, 214, 0);
            NextStage.text = "다음 스테이지";
        }

    }

public void OnClickNextStage()
    {
        GM.Money = 0;

        if (GM.GameAllClear)
        {
            GM.PCam.transform.position = new Vector3(843f, 1029f, 275f);
            GM.PCam.transform.rotation = Quaternion.Euler(90, 0, 0);
            GM.cam.followSpeed = 0;
            GM.cam.rotationSpeed = 0;
            GM.UI.SetActive(false);
            GM.MainMenu.SetActive(true);
            UM.ResetUpGrade();
            RM.AddCurrentScoreToRanking();
            GM.StageClear.SetActive(false);
        }
        else if (!GM.GameAllClear && !GM.lose)
        {
            if (GM._StageLevel == 1)
            {
                GM.Stage1Enemy.SetActive(false);
            }
            GM._StageLevel += 1;
            GM.NextGame();
            GM.StageClear.SetActive(false);

        }
        else if (!GM.GameAllClear && GM.lose)
        {
            Destroy(GM._Stage1Items);
            Destroy(GM._Stage2Items);
            Destroy(GM._Stage3Items);
            GM.StartGame();
            GM.StageClear.SetActive(false);
        }
        
    }

    public void OnClickUpgrade()
    {
        GM.cam.followSpeed = 0;
        GM.cam.rotationSpeed = 0;
        Time.timeScale = 0.001f;

        GM.PCam.transform.position = new Vector3(-214.2403f, -131.58f, 247.84f);
        GM.PCam.transform.rotation = Quaternion.Euler(15, 0, 0);

        GM.UI.SetActive(false);
        GM.UIable = false;
        GM.Upgrading = true;
        Upgrade.SetActive(true);
        GM.StageClear.SetActive(false);
    }
}
