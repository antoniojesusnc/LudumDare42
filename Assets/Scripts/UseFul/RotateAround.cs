using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour {

    [SerializeField]
    float _angularVelocity;

	void Start () {
		
	}
	
	void Update () {
        transform.localRotation = Quaternion.Euler(0, 0, transform.localRotation.eulerAngles.z + _angularVelocity * Time.deltaTime);
    }
}
