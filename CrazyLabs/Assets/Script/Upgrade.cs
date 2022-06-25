using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Upgrade : MonoBehaviour
{
    public PlayerSO player;
    public TextMeshProUGUI upgradeCapacityPrice;
    public TextMeshProUGUI upgradeAbilityPrice;
    public SuctionDeviceSO playerDevice;

    // Start is called before the first frame update
    void Start()
    {
      
        upgradeAbilityPrice.text = player.upgradeAbilityPrice.ToString() + "eggs";
        upgradeCapacityPrice.text = player.upgradeCapacityPrice.ToString() + "eggs";
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpgradeCapacity()
    {
        if (GameManager.instance.playerCoins.playerCurrency < player.upgradeCapacityPrice)
            return;

        GameManager.instance._SubtractCoins(player.upgradeCapacityPrice);
        playerDevice.deviceCapacity += 1;
        player.upgradeCapacityPrice *= 2;
        upgradeCapacityPrice.text = player.upgradeCapacityPrice + "coins";
        //vacuumCapacity += 10;
      //  PlayerController.userPlayer.UpdateStats();
    }


    public void UpgradePlayerAbility()
    {
        if (GameManager.instance.playerCoins.playerCurrency < player.upgradeAbilityPrice)
            return;

        GameManager.instance._SubtractCoins(player.upgradeAbilityPrice);
        player.playerSpeed += 0.5f;
        player.upgradeAbilityPrice *= 2;
        upgradeAbilityPrice.text = player.upgradeAbilityPrice + "coins";
        PlayerController.userPlayer.UpdateStats();
    }
}
