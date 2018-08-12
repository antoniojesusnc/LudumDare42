using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour
{

    const string AnimKeyCloudID = "CloudID";
    const string AnimKeyStorm = "Storm";

    OrbitMovement _orbitMov;
    Animator _animator;


    public void Init(int orbit, int cloudID, float timeNormal, float timeAngry, float speed)
    {
        _orbitMov = GetComponent<OrbitMovement>();

        _orbitMov.Speed = speed;
        _orbitMov.CurrentOrbit = orbit;

        _animator = GetComponentInChildren<Animator>();
        _animator.SetInteger(AnimKeyCloudID, cloudID);

        StartCoroutine(BecomeAngryCo(timeNormal));
    }

    private IEnumerator BecomeAngryCo(float timeNormal)
    {
        yield return new WaitForSeconds(timeNormal);
        _animator.SetBool(AnimKeyStorm, true);
    }
}
