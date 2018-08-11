using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PegiController : MonoBehaviour {

    [SerializeField]
    PegiInput _input;

    [SerializeField]
    float _speed;

    [SerializeField]
    float _orbitCoolDown;

    int _currentOrbit = 1;
    float _orbitTimeStamp;
    float _currentSpeed;

    PlanetController _planet;

	void Start () {
        _currentSpeed = _speed;

        _planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<PlanetController>();
    }
	
	void LateUpdate () {
        transform.position += _input.Momentum.x * _speed * transform.right* Time.deltaTime;
        SetOrbitPosition();
    }

    private void SetOrbitPosition()
    {
        _orbitTimeStamp -= Time.deltaTime;
        if (_input.Momentum.y != 0 && _orbitTimeStamp <= 0)
        {

            int newOrbit = _currentOrbit + (int)_input.Momentum.y;
            newOrbit = Mathf.Clamp(newOrbit, 1, 3);
            if(newOrbit != _currentOrbit)
            {
                _currentOrbit = newOrbit;
                _orbitTimeStamp = _orbitCoolDown;
            }
        }

        float orbitPosition = _planet.GetOrbitPosition(_currentOrbit);

        Vector3 dirToPlanet = (transform.position - _planet.transform.position ).normalized;

        transform.position =  dirToPlanet* orbitPosition;

        transform.up = dirToPlanet;
    }
}
