using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIInventory : MonoBehaviour {


    [SerializeField]
    GameObject _topBar;

    [SerializeField]
    List<GameObject> _keys;

    [SerializeField]
    List<TMPro.TextMeshProUGUI> text;

    [SerializeField]
    List<Image> _downBar;




    List<GameObject> _topBarElements;

    void Start () {
        
    }
    
    void Update () {
        
    }
}
