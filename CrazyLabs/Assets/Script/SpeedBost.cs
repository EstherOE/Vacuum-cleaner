using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBost : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {

            PlayerController.userPlayer.ActivateSpeedBoost();
            Debug.Log("Speed Up");
            Destroy(gameObject);
        }
    }
}
