using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CurrencySo", menuName = "CrazyLabs/New Currency")]
public class CurrencySO : ScriptableObject
{
    public int playerCurrency;


    public void CurrencyInitializer() 
    {
        playerCurrency =   PlayerPrefs.GetInt("currency");
    }
    public void SubtractCoins(int coinsToSubtract) 
    {
        playerCurrency -= coinsToSubtract;
        PlayerPrefs.SetInt("currency", playerCurrency);
    }

    public void AddCoins(int coinsToAdd) 
    {
        playerCurrency += coinsToAdd;
        PlayerPrefs.SetInt("currency", playerCurrency);
    }
}
