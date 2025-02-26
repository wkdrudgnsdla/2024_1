using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class CheatManager : MonoBehaviour
{
    GameManager GM;
    ItemUIManager IUM;
    UpgradeManager UM;
    AudioSource Bost;
    public AudioSource CheatSound;

    public GameObject CheatItem;
    public GameObject Upgrade;

    public bool pused;
    public bool OnCheatItem;

    public void Awake()
    {
        Bost = GameObject.Find("Bost").GetComponent<AudioSource>();
        CheatSound = GameObject.Find("CheatSound").GetComponent<AudioSource>();
        GM = GetComponent<GameManager>();
        CheatItem = GameObject.Find("CheatItem");
        Upgrade = GameObject.Find("Upgrade");
        IUM = gameObject.GetComponent<ItemUIManager>();
        UM = gameObject.GetComponent<UpgradeManager>();
    }

    public void Start()
    {
        pused = false;
        OnCheatItem = false;
        CheatItem.SetActive(false);
    }

    void Update()
    {
        F1();
        F2();
        F3();
        F4();
        F5();
    }

    void F1()
    {
        if(Input.GetKeyUp(KeyCode.F1))
        {
            CheatSound.Play();
            CheatItem.SetActive(true);
            OnCheatItem = true;
        }

        if (OnCheatItem)
        {
            if (Input.GetKeyUp(KeyCode.Alpha1))
            {
                Item1();
                CheatItem.SetActive(false);
                OnCheatItem = false;
            }
            else if (Input.GetKeyUp(KeyCode.Alpha2))
            {
                Item2();
                CheatItem.SetActive(false);
                OnCheatItem = false;
            }
            else if (Input.GetKeyUp(KeyCode.Alpha3))
            {
                Item3();
                CheatItem.SetActive(false);
                OnCheatItem = false;
            }
            else if (Input.GetKeyUp(KeyCode.Alpha4))
            {
                Item4();
                CheatItem.SetActive(false);
                OnCheatItem = false;
            }
            else if (Input.GetKeyUp(KeyCode.Alpha5))
            {
                Item5();
                CheatItem.SetActive(false);
                OnCheatItem = false;
            }
            else if (Input.GetKeyUp(KeyCode.Alpha6))
            {
                Item6();
                CheatItem.SetActive(false);
                OnCheatItem = false;
            }
        }
    }

    void F2()
    {
        if(Input.GetKeyUp(KeyCode.F2))
        {
            CheatSound.Play();

            if (UM.Upgrade.active == true)
            {
                UM.ZeroCost();
            }
            else
            {
                return;
            }
        }
    }

    void F3()
    {
        if (Input.GetKeyUp(KeyCode.F3))
        {
            CheatSound.Play();

            Destroy(GM._Stage1Items);
            Destroy(GM._Stage2Items);
            Destroy(GM._Stage3Items);
            GM.StartGame();
        }
    }

    void F4()
    {
        if(Input.GetKeyUp(KeyCode.F4))
        {
            CheatSound.Play();

            GM._StageLevel += 1;
            GM.NextGame();
        }
    }


    void F5()
    {
        if (Input.GetKeyUp(KeyCode.F5))
        {
            CheatSound.Play();

            pused = !pused;
            Time.timeScale = pused ? 0 : 1;
        }
    }


    private void Item1()
    {
        IUM.itemnum = 1;
        GM.cash += 1000000;
        Debug.Log("100만원");
    }

    private void Item2()
    {
        IUM.itemnum = 2;

        GM.cash += 5000000;
        Debug.Log("500만원");

    }

    private void Item3()
    {
        IUM.itemnum = 3;

        GM.cash += 10000000;
        Debug.Log("1000만원");
    }

    private void Item4()
    {
        Bost.Play();
        IUM.itemnum = 4;
        GM.player.rb.AddForce(GM.player.transform.right * 1000 * Time.deltaTime, ForceMode.Impulse);
        GM.PCam.fieldOfView = 75f;
        Debug.Log("속도 소폭 증가");
    }

    private void Item5()
    {
        Bost.Play();
        IUM.itemnum = 5;
        GM.player.rb.AddForce(GM.player.transform.right * 2000 * Time.deltaTime, ForceMode.Impulse);
        GM.PCam.fieldOfView = 80f;
        Debug.Log("속도 대폭 증가");
    }

    private void Item6()
    {
        GM.PCam.transform.position = new Vector3(-214.2403f, -131.58f, 247.84f);
        GM.PCam.transform.rotation = Quaternion.Euler(15, 0, 0);
        GM.cam.followSpeed = 0;
        GM.cam.rotationSpeed = 0;
        IUM.itemnum = 6;
        Time.timeScale = 0.001f;
        GM.UI.SetActive(false);
        GM.UIable = false;
        GM.Upgrading = true;
        Upgrade.SetActive(true);
    }
}
