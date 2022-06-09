using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSo", menuName = "CrazyLabs/New Player") ]
public class PlayerSO : ScriptableObject
{
    public float playerSpeed;
    public float playerRotationSpeed;
}
