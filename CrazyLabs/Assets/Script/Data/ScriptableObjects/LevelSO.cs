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
    public GameObject levelPrefab;
    public Vector3 levelPosition;


    public void LoadLevelScene(int id) 
    {

           }

}
