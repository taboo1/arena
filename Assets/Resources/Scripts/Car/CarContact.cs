﻿using UnityEngine;
using System.Collections.Generic;
using System;


public class CarContact : MonoBehaviour
{

    Library library;
    Vector3 dir = new Vector3();
    Vector3 lastPos = new Vector3();
    CarController carController;

    WheelCollider[] wheelColliders;

    Dictionary<string, int> dictionaryDestroyable = new Dictionary<string, int>();

    bool isCollide;

    bool isZavis;

    Poddon poddon;


    void Start()
    {
        // transform.GetComponent<Collider>().isTrigger = true;
        library = GameObject.Find("Global").GetComponent<Library>();
        carController = GetComponent<CarController>();
        wheelColliders = carController.GetWheelColliders();

    }

    // Update is called once per frame
    void Update()
    {
        UpdateDirection();
    }

    private void UpdateDirection()
    {
        if (library.globalController.gs == GlobalController.GameState.Ride)
        {
            Vector3 temp = (transform.position - lastPos).normalized;
            if (temp != Vector3.zero)
                dir = temp;

            lastPos = transform.position;
        }
    }



    public void OnTriggerEnter1(Collider col)
    {

        Destroyable destoyable = col.gameObject.GetComponent<Destroyable>();

        if (destoyable != null)
        {
            destoyable.OnCollisionObject(transform);
            library.score.AddScoreForDestroy(destoyable);
            library.energy.AddEnergy(destoyable);

            EZCameraShake.CameraShakeInstance c = EZCameraShake.CameraShaker.Instance.ShakeOnce(2, 4, 0.1f, 2f);
            

            AddDestroyable(destoyable.GetType().Name);
        }

        Letter letter = col.gameObject.GetComponent<Letter>();

        if (letter != null)
        {
            WordRide wordRide = library.level.GetComponentInChildren<WordRide>();
            
            if (wordRide != null)
            {
                wordRide.CheckLetter(letter.letterNum);
            }

            Destroy(letter.gameObject);
        }

        //   c.PositionInfluence = new Vector3(1,1,1);
        // c.PositionInfluence = new Vector3(1, 1, 1);
    }

    
    void OnCollisionEnter(Collision collision)
    {
        isCollide = true;
    }

    void OnCollisionStay(Collision collision)
    {
        isCollide = true;
    }

    void OnCollisionExit(Collision collision)
    {
        isCollide = false;
    }

    

    private void AddDestroyable(String name)
    {
        int val = 0;

        if (dictionaryDestroyable.TryGetValue(name, out val))
            dictionaryDestroyable[name] += 1;
        else
            dictionaryDestroyable.Add(name, 1);

    }

    public Vector3 GetDirection()
    {
        return dir;
    }

    public int GetDestroyableCount(string name)
    {
        int val = 0;

        if (dictionaryDestroyable.TryGetValue(name, out val))
            return val;
        else
            return 0;
    }

    public bool IsCollide()
    {
        return isCollide;
    }

    public bool IsFlight()
    {
        bool temp = true;


        for (int i = 0; i < 4; i++)
        {
            if (wheelColliders == null)
            {
                return false;
            }

            temp = temp & !wheelColliders[i].isGrounded;
        }

        temp = temp & !IsCollide();

        return temp;
    }

    public bool IsFreeAllWheel()
    {
        bool temp = true;

        for (int i = 0; i < 4; i++)
        {
            temp = temp && !wheelColliders[i].isGrounded;
        }

        return temp;
    }

    public bool IsOneContact()
    {
        bool temp = false;

        for (int i = 0; i < 4; i++)
        {
            temp = temp || wheelColliders[i].isGrounded;
        }

        temp = temp || IsCollide();

        return temp;
    }

    public bool IsOneContactWheel()
    {
        bool temp = false;

        for (int i = 0; i < 4; i++)
        {
            temp = temp || wheelColliders[i].isGrounded;
        }


        return temp;
    }

    public void MetalSparks(Vector3 sparksPosition)
    {
        GameObject particle = Instantiate(library.metalSparksPrefab);
        particle.transform.position = new Vector3(sparksPosition.x, sparksPosition.y, sparksPosition.z);
        particle.transform.rotation = Quaternion.LookRotation(dir);
        particle.GetComponent<ParticleSystem>().Play();
    }

    public void ToDefault()
    {
        dictionaryDestroyable.Clear();
    }



}

