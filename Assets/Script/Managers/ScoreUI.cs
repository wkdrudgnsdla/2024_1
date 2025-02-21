using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public GameManager GM;
    public ItemUIManager IUM;
    public InputField PlayerNameInput;
    public GameObject InputField;

    public Text StageScore;
    public Text TimeScore;
    public Text ResultScore;
    public Text StageCash;
    public Text Result;
    public Text NextStage;
    public GameObject Upgrade;

    public bool not1st;

    public void Awake()
    {
        PlayerNameInput = GameObject.Find("PlayerNameInput").GetComponent<InputField>();
        InputField = GameObject.Find("PlayerNameInput");
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

        if (not1st)
        {
            Result.text = "2등";
            Result.color = new Color(171, 171, 171);
            NextStage.text = "다시 플레이";
        }
        else if (!not1st)
        {
            Result.text = "1등";
            Result.color = new Color(255, 214, 0);
            NextStage.text = "다음 스테이지";
        }
        else if(GM.GameAllClear)
        {
            NextStage.text = "메뉴화면으로";
        }
}

public void OnClickNextStage()
    {
        GM.Money = 0;

        if (!not1st)
        {
            GM._StageLevel += 1;
            GM.NextGame();
            GM.StageClear.SetActive(false);
        }
        else if (not1st)
        {
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
