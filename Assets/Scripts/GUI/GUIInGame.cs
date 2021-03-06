﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIInGame : MonoBehaviour {

    void Start()
    {
    }

    public  void Show()
    {
        Time.timeScale = 0;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }


    public void ClickInResume()
    {
        Hide();    
    }

    public void ClickOnRetry()
    {
        Hide();
        FindObjectOfType<GUIManager>().ClickInRetry();
    }


    public void ClickOnMainManu()
    {
        Hide();
        FindObjectOfType<GUIManager>().ClickOnMainManu();
    }
}
