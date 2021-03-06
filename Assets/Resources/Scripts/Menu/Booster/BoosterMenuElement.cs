﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEngine.EventSystems;

public abstract class BoosterMenuElement : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
 {


    protected int boosterNum;
     
    LibraryMenu libraryMenu;

    public Text description;
    public Text cost;
    public Text time;

    public Sprite unActiveSprite;
    public Sprite activeSprite;

    public Sprite activeSteel;
    public Sprite unActiveSteel;

    public Image steel;
    //   bool isActive;

    public Image boosterImage;
    DateTime lastDateTime;
    TimeSpan subtractTime;

    bool isActive;

    public RawBoosterButton rawButton;

    // Use this for initialization
    public void Start () {
        libraryMenu = GameObject.FindObjectOfType<LibraryMenu>();
        lastDateTime = PreferencesSaver.GetBoosterActivateTime(boosterNum);
        description.text = TextStrings.GetString("booster_"+boosterNum+"_description");

    }




    public void BuyBoosters()
    {
        if (isActive)
        {
            libraryMenu.windowWarning.Show(TextStrings.GetString("booster_is_active"));
        }
        else
        {
            if (Bank.GetFreeBooster(boosterNum) > 0)
            {
                lastDateTime = System.DateTime.Now;
                PreferencesSaver.SetBoosterActivateTime(boosterNum, lastDateTime);
                Bank.MinusFreeBooster(boosterNum,1);
                
            }
            else
            {
                if (BoosterValues.BoosterCost[boosterNum] > Bank.GetMoney())
                {
                    libraryMenu.windowWarning.Show(TextStrings.GetString("no_money"));
                }
                else
                {
                    lastDateTime = System.DateTime.Now;
                    PreferencesSaver.SetBoosterActivateTime(boosterNum, lastDateTime);
                    Bank.MinusMoney(BoosterValues.BoosterCost[boosterNum]);
                }
            }
        }
    }

    public void Update()
    {
        DateTime nowTime = System.DateTime.Now;

        subtractTime = nowTime.Subtract(lastDateTime);


        if (subtractTime.TotalSeconds < BoosterValues.BoosterTime[boosterNum] * 60)
        {   
            UpdateTime();

            string costText = TextStrings.GetString("is_activate");

            if(!costText.Equals(cost.text))
                cost.text = costText;

            if (!boosterImage.sprite.Equals(activeSprite))
            {
                boosterImage.sprite = activeSprite;
                rawButton.DisableButton();
                steel.sprite = activeSteel;
            }
            isActive = true;
        }
        else
        {
            string timeText = BoosterValues.BoosterTime[boosterNum] + " " + TextStrings.GetString("minutes");

            if (!time.text.Equals(timeText))
                time.text = timeText;

                if (Bank.GetFreeBooster(boosterNum) > 0)
                {
                    string textCost = TextStrings.GetString("free");
                    if(!cost.text.Equals(textCost))
                        cost.text = textCost;
                }
                else
                {
                    string textCost = "^ " + BoosterValues.BoosterCost[boosterNum];
                     if (!cost.text.Equals(textCost))
                        cost.text = textCost;
                }

            if (!boosterImage.sprite.Equals(unActiveSprite))
            {
                boosterImage.sprite = unActiveSprite;
                rawButton.EnableButton();
                steel.sprite = unActiveSteel;
            }
        }

    }

    void UpdateTime()
    {
        TimeSpan ts = (new TimeSpan(0, (int)Mathf.Floor(BoosterValues.BoosterTime[boosterNum]), BoosterValues.BoosterTime[boosterNum] * 60 % 60)).Subtract(subtractTime);

        string timeTemp = ts.Minutes.ToString("D2") + " : " + ts.Seconds.ToString("D2");

        if (!time.text.Equals(timeTemp))
        {
            time.text = timeTemp;
        }
    }

    public static bool IsActive(int num)
    {
        DateTime nowTime = System.DateTime.Now;
   
        TimeSpan subtractTime = nowTime.Subtract(PreferencesSaver.GetBoosterActivateTime(num));

        if (subtractTime.TotalSeconds < BoosterValues.BoosterTime[num] * 60)
            return true;
        else
            return false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        rawButton.OnPointerDown(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        rawButton.OnPointerUp(eventData);
    }


}
