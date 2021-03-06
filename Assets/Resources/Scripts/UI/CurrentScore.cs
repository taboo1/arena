﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CurrentScore : MonoBehaviour {

    Text text;

    int score;
    int coef;
    int fullScore;

    Material redColor;
    Material blueColor;

    //  Color yellowColor = new Color(255/255f, 215/255f, 0/255f, 255/255f);
    //Color blueColor = new Color(45 / 255f, 45 / 255f, 255 / 255f, 255/255f);
    Library library;
    Particle fireParticle;
    Particle smokeParticle;

    IEnumerator currentCoroutine;
	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        redColor = Resources.Load("Font/font1") as Material;
        blueColor = Resources.Load("Font/font2") as Material;

        library = GameObject.FindObjectOfType<Library>();

        fireParticle = library.particleCanvas.transform.FindChild("UI").FindChild("FireComboParticle").GetComponent<Particle>();
        smokeParticle = library.particleCanvas.transform.FindChild("UI").FindChild("SmokeComboParticle").GetComponent<Particle>();

        fireParticle.GetComponent<RectTransform>().anchoredPosition =
            new Vector2(
            fireParticle.GetComponent<RectTransform>().anchoredPosition.x,
            GetComponent<RectTransform>().anchoredPosition.y);

        smokeParticle.GetComponent<RectTransform>().anchoredPosition = 
            new Vector2(
            smokeParticle.GetComponent<RectTransform>().anchoredPosition.x,
            GetComponent<RectTransform>().anchoredPosition.y);

        text.enabled = false;
    }

    public void AddScoreAndCoef(int addScore, bool isCoef)
    {
        if(isCoef)
            coef += 1;

        score += addScore;
        fullScore = coef * score;

        ShowScore();
        
    }

   

    private void ShowScore()
    {
        if(!fireParticle.GetParticle().loop)
        fireParticle.PlayLoop();

        string temp = score + " X " + coef;

        if (!text.enabled)
            text.enabled = true;

        if(!temp.Equals(text.text))
            text.text = temp;

        if(!text.material.Equals(redColor))
            text.material = redColor;

        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);
    }


    public int GetFullScore()
    {
        return fullScore;
    }

    public void ClearScore()
    {
        fireParticle.StopLoop();
        smokeParticle.GetParticle().Play();

        if (!text.enabled)
            text.enabled = true;

        if (!text.material.Equals(blueColor))
            text.material = blueColor;

        string temp = fullScore + "";

        if(!temp.Equals(text.text))
            text.text = fullScore+"";

        score = 0;
        coef = 0;
        fullScore = 0;

        currentCoroutine = HideScore();
        StartCoroutine(currentCoroutine);
    }

    IEnumerator HideScore()
    {
        yield return new WaitForSeconds(1);
        text.text = "";

        if (text.enabled)
            text.enabled = false;

    }

    public bool IsZero()
    {
        if (fullScore == 0)
            return true;
        else
            return false;
    }

	
	
}
