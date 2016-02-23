﻿using UnityEngine;
using System.Collections;

public class GlobalController : MonoBehaviour {

    public enum GameState {WaitForStart, Ride, WaitForStay, Baraban, RetryMenu}
    [HideInInspector] public GameState gs;
    // Use this for initialization
    Library library;




	void Start () {
        library = GetComponent<Library>();

        
        SetToDefault();

        Bank.PlusBarabanBooster(2);
	}
	
	public void SetToDefault()
    {
        gs = GameState.WaitForStart;

        library.mainBonus.UpdateState();
        library.mainBonus.MinusItem();
        library.secondCamera.SetActive(false);

        GameObject level = GameObject.FindGameObjectWithTag("Level");
        
        if (level == null)
        {
            level = CreateLevel();
        }
        else
        {

            if (StaticValues.NumLevel == 999)
                StaticValues.NumLevel = level.GetComponent<LevelDevelopNum>().Num;

            if (library.level != null)
            {
                Destroy(library.level);
                level = CreateLevel();
            }
        }

       

        library.level = level;

        library.waitBackground.Hide();



    }

    GameObject CreateLevel()
    {

        string levelName = Info.GetLevelInfo(StaticValues.NumLevel).GetName();

        GameObject newLevel = Instantiate(Resources.Load("Prefabs/Levels/" + levelName)) as GameObject;
        newLevel.name = levelName;

        library.pauseMenu.ClearTasks();
        library.carCreator.UpdateCar();
        library.energy.ToDefault();
        library.wordRideCanvas.ToDefault();
        library.cam.GetComponent<CameraMotion>().ToDefaultPosition();
        library.fullScore.ClearScore();
        library.timerScript.ToDefault();
        library.canvasController.HideEndMenu();
        library.canvasController.ShowGameUI();
        library.canvasController.ShowInput();

        return newLevel;
    }

    public void StartCar()
    {
        library.timerScript.SetStart();
        gs = GameState.Ride;
    }

    public void TimerIsEnd()
    {
        gs = GameState.WaitForStay;
        StartCoroutine(CheckIsStay());

    }

    IEnumerator CheckIsStay()
    {
        while (true)
        {
            if(library.car.GetComponent<CarUserControl>().IsStay())
            {
                StartCoroutine(PartyTime());
                break;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator PartyTime()
    {
        BeforeShowMenu();
        yield return new WaitForSeconds(1.5f);
        ShowMenu();


    }

    void BeforeShowMenu()
    {
        library.score.CurrentScoreToFullScore();
    }

    void ShowMenu()
    {
        library.canvasController.ShowBaraban();
        gs = GameState.Baraban;
      //  Time.timeScale = 0;
    }

    void Update()
    {
    }


}
