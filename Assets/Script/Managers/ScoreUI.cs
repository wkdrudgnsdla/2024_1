using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public GameManager GM;

    public Text StageScore;
    public Text TimeScore;
    public Text ResultScore;
    public Text StageCash;

    public void Awake()
    {
        GM = gameObject.GetComponent<GameManager>();
        StageScore = GameObject.Find("StageScoreText").GetComponent<Text>();
        TimeScore = GameObject.Find("TimeScoreText").GetComponent <Text>();
        ResultScore = GameObject.Find("ResultScoreText").GetComponent<Text>();
        StageCash = GameObject.Find("StageCash").GetComponent<Text>();
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
        //다음스테이지 포지션,로테이션
        GM.StageClear.SetActive(false);
    }
}
