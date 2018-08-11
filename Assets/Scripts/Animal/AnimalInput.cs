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

    bool _beingAbducted;

    public void Update()
    {
        Momentum = new Vector2();

        _timeStamp -= Time.deltaTime;
        if (_timeStamp <= 0)
        {
            _timeStamp = Random.Range(_minTimeMovement, _maxTimeMovement);
            _direction = Vector2.right * (Random.value < 0.5 ? -1 : 1);
        }
        Momentum = _direction;
    }
}
