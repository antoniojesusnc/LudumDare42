using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalInput : InputController
{
    public float MinTimeMovement { get; set; }
    [SerializeField]
    public float MaxTimeMovement { get; set; }

    float _timeStamp;
    Vector2 _direction;

    AnimalController _animal;

    private void Start()
    {
        _animal = GetComponent<AnimalController>();
    }

    public void Update()
    {
        Momentum = new Vector2();
        if (_animal != null && _animal.BeingAbduced)
            return;

        _timeStamp -= Time.deltaTime;
        if (_timeStamp <= 0)
        {
            _timeStamp = Random.Range(MinTimeMovement, MaxTimeMovement);
            _direction = Vector2.right * (Random.value < 0.5 ? -1 : 1);
        }
        Momentum = _direction;
    }
}
