using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalInput : InputController
{
    [Header("Movement Behavior")]
    [SerializeField]
    float _minTimeMovement;
    [SerializeField]
    float _maxTimeMovement;

    float _timeStamp;
    Vector2 _direction;

    public void Update()
    {
        _timeStamp -= Time.deltaTime;
        if (_timeStamp <= 0)
        {
            _timeStamp = Random.Range(_minTimeMovement, _maxTimeMovement);
            _direction = Vector2.right * (Random.Range(0.0f, 1.0f) < 0.5 ? -1 : 1);
        }
        Momentum = _direction;
    }
}
