﻿using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.Net.Cache;
using System.IO;
using System.Text.RegularExpressions;

public class MainBonus : MonoBehaviour {

    public const int MaxValue = 5;
    public static int count = MaxValue;
    DateTime lastDateTime;
    TimeSpan subtractTime;

    bool isAvail;

    public const int RecoveryTime = 1800;

    // Use this for initialization
    void Start () {
        count = PreferencesSaver.GetMainBonus();
        lastDateTime = PreferencesSaver.GetMainBonusTime();
        subtractTime = new TimeSpan();
    }

    public void Update()
    {

        UpdateState();
    }

    public bool IsAvailable()
    {

        return isAvail;
    }

    public void MinusItem()
    {
        if (count > 0)
        {
            isAvail = true;
            if (count == MaxValue)
            {
                DateTime nowTime = System.DateTime.Now;
                lastDateTime = nowTime;
                PreferencesSaver.SaveMainBonusTime(nowTime);
            }

            count--;
            PreferencesSaver.SetMainBonus(count);

        }
        else
            isAvail = false;
    }

    public void AddItem(int col)
    {
        if (col > 0)
        {
            
            count += col;
            count = Mathf.Clamp(count, 0, 100);
            PreferencesSaver.SetMainBonus(count);
        }
    }

    /*
    public static void AddItems(int col)
    {
        count += col;
    }*/
    
    

    public TimeSpan GetSubtract()
    {
       //if (subtractTime == null)
         //   return ;

        TimeSpan ts =  (new TimeSpan(0, (int)Mathf.Floor(RecoveryTime / 60f), RecoveryTime % 60)).Subtract(subtractTime); 
        return ts;
    }

    public int GetSubtractSeconds()
    {
        if (subtractTime == null)
            return 0;
        return subtractTime.Seconds;
    }

    public void UpdateState()
    {
        DateTime nowTime = System.DateTime.Now;

        subtractTime = nowTime.Subtract(lastDateTime);

        if (subtractTime.TotalSeconds > RecoveryTime)
        {
            if (MainBonus.count < MainBonus.MaxValue)
            {
                AddItem((int)Mathf.Floor((float)subtractTime.TotalSeconds / RecoveryTime));

                lastDateTime = nowTime - TimeSpan.FromSeconds((float)subtractTime.TotalSeconds % RecoveryTime);

                PreferencesSaver.SaveMainBonusTime(lastDateTime);
            }
        }

    }
}
