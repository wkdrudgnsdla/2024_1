using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashUIManager : MonoBehaviour
{
    public GameObject Front;
    public GameObject Back;
    public GameObject Side;

    public bool isfront;
    public bool isback;
    public bool isside;

    public void Awake()
    {
        Front = GameObject.Find("FrontCrash");
        Back = GameObject.Find("BackCrash");
        Side = GameObject.Find("SideCrash");
    }

    public void Start()
    {
        isfront = false;
        isback = false;
        isside = false;

        Front.SetActive(false);
        Back.SetActive(false);
        Side.SetActive(false);
    }

    public void Update()
    {
        if(isfront)
        {
            StartCoroutine(FrontUI());
        }

        if(isback)
        {
            StartCoroutine (BackUI());
        }

        if(isside)
        {
            StartCoroutine(SideUI());
        }
    }

    IEnumerator FrontUI()
    {
        Front.SetActive(true);
        yield return new WaitForSeconds(1);
        Front.SetActive(false);
        isfront = false;
    }
    IEnumerator BackUI()
    {
        Back.SetActive(true);
        yield return new WaitForSeconds(1);
        Back.SetActive(false);
        isback = false;
    }
    IEnumerator SideUI()
    {
        Side.SetActive(true);
        yield return new WaitForSeconds(1);
        Side.SetActive(false);
        isside = false;
    }
}
