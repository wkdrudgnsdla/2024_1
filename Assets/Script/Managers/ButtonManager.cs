using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    Scene scene;

    public Button StartButton;
    public Button RankingButton;
    public Button HelpButton;
    public Button QuitButton;

    public GameObject MenuObejct;
    public GameObject MainMenu;


    public void Awake()
    {

        MenuObejct = GameObject.Find("MenuObejct");
        
        StartButton = GameObject.Find("StartButton").GetComponent<Button>();
        RankingButton = GameObject.Find("RankingButton").GetComponent <Button>();
        HelpButton = GameObject.Find("HelpButton").GetComponent<Button>();
        QuitButton = GameObject.Find("ExitButton").GetComponent<Button>();

        MenuObejct = GameObject.Find("MenuObejct");
        MainMenu = GameObject.Find("MainMenu");
    }

    public void OnClickStartButton()
    {
        MainMenu.SetActive(false);
    }

    public void OnClickRankingButton()
    {

    }

    public void OnClickHelpButton()
    {

    } 

    public void OnClickQuitButton()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }


}
