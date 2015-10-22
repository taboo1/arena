﻿using UnityEngine;
using System.Collections;

public class TimerScript : MonoBehaviour {

    float timeLeft;
    Library library;
    bool isShow;
    void Start () {
        library = FindObjectOfType<Library>();
        ToDefault();
	}

    public void ToDefault()
    {
        timeLeft = 90;
        isShow = false;
    }
	
	// Update is called once per frame
	void Update () {
        timeLeft = Mathf.Clamp(timeLeft - Time.deltaTime, 0, 1000);

        if (timeLeft == 0 && !isShow)
        {
            isShow = true;
            library.globalController.TimerIsEnd();
        }
	}


    public float GetTimeLeft()
    {
        return timeLeft;
    }


}
