using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIInventoryInput : InputController
{

    float _temp;

    void Update()
    {
        Momentum.Set(0, 0);
        MomentumDown.Set(0, 0);

        _temp = Input.GetAxisRaw("HorizontalPegi");
        if (_temp != 0)
        {
            Momentum += _temp * Vector2.right;
        }

        _temp = Input.GetAxisRaw("Vertical");
        if (_temp != 0)
        {
            Momentum += _temp * Vector2.up;
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
}
