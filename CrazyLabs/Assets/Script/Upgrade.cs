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
    public TextMeshProUGUI upgradeProcessorPrice;
    //public Slider PlayerAbilitySlider;
    public SuctionDeviceSO playerDevice;

    // Start is called before the first frame update
    void Start()
    {
        /*PlayerAbilitySlider.maxValue = 3;
        PlayerAbilitySlider.minValue = 0;
        PlayerAbilitySlider.value = 0;*/
        upgradeAbilityPrice.text = player.upgradeAbilityPrice + "coins";
        upgradeCapacityPrice.text = player.upgradeCapacityPrice + "coins";
        upgradeProcessorPrice.text = player.upgradeProcessorPrice + "coins";
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
        playerDevice.deviceCapacity += 10;
        player.upgradeCapacityPrice *= 2;
        upgradeCapacityPrice.text = player.upgradeCapacityPrice + "coins";
        //vacuumCapacity += 10;
        //currentVacuumCapacity.text = _deviceCapacity + "/ " + vacuumCapacity.ToString();
    }

    public void UpgradeProcessingSpeed()
    {
        if (GameManager.instance.playerCoins.playerCurrency < player.upgradeProcessorPrice)
            return;

        GameManager.instance._SubtractCoins(player.upgradeProcessorPrice);
        playerDevice.offloadRate++;
        player.upgradeProcessorPrice *= 2;
        upgradeProcessorPrice.text = player.upgradeProcessorPrice + "coins";
        //playerDevice.offloadRate++;
    }

    public void UpgradePlayerAbility()
    {
        if (GameManager.instance.playerCoins.playerCurrency < player.upgradeAbilityPrice)
            return;

        GameManager.instance._SubtractCoins(player.upgradeAbilityPrice);
        /*if (PlayerAbilitySlider.value == 3)
            return;
        PlayerAbilitySlider.value++;*/
        //player.playerSpeed *= 1.2f;
        player.playerSpeed += 2f;
        player.upgradeAbilityPrice *= 2;
        upgradeAbilityPrice.text = player.upgradeAbilityPrice + "coins";
    }

    /*
    public void DowngradePlayerAbility()
    {
        if (PlayerAbilitySlider.value == 0)
            return;
        PlayerAbilitySlider.value--;
        //player.playerSpeed /= 1.2f;
        player.playerSpeed -= 2f;
    }*/
}
