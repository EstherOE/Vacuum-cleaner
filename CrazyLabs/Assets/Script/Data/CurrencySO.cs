using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CurrencySo", menuName = "CrazyLabs/New Currency")]
public class CurrencySO : ScriptableObject
{
    public int playerCurrency;


    public void SubtractCoins(int coinsToSubtract) 
    {
        playerCurrency -= coinsToSubtract;
    }

    public void AddCoins(int coinsToAdd) 
    {
        playerCurrency += coinsToAdd;
    }
}
