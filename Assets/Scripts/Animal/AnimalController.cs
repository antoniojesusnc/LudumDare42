using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalController : MonoBehaviour
{

    [Header("Animal Properties")]
    [SerializeField]
    float _minSpeed;

    [SerializeField]
    float _maxSpeed;

    [SerializeField]
    float _minTimeChangeDirection;

    [SerializeField]
    float _maxTimeChangeDirection;

    [SerializeField]
    float _reproductionJumpDistance;

    [Header("Others")]

    [SerializeField]
    ETypeAnimal _typeAnimal;

    [SerializeField]
    AnimalInput _input;

    [SerializeField]
    OrbitMovement _orbitMovement;

    PlanetController _planet;

    public event DelegateVoidFunctionBoolParameter OnBeingAbduced;

    bool _beingAbduced;
    public bool BeingAbduced
    {
        get
        {
            return _beingAbduced;
        }
        set
        {
            _beingAbduced = value;
            if (OnBeingAbduced != null)
                OnBeingAbduced(value);

        }
    }


    public event DelegateVoidFunctionBoolParameter OnIsReproducing;

    bool _isReproducing;
    public bool IsReproducing
    {
        get
        {
            return _isReproducing;
        }
        set
        {
            _isReproducing = value;
            if (OnIsReproducing != null)
                OnIsReproducing(value);
            _orbitMovement.enabled = !_isReproducing;

        }
    }

    [SerializeField]
    float _reproductionTime;
    public float ReproductionTime
    {
        get { return _reproductionTime; }
        set { _reproductionTime = value; }
    }

    [SerializeField]
    float _reproductionJumpTime;
    public float ReproductionJumpTime
    {
        get { return _reproductionJumpTime; }
        set { _reproductionJumpTime = value; }
    }

    void Start()
    {
        Init(_typeAnimal);

        _planet = GameObject.FindObjectOfType<PlanetController>();
    }

    public void Init(ETypeAnimal typeAnimal)
    {
        _orbitMovement.Speed = UnityEngine.Random.Range(_minSpeed, _maxSpeed);

        _typeAnimal = typeAnimal;

        _input.MinTimeMovement = _minTimeChangeDirection;
        _input.MaxTimeMovement = _maxTimeChangeDirection;

        if (IsReproducing)
        {
            // DoJumpReproduction(false);
        }
    }

    public void DoJumpReproduction(bool jumpToRight)
    {
        Vector3 jumpSide = (jumpToRight ? transform.right : -transform.right) * _reproductionJumpDistance;
        Vector3 jumpUP = transform.up * _reproductionJumpDistance + jumpSide;

        var moveUp = LeanTween.move(gameObject, transform.position + jumpUP, ReproductionJumpTime * 0.5f)
               .setEase(LeanTweenType.easeOutQuad);
        var moveSide = LeanTween.move(gameObject, transform.position + jumpSide * 2, ReproductionJumpTime * 0.5f)
               .setEase(LeanTweenType.easeOutQuad);
        var sequence = LeanTween.sequence();
        sequence.append(moveUp);
        sequence.append(moveSide);
    }

    public ETypeAnimal GetAnimalType()
    {
        return _typeAnimal;
    }

    public void UpdateAnimal()
    {

    }

    private void LateUpdate()
    {
        _planet.IsInArea(this);
    }

    public void ChangeDirection()
    {
        GetComponent<AnimalInput>().NewDirection();
    }

    public void StartAbduction()
    {
        BeingAbduced = true;
        _orbitMovement.enabled = false;
    }

    public void FinishAbduction()
    {
        BeingAbduced = false;
        _orbitMovement.enabled = true;
    }
}
