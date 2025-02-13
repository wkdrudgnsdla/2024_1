using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatManager : MonoBehaviour
{
    GameManager GM;

    public void Awake()
    {
        GM = GetComponent<GameManager>();
    }
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.F3))
        {
            GM.StartGame();
        }
    }
}
