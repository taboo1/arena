﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class CarsInfo : MonoBehaviour {

    static List<CarParametres> obj = new List<CarParametres>();
    LibraryMenu libraryMenu;
    // Use this for initialization
    void Awake()
    {
        libraryMenu = GameObject.FindObjectOfType<LibraryMenu>();

        TextAsset xmlAsset = Resources.Load("Info/Cars") as TextAsset;

        XmlDocument xmlDoc = new XmlDocument();
        if (xmlAsset)
            xmlDoc.LoadXml(xmlAsset.text);

        foreach (XmlNode node in xmlDoc.ChildNodes[0])
        {

            int[] param = { 1, 1, 1 };

            foreach (XmlNode childNode in node.ChildNodes[0])
            {
                switch (childNode.Attributes["name"].Value)
                {
                    case "1": param[0] = int.Parse(childNode.InnerText); break;
                    case "2": param[1] = int.Parse(childNode.InnerText); break;
                    case "3": param[2] = int.Parse(childNode.InnerText); break;

                }
            }

            int cost = int.Parse(node.ChildNodes[1].InnerText);

            int level = int.Parse(node.ChildNodes[2].InnerText);
            int minSpeed = int.Parse(node.ChildNodes[3].InnerText);

            bool isBonus = false;

            switch (node.ChildNodes[4].InnerText)
            {
                case "main": isBonus = false; break;
                case "bonus": isBonus = true; break;

            }


            CarParametres carParametres = new CarParametres(obj.Count, node.Attributes["name"].Value, param, cost, level,minSpeed,isBonus);

            obj.Add(carParametres);
        }
    }

    public static CarParametres GetCarInfo(int num)
    {
        if (num >= obj.Count)
            return null;
        else
            return obj[num];
    }
    
    public int GetCarsCount()
    {
        return obj.Count;
    }
    /*
    public CarParametres GetCarParametres(string name)
    {
        CarParametres carParametres;
        obj.TryGetValue(name, out carParametres);

        return carParametres;
    }*/
}
