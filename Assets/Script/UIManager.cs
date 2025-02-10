using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameManager GM;

    public GameObject UI;
    public Text time;

    public void Awake()
    {
        UI = GameObject.Find("UI");
        time = GameObject.Find("time").GetComponent<Text>();
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void Update()
    {
        time.text = GM.MCount.ToString("00") + ":" + GM.SCount.ToString("00");
    }
}
