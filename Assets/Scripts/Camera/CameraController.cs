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
    Vector3 _offsetAbducing;
    [SerializeField]
    [Range(0.01f, 1f)]
    float _rateNormal;
    [SerializeField]
    [Range(0.01f, 1f)]
    float _rateHigh;
    [SerializeField]
    [Range(0.01f, 1f)]
    float _rateAbducing;

    [SerializeField]
    float _zoomNormal;
    [SerializeField]
    float _zoomSpace;
    [SerializeField]
    float _zoomAbducing;
    [SerializeField]
    float _abducingAnimTime;


    Vector3 _currentOffset;
    float _currentRate;


    Camera _camera;
    PegiController _pegi;
    Vector3 _finalPos;
    Quaternion _finalRotation;

    LevelManager _level;
    void Start()
    {

        _level = GameObject.FindObjectOfType<LevelManager>();

        _camera = GetComponent<Camera>();
        _camera.orthographicSize = _zoomNormal;
        _pegi = GameObject.FindGameObjectWithTag("Player").GetComponent<PegiController>();
        _finalPos = new Vector3();

        _pegi.OnChangeOrbit += OnChangeOrbit;
        GameObject.FindObjectOfType<LevelManager>().OnChangeAbductionState += OnChangeAbducingState;
    }

    private void OnChangeAbducingState(bool abducing)
    {
        if (abducing)
        {
            StartCoroutine(SetCameraZoom(_zoomAbducing));
        }
        else
        {
            StartCoroutine(SetCameraZoom(_zoomNormal));
        }
    }

    private IEnumerator SetCameraZoom(float zoom)
    {
        float originalSize = _camera.orthographicSize;

        float timeStamp = 0;
        while(timeStamp < _abducingAnimTime)
        {
            _camera.orthographicSize = Mathf.Lerp( originalSize, zoom, timeStamp / _abducingAnimTime);
            yield return 0;
            timeStamp += Time.deltaTime;
        }

        _camera.orthographicSize = zoom;
    }

    private void OnChangeOrbit(int newOrbit)
    {
        if (newOrbit > 3)
        {
            _camera.orthographicSize = _zoomSpace;
        }
        else
        {
            _camera.orthographicSize = _zoomNormal;

            if (_pegi.IsInSpace)
            {
                _finalPos = _pegi.transform.right * _offsetPegi.x + _pegi.transform.up * _offsetPegi.y;
                transform.position = _finalPos;
                transform.rotation = _pegi.transform.rotation;
            }
        }
        _camera.enabled = !PlanetController.IsSpace(newOrbit);
    }

    void LateUpdate()
    {
        _currentOffset = _level.IsAbducting? _offsetAbducing:
            (_pegi.IsInSpace ? _offsetPegiHigh : _offsetPegi);

        _currentRate = _level.IsAbducting? _rateAbducing:
            (_pegi.IsInSpace ? _rateHigh : _rateNormal);

        _finalPos = _pegi.transform.right * _currentOffset.x + _pegi.transform.up * _currentOffset.y;
        transform.position = Vector3.Lerp(transform.position, _finalPos, _currentRate);
        transform.rotation = Quaternion.Lerp(transform.rotation, _pegi.transform.rotation, _currentRate);
    }
}
