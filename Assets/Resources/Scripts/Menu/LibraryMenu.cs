﻿using UnityEngine;
using System.Collections;

public class LibraryMenu : MonoBehaviour {

    [HideInInspector] public Garage garage;
    [HideInInspector] public TaskMenu taskMenu;
    [HideInInspector] public KAController kaController;
    [HideInInspector] public WaitBackground waitBackground;
    [HideInInspector] public MainScreen mainScreen;
    [HideInInspector] public GameObject bg;
    [HideInInspector] public CarsInfo carsInfo;
    [HideInInspector] public PreferencesSaver preferencesSaver;
    [HideInInspector] public Filling filling;
    [HideInInspector] public CarChanger carChanger;
    [HideInInspector] public GameObject fireBackground;
    [HideInInspector] public GameObject secondCanvas;
    [HideInInspector] public GameObject fireStart;
    // [HideInInspector] public GameObject car;

    // Use this for initialization
	void Awake () {
        secondCanvas = GameObject.Find("SecondCanvas");
        fireBackground = GameObject.Find("FireBackground");
        fireStart = GameObject.Find("FireStart");
        garage = GameObject.FindObjectOfType<Garage>();
        taskMenu = GameObject.FindObjectOfType<TaskMenu>();
        kaController = GameObject.FindObjectOfType<KAController>();
        waitBackground = GameObject.FindObjectOfType<WaitBackground>();
        mainScreen = GameObject.FindObjectOfType<MainScreen>();
        bg = GameObject.Find("BG");
        carsInfo = GetComponent<CarsInfo>();
        preferencesSaver = GetComponent<PreferencesSaver>();
        filling = GameObject.FindObjectOfType<Filling>();
        carChanger = GameObject.FindObjectOfType<CarChanger>();
        /*
        car = garage.transform.Find("Car").gameObject;
        if (car == null)
            CreateCar();    */
	}
	
    void CreateCar()
    {
        Instantiate(Resources.Load("Prefabs/UI/Car"));
    }
}
