using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public bool IsAbducting;
    public event DelegateVoidFunctionBoolParameter OnChangeAbductionState;

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

    void Start()
    {
        StartLevel();
    }

    void StartLevel()
    {
        _inventory = GetComponent<InventoryController>();

        AnimalAmount = new Dictionary<ETypeAnimal, int>();
        GenerateStartedAnimals();

        for (int i = _animalPoblation.Count - 1; i >= 0; i--)
        {
            StartCoroutine(CreateNewAnimalDelayed(_animalPoblation[i].AnimalType));
        }
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

        var newAnimal = Instantiate<AnimalController>(animals[0], _animalParent);
        newAnimal.Init(animal);
        newAnimal.transform.position = animals[randomAnimal].transform.position;

        _allAnimals.Add(newAnimal);

        ++AnimalAmount[animal];
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
        _inventory.AddAnimal(animalBeingAbduced.GetAnimalType());
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
        Vector2 point = UnityEngine.Random.insideUnitCircle;
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
    }

    private void CheckLoseGame()
    {
        ETypeAnimal animalType;
        foreach (var animalData in AnimalAmount)
        {
            animalType = animalData.Key;
            if (animalData.Value > GetPoblationInfo(animalType).MaxAmount)
                GUIManager.Instance.OpenLoseGUI();
        }
    }
}
