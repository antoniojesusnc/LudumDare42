using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUITopBar : MonoBehaviour
{
    [SerializeField]
    float _animTime;

    [Header("objs")]
    [SerializeField]
    RectTransform _lowLimit;
    [SerializeField]
    RectTransform _lowDangerLimit;
    [SerializeField]
    RectTransform _highDangerLimit;
    [SerializeField]
    RectTransform _highLimit;

    [Header("Animals")]
    [SerializeField]
    RectTransform _cowImage;
    [SerializeField]
    RectTransform _bearImage;
    [SerializeField]
    RectTransform _camelImage;
    [SerializeField]
    RectTransform _sharkImage;

    Dictionary<ETypeAnimal, AnimalPoblationInfo> _animalPoblationInfo;
    Dictionary<ETypeAnimal, RectTransform> _animalImages;
    Dictionary<ETypeAnimal, int> _lastValue;
    LevelManager _level;

    private void Start()
    {
        _level = LevelManager.Instance;

        _lastValue = new Dictionary<ETypeAnimal, int>();
        



        _animalPoblationInfo = new Dictionary<ETypeAnimal, AnimalPoblationInfo>();
        for (int i = _level.AnimalPoblation.Count - 1; i >= 0; i--)
        {
            _animalPoblationInfo.Add(_level.AnimalPoblation[i].AnimalType, _level.AnimalPoblation[i]);
            _lastValue.Add(_level.AnimalPoblation[i].AnimalType, 0);
        }

        _animalImages = new Dictionary<ETypeAnimal, RectTransform>();
        _animalImages.Add(ETypeAnimal.Cow, _cowImage);
        _animalImages.Add(ETypeAnimal.Shark, _sharkImage);
        _animalImages.Add(ETypeAnimal.Bear, _bearImage);
        _animalImages.Add(ETypeAnimal.Camel, _camelImage);

        UpdateBar(false);
    }

    private void Update()
    {
        UpdateBar();
    }

    private void UpdateBar(bool doAnim = true)
    {
        bool lowArea;
        bool middleArea;
        bool highArea;
        AnimalPoblationInfo animalInfo;

        ETypeAnimal animal;
        for (int i = (int)ETypeAnimal.Size - 1; i >= 0; i--)
        {
            animal = (ETypeAnimal)i;

            Debug.Log(animal + ": " + _level.AnimalAmount[animal]);
            animalInfo = _animalPoblationInfo[animal];

            lowArea = middleArea = highArea = false;

            if (_level.AnimalAmount[animal] < animalInfo.MinDangerAmount)
                lowArea = true;
            else if (_level.AnimalAmount[animal] < animalInfo.MaxDangerAmount)
                middleArea = true;
            else
                highArea = true;

            if (lowArea)
                SetImageInLine(_lowLimit, _lowDangerLimit, animalInfo.MinAmount, animalInfo.MinDangerAmount, _level.AnimalAmount[animal], animal, doAnim);
            else if (middleArea)
                SetImageInLine(_lowDangerLimit, _highDangerLimit, animalInfo.MinDangerAmount, animalInfo.MaxDangerAmount, _level.AnimalAmount[animal], animal, doAnim);
            else
                SetImageInLine(_highDangerLimit, _highLimit, animalInfo.MaxDangerAmount, animalInfo.MaxAmount, _level.AnimalAmount[animal], animal, doAnim);
        }
    }

    private void SetImageInLine(RectTransform minLimit, RectTransform maxLimit, int minAmount, int maxAmount, int currentAmont, ETypeAnimal animal, bool doAnim = true)
    {
        float factor = (float)(currentAmont - minAmount) / (float)(maxAmount - minAmount);
        float distance = maxLimit.anchoredPosition.x - minLimit.anchoredPosition.x;

        float distanceFromOrigin = distance * factor;
        float newPosition = minLimit.anchoredPosition.x + distanceFromOrigin;
        if(_lastValue[animal] != currentAmont) 
        {
            Vector2 finalPos = new Vector2(minLimit.anchoredPosition.x + distanceFromOrigin, _animalImages[animal].anchoredPosition.y);
            if (doAnim)
                LeanTween.move(_animalImages[animal], finalPos, _animTime);
            else
                _animalImages[animal].anchoredPosition = finalPos;

            _lastValue[animal] = currentAmont;
        }
        //rectTransform.anchoredPosition = new Vector2(minLimit.anchoredPosition.x + distanceFromOrigin, rectTransform.anchoredPosition.y);
    }
}
