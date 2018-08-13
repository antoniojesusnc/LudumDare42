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
    bool _useInertia;
    [SerializeField]
    public float Acceleration { get; set; }
    [SerializeField]
    public float Decceleration { get; set; }

    float _speed;
    public float Speed
    {
        get
        {
            return _speed * (SpeedMod == 0?1:SpeedMod);
        }
        set
        {
            _speed = value;
        }
    }

    public float SpeedMod{ get; set; }

    float _currentSpeed;

    public int CurrentOrbit { get; set; }

    PlanetController _planet;
    public bool AnimChangeOrbit { get; set; }

    public bool IsDisable { get; set; }

    void Start()
    {
        _planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<PlanetController>();
        GameObject.FindObjectOfType<LevelManager>().OnChangeAbductionState += OnChangeAbductionState;
    }

    private void OnChangeAbductionState(bool enabled)
    {
        _currentSpeed = 0;
    }

    void LateUpdate()
    {
        if (IsDisable)
            return;

        if (_flipIfRight)
            CheckFlip();

        if (_useInertia)
        {
            _currentSpeed += Acceleration * _input.Momentum.x ;
            _currentSpeed = Mathf.Clamp(_currentSpeed, -Speed, Speed);
        }
        else
        {
            _currentSpeed = _input.Momentum.x * Speed ;

        }

        transform.position += transform.right * _currentSpeed * Time.deltaTime;
        SetOrbitPosition();

        if (_useInertia)
        {
            _currentSpeed *= Decceleration;
        }
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
