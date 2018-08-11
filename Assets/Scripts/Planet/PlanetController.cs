using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour {

    [SerializeField]
    float _surface;

    [SerializeField]
    float _orbit01;
    [SerializeField]
    float _orbit02;
    [SerializeField]
    float _orbit03;
    

    public float GetOrbitPosition(int orbit)
    {
        switch (orbit)
        {
            case 1: return _orbit01;
            case 2: return _orbit02;
            case 3: return _orbit03;
        }
        return _orbit01;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _surface);
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, _orbit01);
        Gizmos.DrawWireSphere(transform.position, _orbit02);
        Gizmos.DrawWireSphere(transform.position, _orbit03);
    }
}
