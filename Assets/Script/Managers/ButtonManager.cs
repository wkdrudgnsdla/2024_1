using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    Scene scene;

    public GameManager GM;

    public Button StartButton;
    public Button RankingButton;
    public Button HelpButton;
    public Button QuitButton;

    public GameObject MenuObejct;
    public GameObject MainMenu;
    public GameObject Help;
    public GameObject Ranking;


    public void Awake()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();

        MenuObejct = GameObject.Find("MenuObejct");
        
        StartButton = GameObject.Find("StartButton").GetComponent<Button>();
        RankingButton = GameObject.Find("RankingButton").GetComponent <Button>();
        HelpButton = GameObject.Find("HelpButton").GetComponent<Button>();
        QuitButton = GameObject.Find("ExitButton").GetComponent<Button>();

        MenuObejct = GameObject.Find("MenuObejct");
        MainMenu = GameObject.Find("MainMenu");
        Help = GameObject.Find("Help");
        Ranking = GameObject.Find("Ranking");
    }

    public void Start()
    {
        Ranking.SetActive(false);
    }

    public void Update()
    {
        if(Ranking.active == true)
        {
            if(Input.GetMouseButton(0) || Input.GetMouseButton(1))
            {
                Ranking.SetActive(false);
            }
        }
        else
        {
            return;
        }
    }

    public void OnClickStartButton()
    {
        MainMenu.SetActive(false);
        GM.StartGame();
        GM.Stage1Enemy.SetActive(true);
    }

    public void OnClickRankingButton()
    {
        Ranking.SetActive(true);
    }

    public void OnClickHelpButton()
    {
        Help.SetActive(true);
    } 

    public void OnClickQuitButton()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }


}
