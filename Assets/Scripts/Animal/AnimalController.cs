using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalController : MonoBehaviour {

    [Header("Animal Speed")]
    [SerializeField]
    float _minSpeed;

    [SerializeField]
    float _maxSpeed;

    [SerializeField]
    float _minTimeChangeDirection;

    [SerializeField]
    float _maxTimeChangeDirection;

    [Header("Others")]

    [SerializeField]
    ETypeAnimal _typeAnimal;

    [SerializeField]
    AnimalInput _input;
    [SerializeField]
    OrbitMovement _orbitMovement;


    void Start() {
        Init(_typeAnimal);
    }

    public void Init(ETypeAnimal typeAnimal) { 
        _orbitMovement.Speed = UnityEngine.Random.Range(_minSpeed, _maxSpeed);

        _typeAnimal = typeAnimal;

        _input.MinTimeMovement = _minTimeChangeDirection;
        _input.MaxTimeMovement = _maxTimeChangeDirection;
    }
    
    public ETypeAnimal GetAnimalType()
    {
        return _typeAnimal;
    }

    internal void UpdateAnimal()
    {
        
    }
}
