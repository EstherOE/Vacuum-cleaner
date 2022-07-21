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
            {
                float angle = i * Mathf.PI * 2 / GameManager.instance.gameLevel[GameManager.instance.currentLevelId].numberofCoins;
                float x = Mathf.Cos(angle) * 5f;
                float z = Mathf.Sin(angle) * 5f;
                Vector3 pos = transform.position + new Vector3(x, 0, z);

                float angleDegrees = -angle * Mathf.Rad2Deg;
                Quaternion der = Quaternion.Euler(0, angleDegrees, 0);
                Instantiate(coinObject, pos, transform.rotation);
            }
            
            
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