using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyChallenge : MonoBehaviour {

    public enum EKeys
    {
        Up,
        Down,
        Right,
        Left,
    }

    public List<KeyChallengeInfo> _keyChallenge;

    private int _currentKeyChallenge;

    public void StartNextChallenge()
    {
        List<EKeys> currentKeys = GenerateRandomKeys(_keyChallenge[_currentKeyChallenge].NumberKeys);


    }

    public List<EKeys> GenerateRandomKeys(int numberOfKeys)
    {
        EKeys newKey;
        List<EKeys> result = new List<EKeys>();
        for (int i = 0; i < numberOfKeys; i++)
        {
            int value = Random.Range(0, 4);
            newKey = (EKeys)value;
            result.Add(newKey);
        }

        return result;
    }
}
