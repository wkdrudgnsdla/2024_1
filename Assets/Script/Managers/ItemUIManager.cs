using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUIManager : MonoBehaviour
{
    public GameObject ItemLogBG;
    public GameObject Item1;
    public GameObject Item2;
    public GameObject Item3;
    public GameObject Item4;
    public GameObject Item5;
    public GameObject Item6;

    public Text ItemName;

    public bool UseItme;

    public int itemnum;

    private void Awake()
    {
        ItemLogBG = GameObject.Find("ItemLogBG");
        Item1 = GameObject.Find("Item1");
        Item2 = GameObject.Find("Item2");
        //Item3 = GameObject.Find("Itme3");
        Item4 = GameObject.Find("Item4");
        Item5 = GameObject.Find("Item5");
        Item6 = GameObject.Find("Item6");

        ItemName = GameObject.Find("ItemName").GetComponent<Text>();
    }

    private void Start()
    {
        ItemLogBG.SetActive(false);
        Item1.SetActive(false);
        Item2.SetActive(false);
        Item3.SetActive(false);
        Item4.SetActive(false);
        Item5.SetActive(false);
        Item6.SetActive(false);
        ItemName.text = " ";

        UseItme = false;
        itemnum = 0;
    }

    void Update()
    {
        if(UseItme)
        {
            StartCoroutine(OnUIItem(itemnum));
        }
        else
        {
            return;
        }
    }

    IEnumerator OnUIItem(int _itemnum)
    {
        ItemLogBG.SetActive(true);
        switch (_itemnum)
        {
            case 1:
                Item1.SetActive(true);
                ItemName.text = "100¸¸¿ø È¹µæ";
                Item2.SetActive(false);
                Item3.SetActive(false);
                Item4.SetActive(false);
                Item5.SetActive(false);
                Item6.SetActive(false);
                break;
            case 2:
                Item2.SetActive(true);
                ItemName.text = "500¸¸¿ø È¹µæ";
                Item1.SetActive(false);
                Item3.SetActive(false);
                Item4.SetActive(false);
                Item5.SetActive(false);
                Item6.SetActive(false);
                break;
            case 3:
                Item3.SetActive(true);
                ItemName.text = "1000¸¸¿ø È¹µæ";
                Item1.SetActive(false);
                Item2.SetActive(false);
                Item4.SetActive(false);
                Item5.SetActive(false);
                Item6.SetActive(false);
                break;
            case 4:
                Item4.SetActive(true);
                ItemName.text = "³ë¸Ö ºÎ½ºÆ® »ç¿ë";
                Item1.SetActive(false);
                Item2.SetActive(false);
                Item3.SetActive(false);
                Item5.SetActive(false);
                Item6.SetActive(false);
                break;
            case 5:
                Item5.SetActive(true);
                ItemName.text = "½´ÆÛ ºÎ½ºÆ® »ç¿ë";
                Item1.SetActive(false);
                Item2.SetActive(false);
                Item3.SetActive(false);
                Item4.SetActive(false);
                Item6.SetActive(false);
                break;
            case 6:
                Item6.SetActive(true);
                ItemName.text = "»óÁ¡";
                Item1.SetActive(false);
                Item2.SetActive(false);
                Item3.SetActive(false);
                Item4.SetActive(false);
                Item5.SetActive(false);
                break;
            default:
                break;
        }
        yield return new WaitForSeconds(0.8f);

        Item1.SetActive(false);
        Item2.SetActive(false);
        Item3.SetActive(false);
        Item4.SetActive (false);
        Item5.SetActive(false);
        Item6.SetActive(false);
        ItemLogBG.SetActive(false);
        ItemName.text = " ";
        UseItme = false;
        itemnum = 0;
    }
}
