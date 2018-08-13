using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalAnim : MonoBehaviour
{

    SpriteRenderer _sprite;

    [SerializeField]
    float _forceMin;
    [SerializeField]
    float _forceMax;

    [SerializeField]
    float _durationMin;
    [SerializeField]
    float _durationMax;

    [Header("Jump")]
    [SerializeField]
    float _jumpHeigh;
    [SerializeField]
    float _jumpDuration;
    [SerializeField]
    float _jumpFrecuencyMinTime;
    [SerializeField]
    float _jumpFrecuencyMaxTime;


    [SerializeField]
    GameObject _heart;

    AnimalController _animal;
    Vector3 _originalLocalPosition;

    void Start()
    {
        _animal = GetComponent<AnimalController>();
        _animal.OnBeingAbduced += OnBeingAbduced;
        _animal.OnIsReproducing += OnIsReproducing;
        _sprite = GetComponentInChildren<SpriteRenderer>();
        _originalLocalPosition = _sprite.transform.localPosition;


        _heart.SetActive(_animal.IsReproducing);

        if (!_animal.IsReproducing)
        {
            StartAnim();
        }
        else
        {
        }
    }

    void StartAnim()
    {
        CancelAnims();

        float duration = UnityEngine.Random.Range(_durationMin, _durationMax);
        float force = UnityEngine.Random.Range(_forceMin, _forceMax);

        LeanTween.rotateAroundLocal(_sprite.gameObject, Vector3.forward, force, duration)
                .setEase(LeanTweenType.easeShake) // this is a special ease that is good for shaking
                .setLoopClamp();

        StartCoroutine(JumpCo());
    }

    private IEnumerator JumpCo()
    {
        while (true)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(_jumpFrecuencyMinTime, _jumpFrecuencyMaxTime));
            LeanTween.moveLocalY(_sprite.gameObject, _jumpHeigh, _jumpDuration).setLoopPingPong(1).setEase(LeanTweenType.easeOutCubic);
        }
    }

    private void CancelAnims()
    {
        LeanTween.cancel(_sprite.gameObject);
        StopAllCoroutines();

        LeanTween.moveLocal(_sprite.gameObject, _originalLocalPosition, 0.1f);
        _sprite.transform.localRotation = Quaternion.identity;
    }

    private void OnBeingAbduced(bool beingAbduced)
    {
        if (beingAbduced)
        {
            CancelAnims();
            _sprite.transform.localRotation = Quaternion.Euler(0, 0, (_sprite.flipX ? -1 : 1) * 30);
        }
        else
        {
            StartAnim();
        }
    }


    private void OnIsReproducing(bool isReproducing)
    {
        if (isReproducing)
        {
            CancelAnims();
            StartCoroutine(ReprocutionAnimCo());
        }
        else
        {
            StartAnim();
        }
        _heart.SetActive(isReproducing);
    }

    private IEnumerator ReprocutionAnimCo()
    {
        LeanTween.rotateAroundLocal(_sprite.gameObject, Vector3.forward, _forceMax, _durationMin)
                .setEase(LeanTweenType.easeShake) // this is a special ease that is good for shaking
                .setLoopClamp();

        yield return new WaitForSeconds(_animal.ReproductionTime);

        CancelAnims();
    }
}

