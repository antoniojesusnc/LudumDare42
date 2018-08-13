using System;
using System.Collections;
using UnityEngine;

public class CloudManager : MonoBehaviour
{

    [SerializeField]
    int _simultaneousClouds;

    [SerializeField]
    float _minTimeNewCloud;
    [SerializeField]
    float _maxTimeNewCloud;

    [SerializeField]
    float _minTimeNewCloudNormal;
    [SerializeField]
    float _maxTimeNewCloudNormal;

    [SerializeField]
    float _minxTimeNewCloudAngry;
    [SerializeField]
    float _maxTimeNewCloudAngry;

    [SerializeField]
    float _minSpeed;
    [SerializeField]
    float _maxSpeed;

    [SerializeField]
    CloudController _cloudPrefab;

    [SerializeField]
    float _alphaAnimTime;

    void Start()
    {

        for (int i = _simultaneousClouds - 1; i >= 0; --i)
        {
            StartCoroutine(GenerateRandomCloudCo());
        }
    }

    private IEnumerator GenerateRandomCloudCo()
    {
        var newCloud = GenerateRandomCloud();

        LeanTween.alpha(newCloud.GetComponentInChildren<SpriteRenderer>().gameObject, 1, 2);

        yield return new WaitForSeconds(UnityEngine.Random.Range(_minTimeNewCloud, _maxTimeNewCloud));
        StartCoroutine(GenerateRandomCloudCo());

    }

    private CloudController GenerateRandomCloud()
    {
        CloudController newCloud = Instantiate<CloudController>(_cloudPrefab, transform);

        int orbit = UnityEngine.Random.Range(1, 4);
        int cloudID = UnityEngine.Random.Range(1, 4);
        float timeNormal = UnityEngine.Random.Range(_minTimeNewCloudNormal, _maxTimeNewCloudNormal);
        float timeAngry = UnityEngine.Random.Range(_minxTimeNewCloudAngry, _maxTimeNewCloudAngry);
        float speed = UnityEngine.Random.Range(_minSpeed, _maxSpeed);

        newCloud.Init(orbit, cloudID, timeNormal, timeAngry, speed);

        Vector2 point = UnityEngine.Random.insideUnitCircle;
        newCloud.transform.position = point;


        newCloud.GetComponentInChildren<SpriteRenderer>().color = new Color(1, 1, 1, 0);


        StartCoroutine(DestroyCloudInCo(newCloud, timeNormal + timeAngry + 1));

        return newCloud;
    }

    private IEnumerator DestroyCloudInCo(CloudController newCloud, float time)
    {
        yield return new WaitForSeconds(time);

        LeanTween.alpha(newCloud.GetComponentInChildren<SpriteRenderer>().gameObject, 0, _alphaAnimTime);
        newCloud.IsBeingDestroyed = true;
        yield return new WaitForSeconds(_alphaAnimTime);

        Destroy(newCloud.gameObject);
    }
}
