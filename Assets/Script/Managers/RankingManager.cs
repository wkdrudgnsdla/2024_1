using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class RankingManager : MonoBehaviour
{
    public GameManager GM;
    public ScoreUI SUI;

    //-------------·©Å· ÀÌ¸§ UI-------------
    public Text OnestRanking;
    public Text TwostRanking;
    public Text ThreestRanking;
    public Text FourstRandking;
    public Text FivestRandking;

    //-------------·©Å· Á¡¼ö UI-------------
    public Text OnestRankingScore;
    public Text TwostRankingScore;
    public Text ThreestRankingScore;
    public Text FourstRandkingScore;
    public Text FivestRandkingScore;


    public List<RankingEntry> rankingList = new List<RankingEntry>();

    [System.Serializable]
    public class RankingEntry
    {
        public string playerName;
        public int score;

        public RankingEntry(string name, int score)
        {
            this.playerName = string.IsNullOrWhiteSpace(name) ? "???" : name;
            this.score = score;
        }
    }


    public void Awake()
    {
        GM = gameObject.GetComponent<GameManager>();
        SUI = gameObject.GetComponent<ScoreUI>();

        OnestRanking = GameObject.Find("1stRanking").GetComponent<Text>();
        TwostRanking = GameObject.Find("2stRanking").GetComponent<Text>();
        ThreestRanking = GameObject.Find("3stRanking").GetComponent<Text>();
        FourstRandking = GameObject.Find("4stRanking").GetComponent<Text>();
        FivestRandking = GameObject.Find("5stRanking").GetComponent<Text>();

        OnestRankingScore = GameObject.Find("1stRankingScore").GetComponent<Text>();
        TwostRankingScore = GameObject.Find("2stRankingScore").GetComponent<Text>();
        ThreestRankingScore = GameObject.Find("3stRankingScore").GetComponent<Text>();
        FourstRandkingScore = GameObject.Find("4stRankingScore").GetComponent<Text>();
        FivestRandkingScore = GameObject.Find("5stRankingScore").GetComponent<Text>();
    }

    public void AddCurrentScoreToRanking()
    {
        string playerName = SUI.PlayerNameInput.text;
        if (string.IsNullOrWhiteSpace(playerName))
        {
            playerName = "???";
        }
        int resultScore = (int)GM.Resultscore;

        RankingEntry newEntry = new RankingEntry(playerName, resultScore);
        rankingList.Add(newEntry);

        rankingList.Sort((entry1, entry2) => entry2.score.CompareTo(entry1.score));

        UpdateRankingUI();
    }

    public void UpdateRankingUI()
    {
        OnestRanking.text = "";
        OnestRankingScore.text = "";
        TwostRanking.text = "";
        TwostRankingScore.text = "";
        ThreestRanking.text = "";
        ThreestRankingScore.text = "";
        FourstRandking.text = "";
        FourstRandkingScore.text = "";
        FivestRandking.text = "";
        FivestRandkingScore.text = "";

        if (rankingList.Count >= 1)
        {
            OnestRanking.text = rankingList[0].playerName;
            OnestRankingScore.text = rankingList[0].score.ToString();
        }
        if (rankingList.Count >= 2)
        {
            TwostRanking.text = rankingList[1].playerName;
            TwostRankingScore.text = rankingList[1].score.ToString();
        }
        if (rankingList.Count >= 3)
        {
            ThreestRanking.text = rankingList[2].playerName;
            ThreestRankingScore.text = rankingList[2].score.ToString();
        }
        if (rankingList.Count >= 4)
        {
            FourstRandking.text = rankingList[3].playerName;
            FourstRandkingScore.text = rankingList[3].score.ToString();
        }
        if (rankingList.Count >= 5)
        {
            FivestRandking.text = rankingList[4].playerName;
            FivestRandkingScore.text = rankingList[4].score.ToString();
        }
    }
}
