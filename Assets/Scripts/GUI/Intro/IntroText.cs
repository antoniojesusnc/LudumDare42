using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroText : MonoBehaviour {


    [SerializeField]
    TMPro.TextMeshProUGUI _text;

    string text01 = "Hey Bob, welcome to C.R.A.P,  Company for Redistribution of Animal Population. Your assignated spacecarft is Pegi. As trainee, an easy one will be your first mission: to solve the problems of Andromeda Galaxy.";
    string text02 = "Your task is to keep the balance between different species inside WCc, one of the planets in that galaxy.";
    string text03 = "In order to do so, you will have to watch the population rate of each specie. If there are too many, you will have to relocate them abducting them into Pegi to send them into other planets of our C.R.A.P. net later. On the contrary, if their numbers are too low they will be an endangered specie and take longer to breed.";
    string text04 = "The balance between ecosystems is very important, and a limited number of animals may inhabit in them so resources are not depleted and the planet doesn't collapse running out of space.";
    string text05 = "Good luck, Roger-Bob. Do not fail, that planet depends on C.R.A.P.";

    int _indexText;

    void Start () {
        _text.text = text01;

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            string nextText = GetText(_indexText++);
            if (!string.IsNullOrEmpty(nextText))
                _text.text = nextText;
            else
                FinishIntro();
        }
    }

    private void FinishIntro()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }

    public string GetText(int text)
    {
        switch (text)
        {
            case 0: return text01;
            case 1: return text02;
            case 2: return text03;
            case 3: return text04;
            case 4: return text05;
        }
        return "";
    }
}
