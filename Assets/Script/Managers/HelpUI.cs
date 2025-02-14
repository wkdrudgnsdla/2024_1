using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpUI : MonoBehaviour
{
    public Text HelpText;
    public Text HelpTitle;

    public Button NextPageButton;
    public Button BeforePageButton;

    public GameObject Help;

    [SerializeField]
    private int _pagenum;

    public int pagenum
    {
        get
        {
            return _pagenum;
        }
        set
        {
            if (value > 0 && value <= 3)
            {
                _pagenum = value;
            }
        }
    }


    public void Awake()
    {
        HelpText = GameObject.Find("HelpText").GetComponent<Text>();
        NextPageButton = GameObject.Find("NextPageButton").GetComponent<Button>();
        BeforePageButton = GameObject.Find("BeforePageButton").GetComponent <Button>();
        HelpTitle = GameObject.Find("HelpTitle").GetComponent<Text>();
        Help = GameObject.Find("Help");
    }

    public void Start()
    {
        _pagenum = 1;
        Help.SetActive(false);
    }

    public void Update()
    {
        if (_pagenum == 1)
        {
            HelpTitle.text = "���丮 & ���ӹ��";
            HelpText.text = "Racer�� ���������� Ŭ���� �ذ��� ����� ȹ���ϰ�\r\n" +
                "��ǰ�� �����Ͽ� ���� ������������ �¸��ؼ�\r\n" +
                "����� ���� Ƽ���� ��� ���� ��ǥ�� �����Դϴ�.\r\n" +
                "\r\n�������� ���� �����ϴ� �پ��� ���ؿ�Ҹ� ���ϰ� �����۰�\r\n" +
                "�ν�Ʈ ������ �̿��Ͽ� �� �������� ���� ��� ������ ����Ͽ�  ����� ���� Ƽ���� ȹ���ϼ���!";
        }
        else if(_pagenum == 2)
        {
            HelpTitle.text = "����Ű";
            HelpText.text = "�̵�- W,A,S,D or ����Ű\r\n�극��ũ - �����̽���\r\n\r\n������ ġƮŰ - F1               ���� ���� ġƮŰ - F2\r\n�������� �����- F3            ���� �������� - F4\r\n�Ͻ����� - F5                       �޴�ȭ�� - F6";
        }
        else if(pagenum == 3)
        {
            HelpTitle.text = "������ �� ��ǰ";
            HelpText.text = "������ ����\r\n- 100, 500, 1000���� ȹ��\r\n- ���� �ν�Ʈ, ���� �ν�Ʈ\r\n- ���� �̵�\r\n\r\n���� ��ǰ ����\r\n- �� �������� ���� Ÿ�̾�\r\n- 6���� ����, 8���� ����\r\n-�ڵ� ��� ��ȭ";
        }
    }

    public void OnClickNextPageButton()
    {
        _pagenum += 1;
    }

    public void OnClickBeforePageButton()
    {
        _pagenum -= 1;
    }


    public void OnClickBackButton()
    {
        Help.SetActive(false);
    }

}
