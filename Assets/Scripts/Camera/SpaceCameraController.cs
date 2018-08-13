using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceCameraController : MonoBehaviour {

    PegiController _pegi;
    Camera _camera ;

    void Start()
    {
        _pegi = GameObject.FindGameObjectWithTag("Player").GetComponent<PegiController>();
        _pegi.OnChangeOrbit += OnChangeOrbit;

        _camera = GetComponent<Camera>();
        _camera.enabled = false;
    }

    private void OnChangeOrbit(int newOrbit)
    {
        _camera.enabled = PlanetController.IsSpace(newOrbit);
    }
}
