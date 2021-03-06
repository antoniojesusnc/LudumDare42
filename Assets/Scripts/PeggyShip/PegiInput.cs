﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PegiInput : InputController
{
    // key binding
    public bool IsPressingRight { get; private set; }
    public bool IsPressingLeft { get; private set; }


    public bool Shooting;

    OrbitMovement _orbit;
    PegiController _pegi;
    float _temp;

    private void Start()
    {
        _orbit = GetComponent<OrbitMovement>();
        GameObject.FindObjectOfType<LevelManager>().OnChangeAbductionState += OnChangeAbductionState;
        _pegi = GetComponent<PegiController>();

    }

    private void OnChangeAbductionState(bool enabledAbduction)
    {
        _orbit.enabled = !enabledAbduction;
    }
    void Update()
    {
        ResetVars();

        if (_pegi.IsInShock )
            return;

        if (!_pegi.IsInSpace)
        {
            _temp = Input.GetAxisRaw("HorizontalPegi");
            if (_temp != 0)
            {
                Momentum += _temp * Vector2.right;
                IsPressingRight = Momentum.x > 0f;
                IsPressingLeft = Momentum.x < 0f;
            }
        }

        _temp = Input.GetAxisRaw("Vertical");
        if (_temp != 0)
        {
            Momentum += _temp * Vector2.up;
        }

        //float action = Input.GetkeyRaw("Jump");
        //if (action != 0)
        bool action = Input.GetKeyDown(KeyCode.Space);
        if (action)
        {
            Shooting = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            MomentumDown.Set(-1, MomentumDown.y);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            MomentumDown.Set(1, MomentumDown.y);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            MomentumDown.Set(MomentumDown.x, 1);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            MomentumDown.Set(MomentumDown.x, -1);
        }
    }

    private void ResetVars()
    {
        Momentum = new Vector2(0, 0);
        MomentumDown = new Vector2(0, 0);

        Shooting = false;

        IsPressingRight = false;
        IsPressingLeft = false;
    }
}

