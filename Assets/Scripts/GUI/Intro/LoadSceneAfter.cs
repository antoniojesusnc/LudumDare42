using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LoadSceneAfter : MonoBehaviour {

    [SerializeField]
    float _time;

    [SerializeField]
    Image _image;

    void Start () {
        Invoke("LoadScene", _time);

        LeanTween.alphaCanvas(_image.GetComponent<CanvasGroup>(), 0, _time * 0.85f);
	}
	
    void LoadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
    // Update is called once per frame
    void Update () {
		
	}
}
