using System;
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

    private void Start()
    {
        _orbit = GetComponent<OrbitMovement>();
        LevelManager.Instance.OnChangeAbductionState += OnChangeAbductionState;

    }

    private void OnChangeAbductionState(bool enabledAbduction)
    {
        _orbit.enabled = !enabledAbduction;
    }
    void Update()
    {
        ResetVars();

        float temp = Input.GetAxisRaw("HorizontalPegi");
        if (temp != 0)
        {
            Momentum += temp * Vector2.right;
            IsPressingRight = Momentum.x > 0f ;
            IsPressingLeft = Momentum.x < 0f;
        }

        temp = Input.GetAxisRaw("Vertical");
        if (temp != 0)
        {
            Momentum += temp * Vector2.up;
        }

        //float action = Input.GetkeyRaw("Jump");
        //if (action != 0)
        bool action = Input.GetKeyDown(KeyCode.Space);
        if (action)
        {
            Shooting = true;
        }
             temp = Input.GetAxisRaw("HorizontalPegi");
        if(temp != 0)
        {
            Debug.Log(temp);
        }
    }


    private void ResetVars()
    {
        Momentum = new Vector2(0, 0);
        Shooting = false;

        IsPressingRight = false;
        IsPressingLeft = false;
    }
}
