using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CollectibleSO", menuName = "CrazyLabs/New Collectible")]
public class CollectibleSO : ScriptableObject
{
    public float timer;
    public float effectTime;
    public float spawnRate;
}
