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

    public bool buyCTire;
    public bool buyDTire;
    public bool buyFTire;

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
    }

    public void Start()
    {
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
        CoinText.text = _coin.ToString() + "¸¸¿ø";
    }

    public void OnCliCkDesert()
    {
        if(GM.cash < 5000000)
        {
            return;
        }

        if (!buyDTire)
        {
            GM.cash -= 5000000;
        }
        buyDTire = true;
        GM.isDTires = true;
        SODT.SetActive(true);
    }

    public void OnClickForest()
    {
        if(GM.cash < 10000000)
        {
            return;
        }

        if(!buyFTire)
        {
            GM.cash -= 10000000;
        }
        buyFTire = true;
        GM.isFTires = true;


        SOFT.SetActive(true);
    }

    public void OnCliCkCTires()
    {
        if(GM.cash < 15000000)
        {
            return;
        }

        if (!buyCTire)
        {
            GM.cash -= 15000000;
        }
        buyCTire = true;
        GM.isCTires = true;
        
        SOCT.SetActive(true);
    }

    public void OnCliCk6Engine()
    {
        if(GM.cash < 10000000)
        {
            return;
        }
        GM.player.SetSpeed = 8;
        GM.cash -= 10000000;

        SO6E.SetActive(true);
    }

    public void OnCliCk8Engine()
    {
        if(GM.cash < 20000000)
        {
            return;
        }
        GM.player.SetSpeed = 10;
        GM.cash -= 20000000;

        SO8E.SetActive(true);
    }
        
    public void OnCliCkHandle()
    {
        if(GM.cash < 15000000)
        {
            return; 
        }
        GM.player.SetTurnSpeed = 1.5f;
        GM.cash -= 15000000;
        
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
    }
}
