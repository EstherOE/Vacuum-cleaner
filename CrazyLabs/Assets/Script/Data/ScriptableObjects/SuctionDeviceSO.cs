using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SuctionDeviceSo", menuName = "CrazyLabs/New Device")]
public class SuctionDeviceSO : ScriptableObject
{
    public int deviceCapacity;
    public int offloadRate;
    public int pickUpRate;
    
}
