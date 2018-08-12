using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PegiAnimator : MonoBehaviour
{
    const string AnimKeyRight = "PressingRight";
    const string AnimKeyLeft = "PressingLeft";
    const string AnimKeyPressNothing = "PressingNothing";

    PegiInput _input;
    Animator _animator;

    void Start()
    {
        _input = GetComponentInChildren<PegiInput>();
        _animator = transform.Find("Graphic").GetComponentInChildren<Animator>();
    }

    void Update()
    {
        _animator.SetBool(AnimKeyRight, _input.IsPressingRight);
        _animator.SetBool(AnimKeyLeft, _input.IsPressingLeft);
        _animator.SetBool(AnimKeyPressNothing, !_input.IsPressingRight && !_input.IsPressingLeft);
    }
}
