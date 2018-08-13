using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class PlanetController : MonoBehaviour
{

    [SerializeField]
    float _surface;
    [SerializeField]
    float _orbit01;
    [SerializeField]
    float _orbit02;
    [SerializeField]
    float _orbit03;
    [SerializeField]
    float _space;
    [SerializeField]
    float _angularVelocity;


    [Header("MaxSize")]
    [SerializeField]
    int _minSize;
    [SerializeField]
    int _maxSize;
    [SerializeField]
    List<Sprite> _ecoSystem;

    [Header("animalByEcosystem")]
    [SerializeField]
    List<PlanetControllerInfo> _animalAndEcosystems;

    [SerializeField]
    bool _generateMap;


    public Dictionary<EEcosystem, List<PlanetControllerInfoEcosystem>> EcosystemAngles { get; private set; }

    public Dictionary<ETypeAnimal, EEcosystem> EcosystemByAnimal { get; set; }

    bool _canRotate = true;

    public float GetOrbitPosition(int orbit)
    {
        switch (orbit)
        {
            case 0: return _surface;
            case 1: return _orbit01;
            case 2: return _orbit02;
            case 3: return _orbit03;
            case 4: return _space;
        }
        return _orbit01;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _surface);
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(transform.position, _orbit01);
        Gizmos.DrawWireSphere(transform.position, _orbit02);
        Gizmos.DrawWireSphere(transform.position, _orbit03);
        Gizmos.DrawWireSphere(transform.position, _space);
    }

    private void Start()
    {
        GameObject.FindObjectOfType<LevelManager>().OnChangeAbductionState += OnChangeAbductionState;

        EcosystemByAnimal = new Dictionary<ETypeAnimal, EEcosystem>();
        for (int i = _animalAndEcosystems.Count - 1; i >= 0; --i)
        {
            EcosystemByAnimal.Add(_animalAndEcosystems[i].animalType, _animalAndEcosystems[i].ecosystem);
        }

        EcosystemAngles = new Dictionary<EEcosystem, List<PlanetControllerInfoEcosystem>>();
        EcosystemAngles.Add(EEcosystem.Dessert, new List<PlanetControllerInfoEcosystem>());
        EcosystemAngles.Add(EEcosystem.Forest, new List<PlanetControllerInfoEcosystem>());
        EcosystemAngles.Add(EEcosystem.Mountain, new List<PlanetControllerInfoEcosystem>());
        EcosystemAngles.Add(EEcosystem.Sea, new List<PlanetControllerInfoEcosystem>());

        if (_generateMap)
        {
            GenerateEcoSystem();
        }
        else
        {
            EcosystemAngles[EEcosystem.Sea].Add(new PlanetControllerInfoEcosystem(0, -35, EEcosystem.Sea));
            EcosystemAngles[EEcosystem.Mountain].Add(new PlanetControllerInfoEcosystem(-35, -101, EEcosystem.Mountain));
            EcosystemAngles[EEcosystem.Mountain].Add(new PlanetControllerInfoEcosystem(-101, -143, EEcosystem.Mountain));
            EcosystemAngles[EEcosystem.Sea].Add(new PlanetControllerInfoEcosystem(-143, -203, EEcosystem.Sea));
            EcosystemAngles[EEcosystem.Dessert].Add(new PlanetControllerInfoEcosystem(-203, -235, EEcosystem.Dessert));

            EcosystemAngles[EEcosystem.Forest].Add(new PlanetControllerInfoEcosystem(-235, -360, EEcosystem.Forest));
        }
    }

    private void GenerateEcoSystem()
    {
        List<AngleRange> angles = new List<AngleRange>();
        SpriteShape spriteShape = GetComponentInChildren<SpriteShapeController>().spriteShape;

        bool isDone = false;
        int acumSize = 0;
        int remainingSize = 360;
        int nextMaxSize;

        int size;

        AngleRange newAngle;

        EEcosystem ecosystem;

        while (!isDone)
        {
            nextMaxSize = Math.Min(_maxSize, remainingSize);

            remainingSize = 360 - acumSize;
            size = UnityEngine.Random.RandomRange(_minSize, nextMaxSize);
            if ((acumSize + size) > 360)
            {
                size = (360 - acumSize);
            }

            if ((360 - (acumSize + size)) < _minSize)
                size += (360 - (acumSize + size));

            newAngle = new AngleRange();
            newAngle.sprites = new List<Sprite>();
            ecosystem = (EEcosystem)UnityEngine.Random.Range(0, 4);
            newAngle.sprites.Add(_ecoSystem[(int)ecosystem]);
            newAngle.start = acumSize;
            newAngle.end = acumSize + size;

            acumSize += size;
            remainingSize -= size;

            if (remainingSize <= 0)
                isDone = true;

            EcosystemAngles[ecosystem].Add(new PlanetControllerInfoEcosystem((int)newAngle.start, (int)newAngle.end, ecosystem));

            angles.Add(newAngle);
        }

        spriteShape.angleRanges = angles;
    }

    public void IsInArea(AnimalController animal)
    {
        EEcosystem ecosystem = EcosystemByAnimal[animal.GetAnimalType()];
        if (!IsAngleOfEcosystem(animal.transform.position, ecosystem))
        {
            animal.ChangeDirection();
        }
    }

    private bool IsAngleOfEcosystem(Vector3 position, EEcosystem ecosystem)
    {
        float angle = Vector3.SignedAngle(transform.up, position - transform.position, Vector3.forward);

        //Debug.Log(angle);

        return true;
    }

    private void OnChangeAbductionState(bool boolean)
    {
        _canRotate = !boolean;
    }

    public void Update()
    {
        if (_canRotate)
            transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + _angularVelocity * Time.deltaTime);
    }

    public static bool IsSpace(int orbit)
    {
        return orbit >= 4;
    }
}
