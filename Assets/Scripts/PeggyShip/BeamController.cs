using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamController : MonoBehaviour {

    const string AnimKeyBeamStart = "StartBeam";
    const string AnimKeyBeamFinish = "FinishBeam";

    Animator _animator;

    [SerializeField]
    GameObject _beamObj;

    private void Start()
    {
        _beamObj.SetActive(false);
        _animator = _beamObj.GetComponentInChildren<Animator>();
        LevelManager.Instance.OnChangeAbductionState += OnChangeAbductionState;
    }

    private void OnChangeAbductionState(bool enabled)
    {
        if (enabled) {
            StartBeanAnim();
        }
        else
        {
            FinishBeamAnim();
        }
    }

    private void StartBeanAnim()
    {
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
