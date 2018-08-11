using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalController : MonoBehaviour {

    [SerializeField]
    ETypeAnimal _typeAnimal;

    [SerializeField]
    InputController _input;
    [SerializeField]
    OrbitMovement _orbitMovement;

    [SerializeField]
    float _speed;

    void Start() {
        Init(_typeAnimal);
    }

    public void Init(ETypeAnimal typeAnimal) { 
        _orbitMovement.Speed = _speed;

        _typeAnimal = typeAnimal;
    }
	
    public ETypeAnimal GetAnimalType()
    {
        return _typeAnimal;
    }

    internal void UpdateAnimal()
    {
        
    }
}
