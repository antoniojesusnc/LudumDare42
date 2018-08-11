using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PegiAnimator : MonoBehaviour
{
    [SerializeField]
    AnimationClip _turnAnimation;
    const string AnimKeyTurnRight = "TurnRight";
    const string AnimKeyTurnLeft = "TurnLeft";

    [SerializeField]
    AnimationClip _movingAnimation;
    const string KeyMovement = "Move";

    PegiInput _input;
    SpriteRenderer _sprite;
    Animator _animator;

    AnimatorStateInfo _infoState;

    void Start()
    {
        _sprite = GetComponentInChildren<SpriteRenderer>();
        _input = GetComponentInChildren<PegiInput>();
        _animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _animator.SetBool(AnimKeyTurnRight, _input.IsPressingRight);
        _animator.SetBool(AnimKeyTurnLeft, _input.IsPressingLeft);
    }

    public void Temp() { Debug.Log("Temp"); }

    void Flip(bool flip)
    {
        _sprite.flipX = flip;
    }
}
