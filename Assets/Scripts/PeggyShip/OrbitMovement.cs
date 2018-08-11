using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitMovement : MonoBehaviour
{

    [SerializeField]
    InputController _input;

    [SerializeField]
    bool _flipIfRight;

    [SerializeField]
    bool _userInertia;
    [SerializeField]
    public float Acceleration { get; set; }
    [SerializeField]
    public float Decceleration { get; set; }

    float _speed;
    public float Speed
    {
        get
        {
            return _speed;
        }
        set
        {
            _speed = value;
            _currentSpeed = value;
        }
    }
    float _currentSpeed;

    public int CurrentOrbit { get; set; }

    PlanetController _planet;
    public bool AnimChangeOrbit { get; set; }

    void Start()
    {
        _currentSpeed = Speed;
        _planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<PlanetController>();
    }

    void LateUpdate()
    {
        if (_flipIfRight)
            CheckFlip();

        transform.position += _input.Momentum.x * _currentSpeed * transform.right * Time.deltaTime;
        SetOrbitPosition();
    }

    private void CheckFlip()
    {
        foreach (var spriteRender in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            if (_input.Momentum.x != 0)
                spriteRender.flipX = _input.Momentum.x > 0;
        }
    }

    private void SetOrbitPosition()
    {
        if (!AnimChangeOrbit)
        {
            float orbitPosition = _planet.GetOrbitPosition(CurrentOrbit);
            Vector3 dirToPlanet = (transform.position - _planet.transform.position).normalized;

            transform.position = dirToPlanet * orbitPosition;
            transform.up = dirToPlanet;
        }
    }
}
