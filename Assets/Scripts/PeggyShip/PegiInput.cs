using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PegiInput : MonoBehaviour {

    public Vector2 Momentum;

    void Update () {

        Momentum = new Vector2(0, 0);

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
    }

   
}
