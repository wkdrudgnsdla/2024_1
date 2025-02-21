using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using System.Linq;

public class ItemBox : MonoBehaviour
{
    public ItemUIManager IUM;
    public PlayerMove player;
    public GameManager GM;
    public Camera PCam;

    public GameObject Upgrade;

    private int _ItemNum;

    public int ItemNum
    {
        get
        {
            return _ItemNum;
        }
        set
        {
            _ItemNum = value;
            changeable = false;
        }
    }
    public bool changeable;

    private void Awake()
    {
        Upgrade = GameObject.Find("Upgrade");
        IUM = GameObject.Find("GameManager").GetComponent<ItemUIManager>();
        player = GameObject.Find("Player").GetComponent<PlayerMove>();
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        PCam = GameObject.Find("PlayerCam").GetComponent<Camera>();
    }

    private void Start()
    {
        _ItemNum = 0;
        changeable = true;

        
    }

    public void Update()
    {
        if(Upgrade == null)
        {
            GameObject upgradeComp = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(go => go.name == "Upgrade");
                Debug.Log("Find Component...");
            Upgrade = upgradeComp;
        }
        
    }



    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(changeable)
            {
                IUM.UseItme = true;
                ItemNum = Random.Range(1, 6 + 1);
                ItemControl(_ItemNum);
            }
            Destroy(gameObject);
        }
    }

    private void ItemControl(int Num)
    {
        switch (Num)
        {
            case 1:
                Item1();
                break;
            case 2:
                Item2();
                break;
            case 3:
                Item3(); 
                break;
            case 4:
                Item4();
                break;
            case 5:
                Item5();
                break;
            case 6:
                Item6();
                break;
            default:
                break;
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
        IUM.itemnum = 4;
        player.rb.AddForce(player.transform.right * 1000 * Time.deltaTime, ForceMode.Impulse);
        PCam.fieldOfView = 75f;
        Debug.Log("속도 소폭 증가");
    }

    private void Item5()
    {
        IUM.itemnum = 5;
        player.rb.AddForce(player.transform.right * 2000* Time.deltaTime, ForceMode.Impulse);
        PCam.fieldOfView = 80f;
        Debug.Log("속도 대폭 증가");
    }

    private void Item6()
    {
        PCam.transform.position = new Vector3(-214.2403f, -131.58f, 247.84f);
        PCam.transform.rotation = Quaternion.Euler(15, 0, 0);
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
