using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "LevelSO", menuName = "CrazyLabs/New Level")]
public class LevelSO : ScriptableObject
{
    [Header("Collectible Attributes")]
    public float[] CollectiblesTimers;
    public float[] CollectiblesEffectTimes;
    public float[] CollectiblesSpawnRates;
    public GameObject[] itemsSpawnedInScene;



    [Header("Hen AI Attributes")]
    public int henCount;
    public float henSpeed;
    public float henSightRange;
    public float henXPositive;
    public float henXNegative;
    public float henZPositive;
    public float henZNegative;
    public float henWalkPointRange;
    public int eggCount;

    [Header("Chick AI Attributes")]
    public int chickCount;
    public float chickSpeed;
    public float chickSightRange;
    public float chickXPositive;
    public float chickXNegative;
    public float chickZPositive;
    public float chickZNegative;
    public float chickWalkpointRange;


    [Header("LevelPrefab Attributes")]
    public float levelTime;
    public GameObject levelPrefab;
    public Vector3 levelPosition;
    public int scoreToReach;

}
