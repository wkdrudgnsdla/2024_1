using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class CheatManager : MonoBehaviour
{
    GameManager GM;

    public bool pused;

    public void Awake()
    {
        GM = GetComponent<GameManager>();
    }

    public void Start()
    {
        pused = false;
    }

    void Update()
    {


        F3();
        F4();
        F5();
    }

    void F3()
    {
        if (Input.GetKeyUp(KeyCode.F3))
        {
            Destroy(GM._Stage1Items);
            Destroy(GM._Stage2Items);
            GM.StartGame();
        }
    }

    void F4()
    {
        if(Input.GetKeyUp(KeyCode.F4))
        {
            GM._StageLevel += 1;
            GM.NextGame();
        }
    }


    void F5()
    {
        if (Input.GetKeyUp(KeyCode.F5))
        {
            if (!pused)
            {
                Time.timeScale = 0;
                pused = true;
            }
        }
        if (pused)
        {
            if (Input.GetKeyUp(KeyCode.F5))
            {
                Time.timeScale = 1;
            }
        }
    }
}
