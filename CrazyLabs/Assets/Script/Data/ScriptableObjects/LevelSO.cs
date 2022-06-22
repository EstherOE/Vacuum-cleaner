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

    [Header("LevelPrefab Attributes")]
    public int henCount;
    public int eggCount;
    public int chickCount;
    public float levelTime;
    public float henSpeed;
    public float chickSpeed;
    public float henSightRange;
    public float chickSightRange;
    public GameObject levelPrefab;
    public Vector3 levelPosition;
    public int scoreToReach;

   

   


    public void LoadLevelScene(int id) 
    {

           }

}
