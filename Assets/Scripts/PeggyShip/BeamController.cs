using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamController : MonoBehaviour
{

    const string AnimKeyBeamStart = "StartBeam";
    const string AnimKeyBeamFinish = "FinishBeam";

    Animator _animator;

    [SerializeField]
    GameObject _beamObj;
    [SerializeField]
    KeyChallengeController _keyChallenge;

    SpriteRenderer _sprite;

    PegiController _pegi;
    PegiInput _input;

    [SerializeField]
    Vector3 _originalPos;
    [SerializeField]
    Vector3 _rightPos;

    [Header("Abduction Animal Data")]
    [SerializeField]
    Transform _animalFinishPosition;
    [SerializeField]
    float _animalFinishAlpha;
    [SerializeField]
    float _animalFinishScale;


    Vector3 _animalStartPos;
    Color _animalInitialColor ;
    Vector3 _animalInitialScale ;

    OrbitMovement _orbitMovement;

    bool IsFacingRight
    {
        get
        {
            return _sprite.flipX;
        }
    }

    private void Start()
    {
        _beamObj.SetActive(false);
        _keyChallenge.gameObject.SetActive(false);

        _animator = _beamObj.GetComponentInChildren<Animator>();
        _sprite = transform.Find("Graphic").GetComponent<SpriteRenderer>();
        _input = GetComponent<PegiInput>();
        _pegi = GetComponent<PegiController>();


        GameObject.FindObjectOfType<LevelManager>().OnChangeAbductionState += OnChangeAbductionState;
    }

    private void OnChangeAbductionState(bool enabled)
    {
        StopAllCoroutines();
        if (enabled)
        {
            StartCoroutine(StartBeanAnimCo());
        }
        else
        {
            StartCoroutine(FinishBeamAnimCo());
        }
    }

    private IEnumerator StartBeanAnimCo()
    {
        _beamObj.transform.localPosition = IsFacingRight ? _rightPos : _originalPos;

        _beamObj.SetActive(true);
        _animator.SetBool(AnimKeyBeamStart, true);
        _animator.SetBool(AnimKeyBeamFinish, false);

        float timeToDisable = 0.8f;
        yield return new WaitForSeconds(timeToDisable);
        _keyChallenge.gameObject.SetActive(true);

        AnimalStartAbduction();

    }

    private IEnumerator FinishBeamAnimCo()
    {
        _animator.SetBool(AnimKeyBeamStart, false);
        _animator.SetBool(AnimKeyBeamFinish, true);
        AnimalFinishAbduction();

        float timeToDisable = 0.8f;
        yield return new WaitForSeconds(timeToDisable);
        _beamObj.SetActive(false);

        _keyChallenge.gameObject.SetActive(false);
    }

    private void AnimalStartAbduction()
    {
       
            
        StartCoroutine(AnimalAscensionCo());

    }

    private IEnumerator AnimalAscensionCo()
    {
        _animalStartPos = _pegi.AnimalBeingAbduced.transform.position;

        SpriteRenderer animalGraphic = _pegi.AnimalBeingAbduced.GetComponentInChildren<SpriteRenderer>();

        _animalInitialColor = animalGraphic.color;
        Color finalColor = new Color(1, 1, 1, _animalFinishAlpha);

        _animalInitialScale = _pegi.AnimalBeingAbduced.transform.localScale;
        Vector3 finalScale = Vector3.one * _animalFinishScale;

        float factor;
        while (true)
        {
            factor= _keyChallenge.CompleteFactor;
            _pegi.AnimalBeingAbduced.transform.position = Vector3.Lerp(_animalStartPos, _animalFinishPosition.position, factor);
            animalGraphic.color = Color.Lerp(_animalInitialColor, finalColor, factor);
            _pegi.AnimalBeingAbduced.transform.localScale = Vector3.Lerp(_animalInitialScale, finalScale, factor);
            yield return 0;
        }
    }

    private void AnimalFinishAbduction()
    {
        if (_pegi.AnimalBeingAbduced == null)
            return;

        _pegi.AnimalBeingAbduced.transform.position = _animalStartPos;
        SpriteRenderer animalGraphic = _pegi.AnimalBeingAbduced.GetComponentInChildren<SpriteRenderer>();
        animalGraphic.color = _animalInitialColor;
        _pegi.AnimalBeingAbduced.transform.localScale = _animalInitialScale;
    }
}
