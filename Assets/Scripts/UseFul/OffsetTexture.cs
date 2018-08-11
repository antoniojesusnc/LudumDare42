using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetTexture : MonoBehaviour {

    [SerializeField]
    float _speed;
    float _currentValue;

    SpriteRenderer _renderer;

    void Start () {
        _renderer = GetComponent<SpriteRenderer>();
    }
    
    void Update () {
        _currentValue += _speed * Time.deltaTime;
        _renderer.material.mainTextureOffset = Vector2.right * _currentValue;



    }
}
