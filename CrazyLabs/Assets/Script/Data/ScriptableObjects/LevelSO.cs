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
   
    [Header("Egg Attributes")]
    public int eggCount;
    public float eggXPositive;
    public float eggXNegative;
    public float eggZPositive;
    public float eggZNegative;

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
    public bool doesLevelHaveTimer;
    public bool hasTutorial;
    public GameObject levelPrefab;
    public Vector3 levelPosition;
    public int scoreToReach;
    public string levelInstructions;

    [Header("LevelUI Attributes")]
    public int totalStars;
    public bool isUnlocked;

    public void SendInstructions() 
    {
        levelInstructions = "Place all " + " " + chickCount + " " + "chicks into the coop";
       
    }

    public void UnlockLevel() 
    {
        
    }

}
