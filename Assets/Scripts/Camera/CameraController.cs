using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField]
    Vector3 _offsetPegi;
    [SerializeField]
    Vector3 _offsetPegiHigh;
    [SerializeField]
    [Range(0.01f, 1f)]
    float _rateNormal;
    [SerializeField]
    [Range(0.01f, 1f)]
    float _rateHigh;


    [SerializeField]
    float _zoomNormal;
    [SerializeField]
    float _zoomHigh;



    Vector3 _currentOffset;
    float _currentRate;


    Camera _camera;
    PegiController _pegi;
    Vector3 _finalPos;
    Quaternion _finalRotation;
    [SerializeField]
    GameObject _pegiGraphic;

    void Start()
    {
        _camera = GetComponent<Camera>();
        _camera.orthographicSize = _zoomNormal;
        _pegi = GameObject.FindGameObjectWithTag("Player").GetComponent<PegiController>();
        _finalPos = new Vector3();

        _pegi.OnChangeOrbit += OnChangeOrbit;
    }

    private void OnChangeOrbit(int newOrbit)
    {
        if (newOrbit > 3)
        {
            _camera.orthographicSize = _zoomHigh;
            _pegiGraphic.SetActive(true);
        }
        else
        {
            _camera.orthographicSize = _zoomNormal;
            _pegiGraphic.SetActive(false);

            if (_pegi.IsInSpace)
            {
                _finalPos = _pegi.transform.right * _offsetPegi.x + _pegi.transform.up * _offsetPegi.y;
                transform.position = _finalPos;
                transform.rotation = _pegi.transform.rotation;
            }
        }
    }

    void LateUpdate()
    {
        _currentOffset = _pegi.IsInSpace ? _offsetPegiHigh : _offsetPegi;
        _currentRate = _pegi.IsInSpace ? _rateHigh : _rateNormal;

        _finalPos = _pegi.transform.right * _currentOffset.x + _pegi.transform.up * _currentOffset.y;
        transform.position = Vector3.Lerp(transform.position, _finalPos, _currentRate);
        transform.rotation = Quaternion.Lerp(transform.rotation, _pegi.transform.rotation, _currentRate);
    }
}
