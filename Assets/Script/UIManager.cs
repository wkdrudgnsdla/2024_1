using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameManager GM;
    PlayerMove player;

    public GameObject UI;
    public Text time;
    public Text CountText;
    public Text SpeedText;
    public Text Cheat;

    public void Awake()
    {
        UI = GameObject.Find("UI");
        time = GameObject.Find("time").GetComponent<Text>();
        CountText = GameObject.Find("CountText").GetComponent<Text>();
        SpeedText = GameObject.Find("SpeedText").GetComponent <Text>();
        Cheat = GameObject.Find("CheatText").GetComponent<Text>();

        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.Find("Player").GetComponent<PlayerMove>();
    }

    public void Update()
    {
        time.text = GM.MCount.ToString("00") + ":" + GM.SCount.ToString("00");


        int count = (int)GM.StartCountdown;

        if (count > 0)
        {
            CountText.text = count.ToString();
        }
        else if (count == 0)
        {
            CountText.text = "GO!";
        }
        else if (count <= -1)
        {
            CountText.text = " ";
        }

        if (Input.GetKeyUp(KeyCode.F3))
        {
            StartCoroutine(F3());
        }

        int speed = (int)player.currentSpeed * 3;
        SpeedText.text = speed.ToString() + "km/h";
    }

    IEnumerator F3()
    {
        Cheat.text = "F3: 레이스 재시작";
        yield return new WaitForSeconds(1.2f);
        Cheat.text = " ";
    }
}
