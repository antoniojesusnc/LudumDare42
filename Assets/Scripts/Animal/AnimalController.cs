using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalController : MonoBehaviour {

    [SerializeField]
    InputController _input;
    [SerializeField]
    OrbitMovement _orbitMovement;

    [SerializeField]
    float _speed;

   

    void Start () {
        _orbitMovement.Speed = _speed;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
