using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CratesScript : MonoBehaviour
{
    public GameObject coinObject;

   
   

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            
            for (int i = 0; i < GameManager.instance.gameLevel[GameManager.instance.currentLevelId].numberofCoins; i++)
                Instantiate(coinObject, RandomObstaclesPos(), Quaternion.identity);
                
            
            Debug.Log("Coin appears");
            Destroy(gameObject);
        }
    }


   


    Vector3 RandomObstaclesPos()
    {
        float x = Random.Range(0, 20);
        float y = 2.65f;
        float z = Random.Range(-20, 20);

        Vector3 ns = new Vector3(x, y, z);
        return ns;
    }
}