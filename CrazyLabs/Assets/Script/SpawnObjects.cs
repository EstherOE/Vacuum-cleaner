using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    public GameObject[] dirt;
  
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnDirt", 0, 2f);
    }

    Vector3 RandomPos()
    {
        //bool validSpawnPoint = false;

        float X = Random.Range(-29, 30);
        float Y = 0.5f;
        float Z = Random.Range(-44, 14);

        Vector3 newPos = new Vector3(X,Y,Z);
        return newPos; 
    }


    void SpawnDirt()
    {
        Instantiate(dirt[Random.Range(0, 3)], RandomPos(), Quaternion.identity);
    }

}
