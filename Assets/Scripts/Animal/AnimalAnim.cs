using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalAnim : MonoBehaviour {

    SpriteRenderer _sprite;

    [SerializeField]
    float _forceMin;
    [SerializeField]
    float _forceMax;

    [SerializeField]
    float _durationMin;
    [SerializeField]
    float _durationMax;

    void Start () {
        _sprite = GetComponentInChildren<SpriteRenderer>();
        StartAnim();
    }

    void StartAnim()
    {
        float duration = UnityEngine.Random.Range(_durationMin, _durationMax);
        float force = UnityEngine.Random.Range(_forceMin, _forceMax);

        LTDescr shakeTween = LeanTween.rotateAroundLocal(_sprite.gameObject, Vector3.forward, force, duration)
                .setEase(LeanTweenType.easeShake) // this is a special ease that is good for shaking
                .setLoopClamp();
    }

    void Update () {
    }
}

