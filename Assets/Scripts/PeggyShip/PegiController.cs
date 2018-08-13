using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PegiController : MonoBehaviour
{

    public event DelegateVoidFunctionIntParameter OnChangeOrbit;
    public event DelegateVoidFunctionBoolParameter OnShock;

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
    float _modSpeedWhenNormalCloud;
    [SerializeField]
    float _shockTime ;

    [SerializeField]
    float _orbitCoolDown;

    LevelManager _level;

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

    public bool IsInBotOrbit
    {
        get
        {
            return _currentOrbit <= 1;
        }
    }

    bool _canMove = true;
    public bool CanMove
    {
        get
        {
            return _canMove && !_level.IsAbducting;
        }
        set
        {
            _canMove = value;
        }
    }

    float _orbitTimeStamp;
    float _currentSpeed;

    public AnimalController AnimalBeingAbduced { get; private set; }

    PlanetController _planet;


    // shocking vars
    public bool IsInShock { get; private set; }

    void Start()
    {
        _level = GameObject.FindObjectOfType<LevelManager>();
        _currentSpeed = _speed;
        _planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<PlanetController>();
        _orbitMovement.Speed = _speed;
        _orbitMovement.CurrentOrbit = _currentOrbit;
        _orbitMovement.Acceleration = _acceleration;
        _orbitMovement.Decceleration = _decceleration;
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



    private void CheckShoot()
    {
        if (IsInBotOrbit && _input.Shooting)
        {
            //Debug.Log ("HI");
            RaycastHit HitInfo = new RaycastHit();
            Vector2 dir = (Vector2)(_planet.transform.position - transform.position).normalized;
            ContactFilter2D contact2D = new ContactFilter2D();
            //Debug.DrawLine(transform.position, transform.position+ (Vector3)dir*5,Color.blue, 5);
            ContactFilter2D filter = new ContactFilter2D();
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 5, 1 << LayerMask.NameToLayer("Animal"));
            if (hit.collider != null)
            {
                StartAbductionMode(hit.collider.GetComponentInParent<AnimalController>());
            }
        }
    }

    private void StartAbductionMode(AnimalController animalBeingAbduced)
    {
        _level.SetAbductionMode(true);
        AnimalBeingAbduced = animalBeingAbduced;
        AnimalBeingAbduced.StartAbduction();


    }

    private void CheckFinishAbduction()
    {
        if (_input.Shooting)
        {
            _level.SetAbductionMode(false);
            AnimalBeingAbduced.FinishAbduction();
        }
    }

    public void SusscessfulAbduction()
    {


        _level.AnimalAbducedSuccessFul(AnimalBeingAbduced);

        AnimalBeingAbduced = null;
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

    public void OnEnterInNormalCloud()
    {
        _orbitMovement.SpeedMod = _modSpeedWhenNormalCloud;
    }
    public void OnEnterInAngryCloud()
    {
        IsInShock = true;
        if (OnShock != null) OnShock(IsInShock);
        StopAllCoroutines();
        StartCoroutine(FinishShockDelayedCo());
    }

    private IEnumerator FinishShockDelayedCo()
    {
        yield return new WaitForSeconds(_shockTime);
        IsInShock = false;
        if (OnShock != null) OnShock(IsInShock);
    }

    public void OnExitNormalCloud()
    {
        _orbitMovement.SpeedMod = 0;
    }
}
