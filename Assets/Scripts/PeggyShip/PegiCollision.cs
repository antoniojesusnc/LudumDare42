using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PegiCollision : MonoBehaviour
{
    [SerializeField]
    PegiController _pegi;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CloudController cloud = collision.GetComponentInParent<CloudController>();
        if (cloud.IsBeingDestroyed)
            return;

        if (!cloud.IsAngry)
            _pegi.OnEnterInNormalCloud();
        else
            _pegi.OnEnterInAngryCloud();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        CloudController cloud = collision.GetComponentInParent<CloudController>();
        if (cloud.IsBeingDestroyed)
            return;

        if (cloud.IsAngry && !_pegi.IsInShock)
            _pegi.OnEnterInAngryCloud();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _pegi.OnExitNormalCloud();
    }
}