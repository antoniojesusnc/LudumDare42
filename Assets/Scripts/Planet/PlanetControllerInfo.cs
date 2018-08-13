using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public enum EEcosystem
{
    Mountain,
    Sea,
    Forest,
    Dessert,
    Size
}

[System.Serializable]
public class PlanetControllerInfo
{
    public EEcosystem ecosystem;
    public ETypeAnimal animalType;

}

[System.Serializable]
public class PlanetControllerInfoEcosystem
{
    public int StartAngle;
    public int EndAngle;
    public EEcosystem ecosystem;

    public PlanetControllerInfoEcosystem(int newStartAngle, int newEndAngle, EEcosystem newEcosystem)
    {
        StartAngle = newStartAngle;
        EndAngle = newEndAngle;
        ecosystem = newEcosystem;
    }
}
