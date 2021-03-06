﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public bool IsAbducting;
    public event DelegateVoidFunctionBoolParameter OnChangeAbductionState;

    [SerializeField]
    int _minAmountOfAnimals;

    [SerializeField]
    int _maxAmountOfAnimals;

    public Dictionary<ETypeAnimal, int> VictoryConditions;

    [SerializeField]
    Transform _animalParent;

    [SerializeField]
    List<AnimalPoblationInfo> _animalPoblation;
    public List<AnimalPoblationInfo> AnimalPoblation
    {
        get
        {
            return _animalPoblation;
        }
    }

    public Dictionary<ETypeAnimal, int> AnimalAmount { get; private set; }

    AnimalManager _animalManager;

    public List<AnimalController> _animalPrefabs;

    [Header("Level Elements")]
    [SerializeField]
    List<AnimalController> _allAnimals = new List<AnimalController>();


    // inventory
    InventoryController _inventory;
    Dictionary<ETypeAnimal, int> _objetive;
    PlanetController _planet;

    void Start()
    {
        StartLevel();
    }

    void StartLevel()
    {

        _inventory = GetComponent<InventoryController>();
        _planet = GameObject.FindObjectOfType<PlanetController>();

        AnimalAmount = new Dictionary<ETypeAnimal, int>();
        GenerateStartedAnimals();

        for (int i = _animalPoblation.Count - 1; i >= 0; i--)
        {
            StartCoroutine(CreateNewAnimalDelayed(_animalPoblation[i].AnimalType));
        }

        GenerateVictoryConditions();
    }

    private void GenerateVictoryConditions()
    {
        VictoryConditions = new Dictionary<ETypeAnimal, int>();
        VictoryConditions.Add(ETypeAnimal.Cow, UnityEngine.Random.Range(_minAmountOfAnimals, _maxAmountOfAnimals));
        VictoryConditions.Add(ETypeAnimal.Shark, UnityEngine.Random.Range(_minAmountOfAnimals, _maxAmountOfAnimals));
        VictoryConditions.Add(ETypeAnimal.Camel, UnityEngine.Random.Range(_minAmountOfAnimals, _maxAmountOfAnimals));
        VictoryConditions.Add(ETypeAnimal.Bear, UnityEngine.Random.Range(_minAmountOfAnimals, _maxAmountOfAnimals));
    }

    IEnumerator CreateNewAnimalDelayed(ETypeAnimal animal)
    {
        float timer = UnityEngine.Random.Range(GetPoblationInfo(animal).ReproductionMaxTime, GetPoblationInfo(animal).ReproductionMinTime);
        //Debug.Log(animal + ": " + timer);
        yield return new WaitForSeconds(timer);

        ReproduceOneAnimal(animal);

        StartCoroutine(CreateNewAnimalDelayed(animal));
    }

    private void ReproduceOneAnimal(ETypeAnimal animal)
    {
        List<AnimalController> animals = new List<AnimalController>();
        for (int i = _allAnimals.Count - 1; i >= 0; i--)
        {
            if (_allAnimals[i].GetAnimalType() == animal)
                animals.Add(_allAnimals[i]);
        }

        int randomAnimal = UnityEngine.Random.Range(0, animals.Count);

        AnimalController animalMother = animals[randomAnimal];
        StartCoroutine(ReproduceOneAnimalCo(animalMother));
    }

    private IEnumerator ReproduceOneAnimalCo(AnimalController animalMother)
    {
        animalMother.IsReproducing = true;

        yield return new WaitForSeconds(animalMother.ReproductionTime);

        var newAnimal = Instantiate<AnimalController>(animalMother, _animalParent);
        newAnimal.IsReproducing = true;
        newAnimal.Init(animalMother.GetAnimalType());
        newAnimal.transform.position = animalMother.transform.position;

        newAnimal.DoJumpReproduction(false);
        animalMother.DoJumpReproduction(true);

        _allAnimals.Add(newAnimal);

        ++AnimalAmount[animalMother.GetAnimalType()];

        yield return new WaitForSeconds(animalMother.ReproductionJumpTime);
        animalMother.IsReproducing = false;
        newAnimal.IsReproducing = false;
    }

    private void GenerateStartedAnimals()
    {
        AnimalController animal;
        int animalAmount;
        for (int i = _animalPoblation.Count - 1; i >= 0; --i)
        {
            animal = GetPrefab(_animalPoblation[i].AnimalType);
            if (animal == null)
            {
                Debug.LogWarning("Animal " + _animalPoblation[i].AnimalType + " not found in prefabs");
                continue;
            }

            animalAmount = Mathf.CeilToInt((_animalPoblation[i].MaxDangerAmount + _animalPoblation[i].MinDangerAmount) * 0.5f);
            AnimalAmount.Add(_animalPoblation[i].AnimalType, animalAmount);

            for (int j = 0; j < animalAmount; ++j)
            {
                GenerateAnimal(animal);
            }
        }
    }

    internal void AnimalAbducedSuccessFul(AnimalController animalBeingAbduced)
    {
        _inventory.AddAnimalToPegi(animalBeingAbduced.GetAnimalType());
        SetAbductionMode(false);
        _allAnimals.Remove(animalBeingAbduced);
        --AnimalAmount[animalBeingAbduced.GetAnimalType()];

        Destroy(animalBeingAbduced.gameObject);
    }

    public void SetAbductionMode(bool setAbduction)
    {
        IsAbducting = setAbduction;
        if (OnChangeAbductionState != null)
            OnChangeAbductionState(IsAbducting);
    }

    private void GenerateAnimal(AnimalController animal)
    {
        var temp = Instantiate<AnimalController>(animal, _animalParent);
        temp.Init(animal.GetAnimalType());

        var angles = _planet.EcosystemAngles[_planet.EcosystemByAnimal[animal.GetAnimalType()]];
        int value = UnityEngine.Random.Range(0, angles.Count);
        var area = angles[value];

        int finalAngle = UnityEngine.Random.Range(area.StartAngle, area.EndAngle);

        float radians = Mathf.Deg2Rad * finalAngle;
        Vector2 finalPosition = new Vector2(Mathf.Sin(radians), Mathf.Cos(radians));

        //Debug.Log(animal.GetAnimalType().ToString() + " : "+ finalAngle);
        //Vector2 finalPosition = Rotate(Vector2.up, finalAngle);

        /*
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        go.name = animal.GetAnimalType().ToString();
        go.transform.position = finalPosition;
        */

        //Vector2 point = UnityEngine.Random.insideUnitCircle;
        Vector2 point = finalPosition;
        temp.transform.position = point;

        _allAnimals.Add(temp);
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

    AnimalPoblationInfo GetPoblationInfo(ETypeAnimal animalType)
    {
        for (int i = 0; i < _animalPoblation.Count; ++i)
        {
            if (_animalPoblation[i].AnimalType == animalType)
            {
                return _animalPoblation[i];
            }
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < _allAnimals.Count; ++i)
        {
            _allAnimals[i].UpdateAnimal();
        }

        CheckLoseGame();
        CheckWinGame();
    }

    private void CheckLoseGame()
    {
        ETypeAnimal animalType;
        foreach (var animalData in AnimalAmount)
        {
            animalType = animalData.Key;
            if (animalData.Value > GetPoblationInfo(animalType).MaxAmount)
                FindObjectOfType<GUIManager>().OpenLoseGUI();
            if (animalData.Value < GetPoblationInfo(animalType).MinAmount)
                FindObjectOfType<GUIManager>().OpenLoseGUI();
        }
    }

    private void CheckWinGame()
    {
        bool allConditions = true;
        ETypeAnimal animalType;
        foreach (var animalData in VictoryConditions)
        {
            animalType = animalData.Key;
            allConditions &= animalData.Value <= _inventory.InventorySent[animalType];
        }
        if (allConditions)
        {
            var go = FindObjectOfType<GUIManager>();
            if(go != null)
                go.OpenVictoryGUI();
        }
    }

    public Vector2 Rotate(Vector2 vector, float degrees)
    {
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

        float tx = vector.x;
        float ty = vector.y;
        vector.x = (cos * tx) - (sin * ty);
        vector.y = (sin * tx) + (cos * ty);
        return vector;
    }
}

