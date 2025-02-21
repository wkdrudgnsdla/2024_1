using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLineStage1 : MonoBehaviour
{
    public GameManager GM;
    public GameObject StageClear;

    public bool Up;

    public void Awake()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        //StageClear = GameObject.Find("StageClear").GetComponent <GameObject>();
    }

    public void Start()
    {
        Up = false;
        StageClear.SetActive(false);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            StageClear.SetActive(true);

            Destroy(GM.InStageItems);

            GM.Finish = true;
            GM.startRace = false;
            if (!Up)
            {
                GM.Money += 5000000;
                GM.StageScore += 50000;
                if (GM.MCount == 1 && GM.SCount <= 10)
                {
                    GM.TimerScore += 50000;
                }
                else if (GM.MCount == 1 && GM.SCount <= 40 && GM.SCount > 10)
                {
                    GM.TimerScore += 30000;
                }
                else if (GM.MCount == 2 && GM.SCount <= 30 && GM.SCount > 40)
                {
                    GM.TimerScore += 10000;
                }
                else if (GM.MCount < 1)
                {
                    GM.TimerScore = 70000;
                }
                else
                {
                    GM.TimerScore += 0;
                }
                GM.cash += GM.Money;
                Up = true;
            }
        }
        



        GM.player.rb.velocity = Vector3.Lerp(GM.player.rb.velocity, Vector3.zero, Time.deltaTime * 40);
        GM.player.rb.angularVelocity = Vector3.Lerp(GM.player.rb.angularVelocity, Vector3.zero, Time.deltaTime * 40);
    }
}
