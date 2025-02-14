using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpUI : MonoBehaviour
{
    public Text HelpText;
    public Text HelpTitle;

    public Button NextPageButton;
    public Button BeforePageButton;

    public GameObject Help;

    [SerializeField]
    private int _pagenum;

    public int pagenum
    {
        get
        {
            return _pagenum;
        }
        set
        {
            if (value > 0 && value <= 3)
            {
                _pagenum = value;
            }
        }
    }


    public void Awake()
    {
        HelpText = GameObject.Find("HelpText").GetComponent<Text>();
        NextPageButton = GameObject.Find("NextPageButton").GetComponent<Button>();
        BeforePageButton = GameObject.Find("BeforePageButton").GetComponent <Button>();
        HelpTitle = GameObject.Find("HelpTitle").GetComponent<Text>();
        Help = GameObject.Find("Help");
    }

    public void Start()
    {
        _pagenum = 1;
        Help.SetActive(false);
    }

    public void Update()
    {
        if (_pagenum == 1)
        {
            HelpTitle.text = "스토리 & 게임방법";
            HelpText.text = "Racer은 스테이지를 클리어 해가며 상금을 획득하고\r\n" +
                "부품을 구매하여 최종 스테이지에서 승리해서\r\n" +
                "희망의 도시 티켓을 얻는 것이 목표인 게임입니다.\r\n" +
                "\r\n스테이지 내에 존재하는 다양한 방해요소를 피하고 아이템과\r\n" +
                "부스트 발판을 이용하여 적 차량보다 먼저 결승 지점을 통과하여  희망의 도시 티켓을 획득하세요!";
        }
        else if(_pagenum == 2)
        {
            HelpTitle.text = "조작키";
            HelpText.text = "이동- W,A,S,D or 방향키\r\n브레이크 - 스페이스바\r\n\r\n아이템 치트키 - F1               상점 무료 치트키 - F2\r\n스테이지 재시작- F3            다음 스테이지 - F4\r\n일시정지 - F5                       메뉴화면 - F6";
        }
        else if(pagenum == 3)
        {
            HelpTitle.text = "아이템 및 부품";
            HelpText.text = "아이템 종류\r\n- 100, 500, 1000만원 획득\r\n- 소폭 부스트, 대폭 부스트\r\n- 상점 이동\r\n\r\n상점 부품 종류\r\n- 각 스테이지 전용 타이어\r\n- 6기통 엔진, 8기통 엔진\r\n-핸들 기어 강화";
        }
    }

    public void OnClickNextPageButton()
    {
        _pagenum += 1;
    }

    public void OnClickBeforePageButton()
    {
        _pagenum -= 1;
    }


    public void OnClickBackButton()
    {
        Help.SetActive(false);
    }

}
