using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
{

    [SerializeField]
    float _surface;

    [SerializeField]
    float _orbit01;
    [SerializeField]
    float _orbit02;
    [SerializeField]
    float _orbit03;
    [SerializeField]
    float _space;
    [SerializeField]
    float _angularVelocity;

    bool _canRotate = true;

    public float GetOrbitPosition(int orbit)
    {
        switch (orbit)
        {
            case 0: return _surface;
            case 1: return _orbit01;
            case 2: return _orbit02;
            case 3: return _orbit03;
            case 4: return _space;
        }
        return _orbit01;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _surface);
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(transform.position, _orbit01);
        Gizmos.DrawWireSphere(transform.position, _orbit02);
        Gizmos.DrawWireSphere(transform.position, _orbit03);
        Gizmos.DrawWireSphere(transform.position, _space);
    }

    private void Start()
    {
        GameObject.FindObjectOfType<LevelManager>().OnChangeAbductionState += OnChangeAbductionState;
    }

    private void OnChangeAbductionState(bool boolean)
    {
        _canRotate = !boolean;
    }

    public void Update()
    {
        if(_canRotate)
            transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + _angularVelocity * Time.deltaTime);
    }
}
