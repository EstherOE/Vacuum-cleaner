using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSo", menuName = "CrazyLabs/New Player") ]
public class PlayerSO : ScriptableObject
{
    public float maxHealth;
    public float playerSpeed;
    public float playerRotationSpeed;
    public int upgradeAbilityPrice;
    public int upgradeProcessorPrice;
    public int upgradeCapacityPrice;
}
