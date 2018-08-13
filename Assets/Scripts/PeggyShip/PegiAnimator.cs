using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PegiAnimator : MonoBehaviour
{
    const string AnimKeyRight = "PressingRight";
    const string AnimKeyLeft = "PressingLeft";
    const string AnimKeyPressNothing = "PressingNothing";
    const string AnimKeyIsInShock = "IsInShock";
    

    PegiController _pegi;
    PegiInput _input;
    Animator _animator;

    LevelManager _level;
    SpriteRenderer _graphic;

    void Start()
    {
        _input = GetComponentInChildren<PegiInput>();
        _animator = transform.Find("Graphic").GetComponentInChildren<Animator>();

        _level = GameObject.FindObjectOfType<LevelManager>();
        _pegi = GetComponent<PegiController>();
        _pegi.OnShock += OnShock;

        _pegi.OnChangeOrbit += OnChangeOrbit;
        _graphic = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnChangeOrbit(int newOrbit)
    {
        _graphic.enabled = !PlanetController.IsSpace(newOrbit);
    }

    private void OnShock(bool boolean)
    {
        //_animator.lay
    }

    void Update()
    {
        if (_level.IsAbducting)
            return;

        _animator.SetBool(AnimKeyRight, _input.IsPressingRight);
        _animator.SetBool(AnimKeyLeft, _input.IsPressingLeft);
        _animator.SetBool(AnimKeyPressNothing, !_input.IsPressingRight && !_input.IsPressingLeft);

        //if(_pegi.IsInShock)  
        _animator.SetBool(AnimKeyIsInShock, _pegi.IsInShock);
    }
}
