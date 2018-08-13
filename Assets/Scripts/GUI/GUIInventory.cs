using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIInventory : MonoBehaviour
{
    [SerializeField]
    float _amountPerClick;
    [SerializeField]
    float _downaRate;

    [SerializeField]
    GameObject _topBar;

    List<Image> _topElements;

    [SerializeField]
    List<GameObject> _keys;

    [SerializeField]
    List<TMPro.TextMeshProUGUI> _text;

    [SerializeField]
    List<TMPro.TextMeshProUGUI> _remainingText;

    [SerializeField]
    List<Image> _downBar;

    InventoryController _inventory;
    GUIInventoryInput _input;
    List<GameObject> _topBarElements;

    bool _enabled;

    LevelManager _level;

    void Start()
    {
        _level = FindObjectOfType<LevelManager>(); ;
        _inventory = FindObjectOfType<InventoryController>();
        _topElements = new List<Image>();
        GameObject reference = _topBar.transform.GetChild(0).gameObject;
        _topElements.Add(reference.GetComponent<Image>());
        for (int i = _inventory.InventoryCapacity - 1; i >= 1; --i)
        {
            _topElements.Add(Instantiate(reference, _topBar.transform).GetComponent<Image>());
        }
        FindObjectOfType<PegiController>().OnChangeOrbit += OnChangeOrbit;

        _input = GetComponent<GUIInventoryInput>();
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    private void OnChangeOrbit(int onChangeOrbit)
    {
        gameObject.SetActive(PlanetController.IsSpace(onChangeOrbit));
    }

    void LateUpdate()
    {
        UpdateCounters();
        UpdateImage();

        UpdateInput();
        CheckSomeSent();

        CheckRemainingInventory();
        CheckCurrentInventory();
    }

    private void CheckRemainingInventory()
    {
        ETypeAnimal animal;
        int amount = 0;
        for (int i = 0; i < _remainingText.Count; ++i)
        {
            animal = GetByIndex(i);
            amount = (_level.VictoryConditions[animal] - _inventory.InventorySent[animal]);

            _remainingText[i].text = "X"+ (amount < 0 ? "0" : amount.ToString());
        }
    }



    private void CheckCurrentInventory()
    {
        int currentAmount = 0;
        foreach (var item in _inventory.InventoryInPegi)
        {
            currentAmount += item.Value;
        }

        for (int i = 0; i < _topElements.Count; ++i)
        {
            _topElements[i].enabled = i < currentAmount;
        }
    }

    private void CheckSomeSent()
    {
        for (int i = _downBar.Count - 1; i >= 0; i--)
        {
            if (_downBar[i].fillAmount >= 1)
            {
                Sent(i);
            }
        }
    }

    private void Sent(int i)
    {
        ETypeAnimal animal = GetByIndex(i);
        Debug.Log(animal + " + ");
        --_inventory.InventoryInPegi[animal];
        ++_inventory.InventorySent[animal];
        _downBar[i].fillAmount = 0;
    }

    private void UpdateImage()
    {
        for (int i = _downBar.Count - 1; i >= 0; i--)
        {
            if (_downBar[i].fillAmount >= 0)
                _downBar[i].fillAmount -= _downaRate * Time.deltaTime;
        }
    }

    private void UpdateCounters()
    {
        _text[0].text = "X" + _inventory.InventoryInPegi[ETypeAnimal.Shark];
        _text[1].text = "X" + _inventory.InventoryInPegi[ETypeAnimal.Camel];
        _text[2].text = "X" + _inventory.InventoryInPegi[ETypeAnimal.Bear];
        _text[3].text = "X" + _inventory.InventoryInPegi[ETypeAnimal.Cow];

        /*
        for (int i = 0; i < 4; i++)
        {
        }
        */
    }

    void UpdateInput()
    {
        if (_input.MomentumDown.x < 0)
        {
            KeyPressed(EKeys.Left);
        }
        else if (_input.MomentumDown.y > 0)
        {
            KeyPressed(EKeys.Up);
        }
        else if (_input.MomentumDown.y < 0)
        {
            KeyPressed(EKeys.Down);
        }
        else if (_input.MomentumDown.x > 0)
        {
            KeyPressed(EKeys.Right);
        }
    }

    private void KeyPressed(EKeys key)
    {
        int index = (int)key;
        GameObject button = _keys[index];
        LeanTween.scale(button, Vector3.one * 1.1f, 0.017f).setLoopPingPong(1);
        LeanTween.alpha(button, 1, 0.017f).setLoopPingPong(1);

        if (_inventory.InventoryInPegi[GetAnimal(key)] > 0)
            _downBar[index].fillAmount += _amountPerClick;
    }

    private ETypeAnimal GetAnimal(EKeys key)
    {
        switch (key)
        {
            case EKeys.Left: return ETypeAnimal.Cow;
            case EKeys.Up: return ETypeAnimal.Shark;
            case EKeys.Down: return ETypeAnimal.Camel;
            case EKeys.Right: return ETypeAnimal.Bear;
        }
        return ETypeAnimal.Size;
    }

    private ETypeAnimal GetByIndex(int index)
    {
        switch (index)
        {
            case 0: return ETypeAnimal.Shark;
            case 1: return ETypeAnimal.Camel;
            case 2: return ETypeAnimal.Bear;
            case 3: return ETypeAnimal.Cow;
        }
        return ETypeAnimal.Size;
    }

    void InSpace()
    {

    }
}
