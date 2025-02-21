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

    public Text StageScore;
    public Text TimeScore;
    public Text ResultScore;
    public Text StageCash;
    public GameObject Upgrade;

    public void Awake()
    {
        Upgrade = GameObject.Find("Upgrade");
        GM = gameObject.GetComponent<GameManager>();
        IUM = gameObject.GetComponent<ItemUIManager>();
        StageScore = GameObject.Find("StageScoreText").GetComponent<Text>();
        TimeScore = GameObject.Find("TimeScoreText").GetComponent <Text>();
        ResultScore = GameObject.Find("ResultScoreText").GetComponent<Text>();
        StageCash = GameObject.Find("StageCash").GetComponent<Text>();
    }

    public void Start()
    {
        Upgrade.SetActive(false);
    }

    public void Update()
    {
        GM.Resultscore = GM.StageScore + GM.TimerScore;

        ResultScore.text = "총 점수 : " + GM.Resultscore.ToString() + "점";

        StageScore.text = "스테이지 점수    " + GM.StageScore.ToString() + "점";

        TimeScore.text = "타이머 점수    " + GM.TimerScore.ToString() + "점";

        double cash = GM.Money / 10000;
        StageCash.text = "획득 상금    " + cash.ToString() + "만원";
    }

    public void OnClickNextStage()
    {
        GM._StageLevel += 1;
        GM.NextGame();
        //다음스테이지 포지션,로테이션
        GM.StageClear.SetActive(false);
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
        GM.Money = 0;
    }
}
