using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class KeyChallengeInfo  {

    public EKeys key;
    public float time;
    public bool Failed;
    public bool Success;
    public Transform ObjKey;

    public KeyChallengeInfo(EKeys newKey, float newTime)
    {
        key = newKey;
        time = newTime;
    }
}
