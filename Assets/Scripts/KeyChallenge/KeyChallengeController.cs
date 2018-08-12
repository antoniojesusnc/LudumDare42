using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EKeys
{
    Up,
    Down,
    Right,
    Left,
    Size,
}

public class KeyChallengeController : MonoBehaviour
{

    [Header("rules")]
    [SerializeField]
    float _initTime;
    [SerializeField]
    float _timeToAddIfFail;
    [SerializeField]
    float _timeToTakeIfPerfect;

    [SerializeField]
    float _timeToSuccessPressed;
    [SerializeField]
    float _timeToPerfectPressed;

    [Header("Game Obj")]
    [SerializeField]
    Transform _lineStart01;
    [SerializeField]
    Transform _lineFinish01;
    [SerializeField]
    Transform _lineStart02;
    [SerializeField]
    Transform _lineFinish02;
    [SerializeField]
    Transform _lineStart03;
    [SerializeField]
    Transform _lineFinish03;
    [SerializeField]
    Transform _lineStart04;
    [SerializeField]
    Transform _lineFinish04;

    [Header("Prefabs")]
    [SerializeField]
    Transform _keysParent;
    [SerializeField]
    Transform _keyDown;
    [SerializeField]
    Transform _keyUp;
    [SerializeField]
    Transform _keyRight;
    [SerializeField]
    Transform _keyLeft;

    [Header("Others")]
    [SerializeField]
    PegiInput _input;

    public float CompleteFactor
    {
        get
        {
            return _currentTime / _secondsToFinish;
        }
    }

    float _secondsToFinish;
    float _speed;
    float _timeBetweenKeys;
    float _currentTime;

    Dictionary<EKeys, List<KeyChallengeInfo>> _keyActives;

    public List<KeyChallengeInfo> _keys;

    // current game data
    private int _index;
    private bool _playing;


    private void OnEnable()
    {
        Debug.Log("Debug InitChallenge");
        InitChallenge(20, 0.7f, 2);
    }

    private void OnDisable()
    {
        StopChallenge();
    }

    public void InitChallenge(float secondsToFinish, float timeBetweenKeys, float speed)
    {
        _secondsToFinish = secondsToFinish;
        _speed = speed;
        _timeBetweenKeys = timeBetweenKeys;

        _keys = new List<KeyChallengeInfo>();

        _keyActives = new Dictionary<EKeys, List<KeyChallengeInfo>>();
        _keyActives.Add(EKeys.Left, new List<KeyChallengeInfo>());
        _keyActives.Add(EKeys.Up, new List<KeyChallengeInfo>());
        _keyActives.Add(EKeys.Down, new List<KeyChallengeInfo>());
        _keyActives.Add(EKeys.Right, new List<KeyChallengeInfo>());

        GenerateInitialValues();
        StartChallengeKeys();
    }

    private void StopChallenge()
    {
        _playing = false;
        StopAllCoroutines();
    }

    private void StartChallengeKeys()
    {
        _playing = true;

        _currentTime = 0;
    }

    private void Update()
    {
        if (!_playing)
            return;

        _currentTime += Time.deltaTime;

        if (CheckNextTime())
        {
            StartCoroutine(ShowNextKeyCo());
            GenerateNextKey();
        }

        CheckInputs();
    }

    private bool CheckNextTime()
    {
        if (_keys[0].time <= (_currentTime + _speed))
        {
            return true;
        }

        return false;
    }

    private void CheckInputs()
    {
        EKeys key = GetKeyByInput();

        if (key == EKeys.Size)
            return;

        var activesInLine = _keyActives[key];
        if (activesInLine.Count <= 0)
            return;

        bool someDetected = false;
        for (int i = 0; !someDetected && i < activesInLine.Count; i++)
        {
            if (activesInLine[i].Failed)
                continue;

            CheckCorrectInput(activesInLine[i]);
            someDetected = true;
        }
    }

    private void CheckCorrectInput(KeyChallengeInfo keyChallengeInfo)
    {
        
    }

    private IEnumerator ShowNextKeyCo()
    {
        KeyChallengeInfo info = _keys[0];
        _keys.RemoveAt(0);

        if (info.key == EKeys.Size)
            yield break;

        _keyActives[info.key].Add(info);

        float startTime = _currentTime;
        float timeStamp = 0;

        Transform obj = CreateKey(info.key);
        Vector2 position;
        while (timeStamp < (info.time - startTime))
        {
            position = GetMiddlePoint(timeStamp / (info.time - startTime), info.key);
            obj.localPosition = position;
            yield return 0;
            timeStamp += Time.deltaTime;
        }

        // here is when finish 
        _keyActives[info.key].Remove(info);
        Destroy(obj.gameObject);
    }

    private Transform CreateKey(EKeys key)
    {
        switch (key)
        {
            case EKeys.Left: return Instantiate<Transform>(_keyLeft, _keysParent);
            case EKeys.Up: return Instantiate<Transform>(_keyUp, _keysParent);
            case EKeys.Down: return Instantiate<Transform>(_keyDown, _keysParent);
            case EKeys.Right: return Instantiate<Transform>(_keyRight, _keysParent);
        }
        return null;
    }



    private void GenerateInitialValues()
    {
        _keys.Add(new KeyChallengeInfo(EKeys.Size, _initTime));

        for (int i = 0; i < 5; ++i)
        {
            GenerateNextKey();
        }
    }

    public void GenerateNextKey()
    {
        EKeys newKey;
        int value = UnityEngine.Random.Range(0, 4);
        newKey = (EKeys)value;

        float nextTime = _keys[_keys.Count - 1].time + _timeBetweenKeys;

        _keys.Add(new KeyChallengeInfo(newKey, nextTime));
    }

    public Vector2 GetMiddlePoint(float factor, EKeys key)
    {
        switch (key)
        {
            case EKeys.Left: return Vector2.Lerp(_lineStart01.localPosition, _lineFinish01.localPosition, factor);
            case EKeys.Up:
                return Vector2.Lerp(_lineStart02.localPosition, _lineFinish02.localPosition, factor);
            case EKeys.Down:
                return Vector2.Lerp(_lineStart03.localPosition, _lineFinish03.localPosition, factor);
            case EKeys.Right:
                return Vector2.Lerp(_lineStart04.localPosition, _lineFinish04.localPosition, factor);
        }
        return Vector2.zero;
    }

    EKeys GetKeyByInput()
    {
        if (_input.MomentumDown.x < 0)
            return EKeys.Left;
        if (_input.MomentumDown.x > 0)
            return EKeys.Right;
        if (_input.MomentumDown.y < 0)
            return EKeys.Down;
        if (_input.MomentumDown.y > 0)
            return EKeys.Up;
        return EKeys.Size;
    }
}
