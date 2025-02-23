using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public GameManager GM;

    public GameObject Upgrade;
    public Text CoinText;

    public GameObject SOCT;
    public GameObject SODT;
    public GameObject SOFT;
    public GameObject SOH;
    public GameObject SO6E;
    public GameObject SO8E;

    public Text DT;
    public Text FT;
    public Text CT;
    public Text H;
    public Text SIXE;
    public Text EIGHT;

    public double DTCost;
    public double FTCost;
    public double CTCost;
    public double HCost;
    public double sixECost;
    public double EightECost;

    public bool buyCTire;
    public bool buyDTire;
    public bool buyFTire;

    public bool Engine8have;

    public void Awake()
    {
        GM = gameObject.GetComponent<GameManager>();
        Upgrade = GameObject.Find("Upgrade");
        CoinText = GameObject.Find("CoinText").GetComponent<Text>();

        SODT = GameObject.Find("SoldOutImageDesertTire");
        SOCT = GameObject.Find("SoldOutImageCityTire");
        SOFT = GameObject.Find("SoldOutImageForestTire");
        SOH = GameObject.Find("SoldOutImageHandle");
        SO6E = GameObject.Find("SoldOutImage6Engine");
        SO8E = GameObject.Find("SoldOutImage8Engine");

        DT = GameObject.Find("DT").GetComponent <Text>();
        FT = GameObject.Find("FT").GetComponent <Text>();
        CT = GameObject.Find("CT").GetComponent <Text>();
        H = GameObject.Find("H").GetComponent <Text>();
        SIXE = GameObject.Find("SIXE").GetComponent <Text>();
        EIGHT = GameObject.Find("EIGHT").GetComponent <Text>();
    }

    public void Start()
    {
        DTCost = 5000000;
        FTCost = 10000000;
        CTCost = 15000000;
        HCost = 15000000;
        sixECost = 10000000;
        EightECost = 20000000;

        Engine8have = false;

        SODT.SetActive(false);
        SOCT.SetActive(false);
        SOFT.SetActive(false);
        SOH.SetActive(false);
        SO6E.SetActive(false);
        SO8E.SetActive(false);
    }

    public void Update()
    {
        int _coin = (int)GM.cash / 10000;
        CoinText.text = _coin.ToString() + "만원";

        double DTC = DTCost / 10000;
        double FTC = FTCost / 10000;
        double CTC = CTCost / 10000;
        double HC = HCost / 10000;
        double SEC = sixECost / 10000;
        double EEC = EightECost / 10000;

        DT.text = "사막 전용 타이어\r\n" + DTC.ToString() + "만원";
        FT.text = "산악 전용 타이어\r\n" + FTC.ToString() + "만원";
        CT.text = "도심 전용 타이어\r\n" + CTC.ToString() + "만원";
        H.text = "핸들 강화\r\n" + HC.ToString() + "만원";
        SIXE.text = "6기통 엔진\r\n" + SEC.ToString() + "만원";
        EIGHT.text = "8기통 엔진\r\n" + EEC.ToString() + "만원";
    }

    public void OnCliCkDesert()
    {
        if(GM.cash < DTCost)
        {
            return;
        }

        if (!buyDTire)
        {
            GM.cash -= DTCost;
        }
        buyDTire = true;
        GM.isDTires = true;
        SODT.SetActive(true);
    }

    public void OnClickForest()
    {
        if(GM.cash < FTCost)
        {
            return;
        }

        if(!buyFTire)
        {
            GM.cash -= FTCost;
        }
        buyFTire = true;
        GM.isFTires = true;


        SOFT.SetActive(true);
    }

    public void OnCliCkCTires()
    {
        if(GM.cash < CTCost)
        {
            return;
        }

        if (!buyCTire)
        {
            GM.cash -= CTCost;
        }
        buyCTire = true;
        GM.isCTires = true;
        
        SOCT.SetActive(true);
    }

    public void OnCliCk6Engine()
    {
        if(!Engine8have)
        {
            GM.player.SetSpeed = 8;
        }

        if (GM.cash < sixECost)
        {
            return;
        }
        GM.cash -= sixECost;
        SO6E.SetActive(true);
    }

    public void OnCliCk8Engine()
    {
        if(GM.cash < EightECost)
        {
            return;
        }
        GM.player.SetSpeed = 10;
        GM.cash -= EightECost;

        SO8E.SetActive(true);
        Engine8have = true;
    }
        
    public void OnCliCkHandle()
    {
        if(GM.cash < HCost)
        {
            return; 
        }
        GM.player.SetTurnSpeed = 1.5f;
        GM.cash -= HCost;
        
        SOH.SetActive(true);
    }

    public void OnCliCkGo()
    {
        Upgrade.SetActive(false);
        GM.UI.SetActive(true);
        GM.UIable = true;
        Time.timeScale = 1;
        GM.PCam.transform.position = GM.player.transform.position;

        GM.Upgrading = false;

        ResetCost();
        if(GM.Finish)
        {
            GM.StageClear.SetActive(true);
        }
    }

    public void ResetUpGrade()
    {
        buyDTire = false;
        GM.isDTires = false;
        SODT.SetActive(false);

        buyFTire = false;
        GM.isFTires = false;
        SOFT.SetActive(false);

        buyCTire = false;
        GM.isCTires = false;
        SOCT.SetActive(false);

        GM.player.SetSpeed = 6;
        SO6E.SetActive(false);

        SO8E.SetActive(false);

        GM.player.SetTurnSpeed = 1f;
        SOH.SetActive(false);

        Engine8have = false;

        ResetCost();
    }

    public void ZeroCost()
    {
        DTCost = 0;
        FTCost = 0;
        CTCost = 0;
        HCost = 0;
        sixECost = 0;
        EightECost = 0;
    }

    public void ResetCost()
    {
        DTCost = 5000000;
        FTCost = 10000000;
        CTCost = 15000000;
        HCost = 15000000;
        sixECost = 10000000;
        EightECost = 20000000;
    }
}
