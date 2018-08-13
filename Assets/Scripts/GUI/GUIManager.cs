using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : SingletonMonoBehaviour<GUIManager>
{

    [SerializeField]
    GUIInGame _guiVictory;
    [SerializeField]
    GUIInGame _guiLose;
    [SerializeField]
    GUIInGame _guiPause;

    private void Start()
    {
        Time.timeScale = 1;
    }
    public void OpenVictoryGUI()
    {
        _guiVictory.Show();
    }


    public void OpenLoseGUI()
    {
        _guiLose.Show();
    }
    public void OpenPauseGUI()
    {
        _guiPause.Show();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_guiPause.gameObject.activeSelf)
            {
                _guiPause.Hide();
            }
            else
            {
                OpenPauseGUI();
            }
        }
    }


    void DestroyAllSingleton()
    {
        DestroyImmediate(gameObject);
    }

    public void ClickInRetry()
    {
        DestroyAllSingleton();
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        //UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
    public void ClickOnMainManu()
    {
        DestroyAllSingleton();
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
