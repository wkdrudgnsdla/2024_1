using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class RankingManager : MonoBehaviour
{
    public GameManager GM;
    public ScoreUI SUI;  // ScoreUI 스크립트에는 InputField(예: playerNameInput)가 있어야 합니다.

    //-------------랭킹 이름 UI-------------
    public Text OnestRanking;
    public Text TwostRanking;
    public Text ThreestRanking;
    public Text FourstRandking;
    public Text FivestRandking;

    //-------------랭킹 점수 UI-------------
    public Text OnestRankingScore;
    public Text TwostRankingScore;
    public Text ThreestRankingScore;
    public Text FourstRandkingScore;
    public Text FivestRandkingScore;

    // 랭킹 정보를 저장할 자료구조
    [System.Serializable]
    public class RankingEntry
    {
        public string playerName;
        public int score;

        public RankingEntry(string name, int score)
        {
            // 이름이 비어있으면 "???"로 대체합니다.
            this.playerName = string.IsNullOrWhiteSpace(name) ? "???" : name;
            this.score = score;
        }
    }

    // 최대 5개의 랭킹을 표시하기 위한 리스트
    public List<RankingEntry> rankingList = new List<RankingEntry>();

    public void Awake()
    {
        GM = gameObject.GetComponent<GameManager>();
        SUI = gameObject.GetComponent<ScoreUI>();

        // UI 오브젝트 찾기 (Hierarchy에 있는 GameObject 이름과 일치해야 합니다.)
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

    // 게임이 끝났을 때 호출하여 현재 판의 점수와 입력한 이름을 랭킹에 추가합니다.
    public void AddCurrentScoreToRanking()
    {
        // ScoreUI의 InputField에서 플레이어 이름을 가져옵니다.
        string playerName = SUI.PlayerNameInput.text;
        // 이름이 비어있다면 "???"로 설정합니다.
        if (string.IsNullOrWhiteSpace(playerName))
        {
            playerName = "???";
        }
        int resultScore = (int)GM.Resultscore;  // GameManager에서 현재 판의 점수를 가져온다고 가정합니다.

        // 새 랭킹 엔트리 생성 후 리스트에 추가
        RankingEntry newEntry = new RankingEntry(playerName, resultScore);
        rankingList.Add(newEntry);

        // 점수 내림차순 정렬 (높은 점수가 위로 오도록)
        rankingList.Sort((entry1, entry2) => entry2.score.CompareTo(entry1.score));

        // 랭킹 UI 업데이트
        UpdateRankingUI();
    }

    // 랭킹 리스트의 내용을 각 UI Text에 반영하는 함수
    public void UpdateRankingUI()
    {
        // 먼저 모든 UI 텍스트를 빈 문자열로 초기화합니다.
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

        // 랭킹이 존재하는 경우에만 해당 Text에 값을 할당합니다.
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
