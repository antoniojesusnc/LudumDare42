using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamController : MonoBehaviour
{

    const string AnimKeyBeamStart = "StartBeam";
    const string AnimKeyBeamFinish = "FinishBeam";

    Animator _animator;

    [SerializeField]
    GameObject _beamObj;

    SpriteRenderer _sprite;

    PegiInput _input;

    [SerializeField]
    Vector3 _originalPos;
    [SerializeField]
    Vector3 _rightPos;

    OrbitMovement _orbitMovement;

    bool IsFacingRight
    {
        get
        {
            return _sprite.flipX;
        }
    }

    private void Start()
    {
        _beamObj.SetActive(false);
        _animator = _beamObj.GetComponentInChildren<Animator>();
        _sprite = transform.Find("Graphic").GetComponent<SpriteRenderer>();
        _input = GetComponent<PegiInput>();


        LevelManager.Instance.OnChangeAbductionState += OnChangeAbductionState;
    }

    private void Update()
    {

    }

    private void OnChangeAbductionState(bool enabled)
    {
        if (enabled)
        {
            StartBeanAnim();
        }
        else
        {
            FinishBeamAnim();
        }
    }

    private void StartBeanAnim()
    {
        _beamObj.transform.localPosition = IsFacingRight ? _rightPos : _originalPos;

        _beamObj.SetActive(true);
        _animator.SetBool(AnimKeyBeamStart, true);
        _animator.SetBool(AnimKeyBeamFinish, false);
    }

    private void FinishBeamAnim()
    {
        _animator.SetBool(AnimKeyBeamStart, false);
        _animator.SetBool(AnimKeyBeamFinish, true);
    }
}
