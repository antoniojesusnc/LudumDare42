using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIMainMenu : MonoBehaviour {

    Vector3 _originalPos;

    [SerializeField]
    Transform _camera;

    [SerializeField]
    Transform _cameraFirstPosition;
    [SerializeField]
    float _timeFirstPos;
    [SerializeField]
    Transform _cameraSecondPosition;
    [SerializeField]
    float _timeSecondPos;

    private void Start()
    {
        _originalPos = _camera.position;
    }
    public void ClickOnNewGame()
    {
        //LeanTween.move(_camera.gameObject, _cameraFirstPosition.position, _timeFirstPos);
        UnityEngine.SceneManagement.SceneManager.LoadScene(3);
        
    }

    public void ClickONExit()
    {
        Application.Quit();
    }

    public void ClickInAudio()
    {
        
    }


}

