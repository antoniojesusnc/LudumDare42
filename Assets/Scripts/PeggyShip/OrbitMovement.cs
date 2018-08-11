using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitMovement : MonoBehaviour {

    [SerializeField]
    InputController _input;

    public float Speed { get; set; }
    float _currentSpeed;

    int _currentOrbit = 0;

    PlanetController _planet;

	void Start () {
        _currentSpeed = Speed;
        _planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<PlanetController>();
    }
	
	void LateUpdate () {
        transform.position += _input.Momentum.x * _currentSpeed * transform.right* Time.deltaTime;
        SetOrbitPosition();
    }

    private void SetOrbitPosition()
    {
        float orbitPosition = _planet.GetOrbitPosition(_currentOrbit);
        Vector3 dirToPlanet = (transform.position - _planet.transform.position ).normalized;
        transform.position =  dirToPlanet* orbitPosition;
        transform.up = dirToPlanet;
    }
}
