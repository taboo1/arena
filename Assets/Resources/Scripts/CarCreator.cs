﻿using UnityEngine;
using System.Collections;

public class CarCreator : MonoBehaviour {

    Library library;
	// Use this for initialization
	void Start () {
        library = GameObject.FindObjectOfType<Library>();

        CarParametres carParametres = CarsInfo.GetCarInfo(CarChanger.NumCar);
        
        GameObject GO = Instantiate(Resources.Load("Prefabs/Cars/" + carParametres.GetName())) as GameObject;
        library.car = GO;

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}