using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    [SerializeField]
    Transform _animalParent;

    [SerializeField]
    List<AnimalPoblationInfo> _animalPoblation;

    AnimalManager _animalManager;

    public List<AnimalController> _animalPrefabs;


    void Start()
    {
        StartLevel();
    }

    void StartLevel()
    {
        GemerateStartedAnimals();

    }

    private void GemerateStartedAnimals()
    {
        AnimalController animal;
        int animalAmount;
        for (int i = _animalPoblation.Count - 1; i >= 0; --i)
        {
            animal = GetPrefab(_animalPoblation[i].AnimalType);
            if(animal == null)
            {
                Debug.LogWarning("Animal "+_animalPoblation[i].AnimalType+" not found in prefabs");
                continue;
            }

            animalAmount = Mathf.CeilToInt((_animalPoblation[i].MaxDangerAmount + _animalPoblation[i].MinDangerAmount) * 0.5f);
            for (int j = 0; j < animalAmount; ++j)
            {
                GenerateAnimal(animal);
            }
        }
    }

    private void GenerateAnimal(AnimalController animal)
    {
        var temp = Instantiate<AnimalController>(animal, _animalParent);
        temp.Init(animal.GetAnimalType());
        Vector2 point = UnityEngine.Random.insideUnitCircle;
        temp.transform.position = point;
    }

    AnimalController GetPrefab(ETypeAnimal animalType)
    {
        for (int i = 0; i < _animalPrefabs.Count; ++i)
        {
            if (_animalPrefabs[i].GetAnimalType() == animalType)
            {
                return _animalPrefabs[i];
            }
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
