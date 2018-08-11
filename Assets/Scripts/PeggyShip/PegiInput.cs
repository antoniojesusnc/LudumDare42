using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PegiInput : InputController
{
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
        Momentum = new Vector2(0, 0);
        Shooting = false;


        float temp = Input.GetAxis("Horizontal");
        if (temp != 0)
        {
            Momentum += temp * Vector2.right;
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
    }
}
