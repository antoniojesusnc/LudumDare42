using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialFirstAnimal : MonoBehaviour
{

    PegiController _pegi;
    InventoryController _inventory;
    [SerializeField]
    TMPro.TextMeshProUGUI _text;

    private void Start()
    {
        _pegi = FindObjectOfType<PegiController>();
        _inventory = FindObjectOfType<InventoryController>();
        _text.enabled = false;
    }

    void Update()
    {
        if (!_text.enabled)
        {
            foreach (var item in _inventory.InventoryInPegi)
            {
                if (item.Value > 0)
                {
                    _text.enabled = true;
                    break;
                }
            }
        }

        if (_pegi.IsInSpace)
            gameObject.SetActive(false);
    }
}
