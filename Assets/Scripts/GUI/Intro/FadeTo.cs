using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FadeTo : MonoBehaviour
{


    [SerializeField]
    float _delayTime;

    [SerializeField]
    float _time;

    [SerializeField]
    float _fadeTo;

    [SerializeField]
    SpriteRenderer _spriteRenderer;

    [SerializeField]
    CanvasGroup _canvas;

    void Start()
    {
        if (_delayTime > 0)
            Invoke("Fade", _delayTime);
        else
            Fade();
    }
    void Fade()
    {
        if (_spriteRenderer != null)
            LeanTween.alpha(_spriteRenderer.gameObject, _fadeTo, _time);

        if (_canvas != null)
            LeanTween.alphaCanvas(_canvas, _fadeTo, _time);


    }

}
