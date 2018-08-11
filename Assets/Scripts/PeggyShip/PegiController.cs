using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PegiController : MonoBehaviour
{

    public event DelegateVoidFunctionIntParameter OnChangeOrbit;
    [SerializeField]
    PegiInput _input;

    [SerializeField]
    OrbitMovement _orbitMovement;

    [SerializeField]
    float _speed;
    [SerializeField]
    float _acceleration;
    [SerializeField]
    float _decceleration;

    [SerializeField]
    float _orbitCoolDown;

    int _currentOrbit = 1;
    public int CurrentObit
    {
        get
        {
            return _currentOrbit;
        }
    }
    public bool IsInSpace
    {
        get
        {
            return _currentOrbit >= 4;
        }
    }

    bool _canMove = true;
    public bool CanMove
    {
        get
        {
            return _canMove && !LevelManager.Instance.IsAbducting;
        }
        set
        {
            _canMove = value;
        }
    }

    float _orbitTimeStamp;
    float _currentSpeed;
    bool _animChangeOrbit;

    PlanetController _planet;

    void Start()
    {
        _currentSpeed = _speed;
        _planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<PlanetController>();
        _orbitMovement.Speed = _speed;
        _orbitMovement.CurrentOrbit = _currentOrbit;
    }

    void Update()
    {
        if (CanMove)
        {
            SetOrbitPosition();
            CheckShoot();
        }
        else
        {
            CheckFinishAbduction();
            CheckInputChallenge();
        }
    }

    private void CheckInputChallenge()
    {

    }

    private void CheckFinishAbduction()
    {
        if (_input.Shooting)
        {
            LevelManager.Instance.SetAbductionMode(false);
        }


    }

    private void CheckShoot()
    {
        if (_input.Shooting)
        {
            //Debug.Log ("HI");
            RaycastHit HitInfo = new RaycastHit();
            Vector2 dir = (Vector2)(_planet.transform.position - transform.position).normalized;
            ContactFilter2D contact2D = new ContactFilter2D();
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir);
            if (hit.collider != null)
            {
                LevelManager.Instance.SetAbductionMode(true);

            }
        }
    }

    private void SetOrbitPosition()
    {
        _orbitTimeStamp -= Time.deltaTime;
        if (_input.Momentum.y != 0 && _orbitTimeStamp <= 0)
        {
            int newOrbit = _currentOrbit + (int)_input.Momentum.y;
            newOrbit = Mathf.Clamp(newOrbit, 1, 4);
            if (newOrbit != _currentOrbit)
            {
                _orbitTimeStamp = _orbitCoolDown;

                if (OnChangeOrbit != null)
                    OnChangeOrbit(newOrbit);

                _currentOrbit = newOrbit;
                _orbitMovement.CurrentOrbit = _currentOrbit;

                if (!IsInSpace)
                {
                    StartAnimOrbitPosition();

                }
            }
        }
    }

    void StartAnimOrbitPosition()
    {
        float animTime = _orbitTimeStamp;

        LeanTween.move(gameObject, transform.up.normalized * _planet.GetOrbitPosition(_currentOrbit), animTime);
        _orbitMovement.AnimChangeOrbit = true;
        LeanTween.delayedCall(animTime, FinishAnimOrbitPosition);
    }

    void FinishAnimOrbitPosition()
    {
        _orbitMovement.AnimChangeOrbit = false;
    }
}
