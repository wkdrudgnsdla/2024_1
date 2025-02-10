using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float sec;
    public int SCount;

    public int MCount;

    public void Update()
    {
        sec += Time.deltaTime;

        SCount = (int)sec;

        if (SCount >= 60)
        {
            SCount = 0;
            MCount += 1;
        }
    }
}
