using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudInput : InputController {


    private void Start()
    {
        Momentum = Vector3.right * (Random.value > 0.5f ? 1 : -1);
    }

    
}
